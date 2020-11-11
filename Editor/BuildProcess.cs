#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

#if UNITY_IOS || UNITY_TVOS || UNITY_STANDALONE_OSX
using Debug = UnityEngine.Debug;
using System.Diagnostics;
using System;
using UnityEditor.Build;
using System.Linq;
using UnityEditor.iOS.Xcode;
#endif

namespace HovelHouse.CloudKit
{


    [InitializeOnLoad]
    public class BuildProcess
    {
        private static string SettingsAssetFilePath { get { return "Assets/Plugins/HovelHouse/CloudKit/Resources/HovelHouseCloudKitBuildSettings.Asset"; } }

        static BuildProcess()
        {
            // Runs on editor start - makes sure we have a build settings asset
            // in the project
            GetBuildSettings();
        }

        [PostProcessBuild(999)]
        public static void OnPostprocessBuild(BuildTarget target, string path)
        {
#if UNITY_IOS || UNITY_TVOS || UNITY_STANDALONE_OSX
            Debug.Log(string.Format("[HovelHouse.CloudKit] building for target: '{0}'", target));

            bool isXCodeTarget = false;

            if (target == BuildTarget.iOS || target == BuildTarget.tvOS)
            {
                isXCodeTarget = true;
            }
            else if (target == BuildTarget.StandaloneOSX)
            {
                // xcode for MacOS builds not supported before this version
#if UNITY_2019_3_OR_NEWER
                string setting = EditorUserBuildSettings.GetPlatformSettings("OSXUniversal", "CreateXcodeProject");
                bool.TryParse(setting, out isXCodeTarget);
#endif
            }

            if (isXCodeTarget)
            {
                PostProcessXCodeProject(path);
            }
            else if (target == BuildTarget.StandaloneOSX)
            {
                PostProcessMacOS(path);
            }
#endif
        }

#if UNITY_IOS || UNITY_TVOS || UNITY_STANDALONE_OSX
        private const string PushNotificationEntitlementKey = "com.apple.developer.aps-environment";
        private const string KeyValueStoreEntitlement = "com.apple.developer.ubiquity-kvstore-identifier";
        private const string ContainersEntitlement = "com.apple.developer.icloud-container-identifiers";
        private const string iCloudServicesEntitlement = "com.apple.developer.icloud-services";
        private const string ApplicationIdentifierEntitlement = "com.apple.application-identifier";

        private static void PostProcessMacOS(string path)
        {
            var settings = GetBuildSettings();
            if (settings.EnablePostProcessMacOS == false)
            {
                Debug.Log("[HovelHouse.CloudKit] skipping post process build step...");
                return;
            }

            try
            {
                // This step merges the plist thats in the app folder with a partial
                // one you specify
                var appPList = new PlistDocument();
                var appPListPath = Path.Combine(path, "Contents/info.plist");

                appPList.ReadFromFile(appPListPath);
                PlistElementDict appRoot = appPList.root;

                if (settings.PerformMergePlistsStep)
                {
                    Debug.Log("[HovelHouse.CloudKit] merging plists");

                    var partialPList = new PlistDocument();

                    partialPList.ReadFromFile(settings.PlistPath);

                    PlistElementDict partialProot = partialPList.root;
                    
                    foreach (var prop in partialProot.values)
                    {
                        appRoot[prop.Key] = prop.Value;
                    }
                }

                appPList.WriteToFile(appPListPath);

                var appEntitlements = new PlistDocument();

                if (settings.PerformMergeEntitlementsStep && string.IsNullOrEmpty(settings.EntitlementsPath) == false)
                {
                    appEntitlements.ReadFromFile(settings.EntitlementsPath);
                }

                // Add cloud kit service if it's not in there already
                if (settings.EnableCloudKit)
                {
                    if (!(appEntitlements.root[iCloudServicesEntitlement] is PlistElementArray cloudServices))
                        cloudServices = appEntitlements.root.CreateArray(iCloudServicesEntitlement);

                    cloudServices.AddIfMissing("CloudKit");
                }

                var cloudContainers = appEntitlements.root.CreateArray(ContainersEntitlement);
                if(settings.AddDefaultContainers)
                {
                    var defaultContainerName = string.Format("iCloud.{0}", PlayerSettings.applicationIdentifier);
                    cloudContainers.AddIfMissing(defaultContainerName);
                }
                cloudContainers.AddRangeIfMissing(settings.CustomContainers);

                // Normally x-code adds the ApplicationIdentifierEntitlement during signing, but we're not
                // building to an xcode project so we have to do this step manually
                // https://developer.apple.com/documentation/security/keychain_services/keychain_items/sharing_access_to_keychain_items_among_a_collection_of_apps?language=objc

                if (appEntitlements.root.values.ContainsKey(ApplicationIdentifierEntitlement) == false)
                {
                    string teamId = PlayerSettings.iOS.appleDeveloperTeamID;

                    if (string.IsNullOrEmpty(teamId))
                        throw new BuildFailedException("[HovelHouse] Please set an appled delevelop team ID in the iOS project settings. It is required by MacOS for code signing.");

                    string appId = PlayerSettings.applicationIdentifier;

                    appEntitlements.root.SetString(ApplicationIdentifierEntitlement, string.Format("{0}.{1}", teamId, appId));
                }

                if (settings.EnableKeyVaueStorage)
                {
                    appEntitlements.root.SetString(KeyValueStoreEntitlement, "$(TeamIdentifierPrefix)$(CFBundleIdentifier)");
                }

                if (settings.EnableCloudKitNotifications)
                {
                    var apsEnvironment = settings.apsEnvironment.ToString().ToLowerInvariant();
                    appEntitlements.root.AddIfMissing(PushNotificationEntitlementKey, apsEnvironment);
                }

                var documentText = FixupBustedXML(appEntitlements);

                var appEntitlementsPath = Path.Combine(path,
                   string.Format("../{0}.entitlements", PlayerSettings.applicationIdentifier.Split('.').Last()));

                File.WriteAllText(appEntitlementsPath, documentText);

                string scriptPath = Path.GetFullPath("Packages/com.hovelhouse.cloudkit/ShellScripts/resign.sh");

                // null checks
                if (string.IsNullOrEmpty(settings.CodeSigningIdentity))
                    throw new Exception("[HovelHouse CloudKit] Please supply a Code Signing Identity in build settings");

                if (string.IsNullOrEmpty(settings.ProvisioningProfilePath))
                    throw new Exception("[HovelHouse CloudKit] Please supply the path to your provisioning profile in build settings");

                Process proc = new Process();
                proc.StartInfo.CreateNoWindow = false;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.FileName = scriptPath;

                var args = string.Format("-a \"{0}\" -i \"{1}\" -e \"{2}\" -v \"{3}\"",
                    path,
                    settings.CodeSigningIdentity,
                    appEntitlementsPath,
                    settings.ProvisioningProfilePath);

                proc.StartInfo.Arguments = args;

                Debug.Log(string.Format("[HovelHouse CloudKit] {0} {1}", scriptPath, args));

                proc.Start();
                var resultMsg = proc.StandardOutput.ReadToEnd();
                var errorMsg = proc.StandardError.ReadToEnd();

                while (!proc.WaitForExit(10000)) ;

                if (proc.ExitCode != 0)
                {
                    Debug.LogError("[HovelHouse CloudKit] CodeSigning Failed");
                    if (errorMsg != null)
                    {
                        throw new BuildFailedException(errorMsg);
                    }
                    else
                    {
                        throw new BuildFailedException(resultMsg);
                    }
                }
                else
                {
                    Debug.Log(resultMsg);
                }
            }
            catch (Exception ex)
            {
                throw new BuildFailedException(ex);
            }
        }

