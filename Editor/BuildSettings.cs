using UnityEngine;

namespace HovelHouse.CloudKit
{
    public class BuildSettings : ScriptableObject
    {
        [Header("Entitlements")]
        public bool EnableCloudKit = true;
        public bool EnableKeyVaueStorage = true;
        public bool EnableDocumentStorage;
        public bool AddDefaultContainers = true;
        public string[] CustomContainers;

        [Header("iOS and tvOS Build Settings")]
        public bool EnablePostProcessIOSandTVOS = true;

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
    }
}