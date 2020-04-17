﻿#if UNITY_EDITOR
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using Debug = UnityEngine.Debug;

#if UNITY_IOS || UNITY_TVOS || UNITY_STANDALONE_OSX
using System;
using System.Collections.Generic;
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
                Debug.Log(string.Format("[HovelHouse.CloudKit] building for target: '{0}'", target));

            bool isXCodeTarget = false;

            if(target == BuildTarget.iOS || target == BuildTarget.tvOS)
            {
                isXCodeTarget = true;
            }
            else if(target == BuildTarget.StandaloneOSX)
            {
                // xcode for MacOS builds not supported before this version
#if UNITY_2019_3_OR_NEWER
                string setting = EditorUserBuildSettings.GetPlatformSettings("OSXUniversal", "CreateXcodeProject");
                bool.TryParse(setting, out isXCodeTarget);
#endif
            }

            if(isXCodeTarget)
            {
                PostProcessXCodeProject(path);
            }
            else if(target == BuildTarget.StandaloneOSX)
            {
                PostProcessMacOS(path);
            }
        }

        private const string KeyValueStoreEntitlement = "com.apple.developer.ubiquity-kvstore-identifier";
        private const string ContainersEntitlement = "com.apple.developer.icloud-container-identifiers";
        private const string iCloudServicesEntitlement = "com.apple.developer.icloud-services";

        private static void PostProcessMacOS(string path)
        {
            var settings = GetBuildSettings();
            if(settings.EnablePostProcessMacOS == false)
            {
                Debug.Log("[HovelHouse.CloudKit] skipping post process build step...");
                return;
            }

            try
            {
                // This step merges the plist thats in the app folder with a partial
                // one you specify

                if (settings.PerformMergePlistsStep)
                {
                    Debug.Log("[HovelHouse.CloudKit] merging plists");

                    var appPList = new PlistDocument();
                    var partialPList = new PlistDocument();

                    var appPListPath = Path.Combine(path, "Contents/info.plist");

                    appPList.ReadFromFile(appPListPath);
                    partialPList.ReadFromFile(settings.PlistPath);

                    PlistElementDict partialProot = partialPList.root;
                    PlistElementDict appRoot = appPList.root;

                    foreach (var prop in partialProot.values)
                    {
                        appRoot[prop.Key] = prop.Value;
                    }

                    appPList.WriteToFile(appPListPath);
                }

                var appEntitlementsPath = Path.Combine(path,
                    string.Format("../{0}.entitlements", PlayerSettings.applicationIdentifier.Split('.').Last()));

                var appEntitlements = new PlistDocument();
                appEntitlements.ReadFromFile(settings.EntitlementsPath);

                if (settings.PerformMergeEntitlementsStep)
                {
                    // Add cloud kit service if it's not in there already
                    if (settings.EnableCloudKit)
                    {
                        if (!(appEntitlements.root[iCloudServicesEntitlement] is PlistElementArray cloudServices))
                            cloudServices = appEntitlements.root.CreateArray(iCloudServicesEntitlement);

                        cloudServices.AddIfMissing("CloudKit");
                    }

                    var cloudContainers = appEntitlements.root.CreateArray(ContainersEntitlement);
                    cloudContainers.AddRangeIfMissing(settings.CustomContainers);

                    if (settings.EnableKeyVaueStorage)
                        appEntitlements.root.SetString(KeyValueStoreEntitlement, "$(TeamIdentifierPrefix)$(CFBundleIdentifier)");
                }

                appEntitlements.WriteToFile(appEntitlementsPath);

                // Get filename.
                string applicationPath = path; // Path.Combine(Application.dataPath, "../../Build/macOS");
                string scriptPath = Path.Combine(Application.dataPath, "../Packages/CloudKit/ShellScripts/resign.sh");

                // null checks
                if (string.IsNullOrEmpty(settings.CodeSigningIdentity))
                    throw new Exception("[HovelHouse CloudKit] Please supply a Code Signing Identity in build settings");

                if (string.IsNullOrEmpty(settings.EntitlementsPath))
                    throw new Exception("[HovelHouse CloudKit] Please supply the path to your entitlements file in build settings");

                if (string.IsNullOrEmpty(settings.ProvisioningProfilePath))
                    throw new Exception("[HovelHouse CloudKit] Please supply the path to your provisioning profile in build settings");

                Process proc = new Process();
                proc.StartInfo.CreateNoWindow = false;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.FileName = scriptPath;
                //proc.OutputDataReceived += (obj, evtArgs) => LogOutput(evtArgs);

                var args = string.Format("-a \"{0}\" -i \"{1}\" -e \"{2}\" -v \"{3}\"",
                    applicationPath,
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
                    if(errorMsg != null)
                    {
                        throw new Exception(errorMsg);
                    }
                    else
                    {
                        throw new Exception(resultMsg);
                    }
                }
                else
                {
                    Debug.Log(resultMsg);
                }
            }
            catch(Exception ex)
            {
                throw new BuildFailedException(ex);
            }
        }

#if UNITY_IOS || UNITY_TVOS || UNITY_STANDALONE_OSX
        private static void PostProcessXCodeProject(string path)
        {
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

        private static void LogOutput(DataReceivedEventArgs evtArgs)
        {
            if (evtArgs.Data == null)
                return;

            var matchError = new Regex("^Error*", RegexOptions.IgnoreCase);
            var matchWarn = new Regex("^Warning*", RegexOptions.IgnoreCase);

            if (matchError.IsMatch(evtArgs.Data))
            {
                Debug.LogError(evtArgs.Data);
            }
            else if (matchWarn.IsMatch(evtArgs.Data))
            {
                Debug.LogWarning(evtArgs.Data);
            }
            else
            {
                Debug.Log(evtArgs.Data);
            }
        }
    }
}
#endif