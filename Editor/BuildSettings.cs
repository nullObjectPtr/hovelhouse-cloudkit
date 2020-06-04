#if UNITY_EDITOR

#if UNITY_IOS || UNITY_TVOS || UNITY_STANDALONE_OSX
using UnityEditor.iOS.Xcode;
#endif

using UnityEngine;


namespace HovelHouse.CloudKit
{
    public class BuildSettings : ScriptableObject
    {
        [Header("Entitlements")]
        public bool EnableCloudKit = true;
        public bool EnableKeyVaueStorage;
        public bool EnableDocumentStorage;
        public bool AddDefaultContainers = true;
        public string[] CustomContainers;

        [Header("iOS and tvOS Build Settings")]
        public bool EnablePostProcessXCodeProject = true;

        [Header("MacOS Build Settings")]
        public bool EnablePostProcessMacOS = true;

        [Tooltip("The identity used to sign the application")]
        public string CodeSigningIdentity;

        [Tooltip("The path to the provisioning profile you wish to use to sign the app")]
        [FilePath("provisionprofile")]
        public string ProvisioningProfilePath;

        [Tooltip("If enabled performs plist merge using the partial plist you specify")]
        public bool PerformMergePlistsStep;

        [Tooltip("If enabled will add the correct cloud kit values to the entitlements file used for signing")]
        public bool PerformMergeEntitlementsStep = true;

        [Tooltip("Path to the partial plist file")]
        [FilePath("plist")]
        public string PlistPath;

        [Tooltip("Your custom entitlements file")]
        [FilePath("entitlements")]
        public string EntitlementsPath;

#if UNITY_IOS || UNITY_TVOS || UNITY_STANDALONE_OSX
        [Tooltip("If enabled, adds the BackgroundData capability to the XCode project. The RemoteNotifications background mode is required for CloudKit Subscriptions to work")]
        public bool AddBackgroundModes;

        [Tooltip("If you use subscriptions - remote notifications must be enabled")]
        public BackgroundModesOptions BackgroundModes = BackgroundModesOptions.RemoteNotifications;
#endif
    }
}
#endif