        // This fixes an issue where Unity will output the xml token '<array />'
        // for an empty array in a pList document
        // x-code command line utilities do not know how to parse this token and
        // this can result in a situation where code signing fails
        private static string FixupBustedXML(PlistDocument appEntitlements)
        {
            var documentText = appEntitlements.WriteToString();
            return documentText.Replace("<array />", "<array/>");
        }

        private static void PostProcessXCodeProject(string path)
        {
            Debug.Log("Post Process X Code Project");

            var settings = GetBuildSettings();
            if (settings.EnablePostProcessXCodeProject == false)
            {
                Debug.Log("[HovelHouse.CloudKit] skipping post process build step...");
                return;
            }

            Debug.Log("[HovelHouse.CloudKit] " + path);

            // When building a MacOS xCode project, unity's GetPBXProjectPath
            // returns the wrong pbxproject path
#if UNITY_STANDALONE_OSX && UNITY_2019_3_OR_NEWER
            string pbxPath = Path.Combine(path, "./project.pbxproj");
#else
            string pbxPath = PBXProject.GetPBXProjectPath(path);
#endif
            var pbxProject = new PBXProject();
            pbxProject.ReadFromFile(pbxPath);

            var name = PlayerSettings.applicationIdentifier.Split('.').Last();


#if UNITY_2019_3_OR_NEWER

            // On MacOS GetUnityManTargetGuid returns null - so we have to look it up by name
            // but honestly, doesn't even look like the ProjectCapabilityManager is doing anything
            // on MacOS
#if UNITY_STANDALONE_OSX
            string targetGUID = pbxProject.TargetGuidByName(name);
            string entitlementsFilename = name + "/" + name + ".entitlements";
#else
            string targetGUID = pbxProject.GetUnityMainTargetGuid();
            string entitlementsFilename = name + ".entitlements";
#endif
            if (string.IsNullOrEmpty(targetGUID))
                throw new BuildFailedException("unable to find the GUID of the build target");

            ProjectCapabilityManager projCapability = new ProjectCapabilityManager(
                pbxPath, entitlementsFilename, null, targetGUID);
#else
            string entitlementsFilename = name + ".entitlements";
            ProjectCapabilityManager projCapability = new ProjectCapabilityManager(
                pbxPath, entitlementsFilename, PBXProject.GetUnityTargetName());
#endif

            projCapability.AddiCloud(
                settings.EnableKeyVaueStorage,
                settings.EnableDocumentStorage,
                settings.EnableCloudKit,
                settings.AddDefaultContainers,
                settings.CustomContainers);

            if (settings.EnableCloudKitNotifications)
            {
                projCapability.AddPushNotifications(settings.apsEnvironment == APSEnvironment.Development);
                projCapability.AddBackgroundModes((BackgroundModesOptions)settings.BackgroundModes);
            }

            projCapability.WriteToFile();
        }
#endif

        private static BuildSettings GetBuildSettings()
        {
            var settings = Resources.Load<BuildSettings>(Path.GetFileNameWithoutExtension(SettingsAssetFilePath));

            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<BuildSettings>();
                Directory.CreateDirectory(Path.GetDirectoryName(SettingsAssetFilePath));
                AssetDatabase.CreateAsset(settings, SettingsAssetFilePath);
            }

            return settings;
        }
    }
}
#endif