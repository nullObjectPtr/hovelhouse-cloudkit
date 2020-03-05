#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

#if UNITY_IOS || UNITY_TVOS
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

        [PostProcessBuild]
        public static void OnPostprocessBuild(BuildTarget target, string path)
        {
            if (target != BuildTarget.iOS && target != BuildTarget.tvOS)
                return;

#if UNITY_IOS || UNITY_TVOS
            var settings = GetBuildSettings();
            if (settings.EnablePostProcessBuild == false)
            {
                Debug.Log("[HovelHouse.CloudKit] skipping post process build step...");
                return;
            }

            Debug.Log(string.Format("[HovelHouse.CloudKit] building for target: '{0}'", target));

            string pbxPath = PBXProject.GetPBXProjectPath(path);
            var pbxProject = new PBXProject();
            pbxProject.ReadFromFile(pbxPath);

            var name = PlayerSettings.applicationIdentifier.Split('.').Last();

#if UNITY_2019_3_OR_NEWER
            ProjectCapabilityManager projCapability = new ProjectCapabilityManager(
                pbxPath, name + ".entitlements", null, pbxProject.GetUnityMainTargetGuid());
#else
            ProjectCapabilityManager projCapability = new ProjectCapabilityManager(
                pbxPath, name + ".entitlements", PBXProject.GetUnityTargetName());
#endif

            projCapability.AddiCloud(
                settings.EnableKeyVaueStorage,
                settings.EnableDocumentStorage,
                settings.EnableCloudKit,
                settings.AddDefaultContainers,
                settings.CustomContainers);

            projCapability.WriteToFile();
#endif
        }

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