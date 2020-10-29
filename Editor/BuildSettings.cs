using System;
using UnityEngine;

namespace HovelHouse.CloudKit
{
    [Serializable]
    [Flags]
    public enum BackgroundModes
    {
        None = 0x0,
        AudioAirplayPiP = 0x1,
        LocationUpdates = 0x2,
        VoiceOverIP = 0x4,
        NewsstandDownloads = 0x8,
        ExternalAccessoryCommunication = 0x10,
        UsesBluetoothLEAccessory = 0x20,
        ActsAsABluetoothLEAccessory = 0x40,
        BackgroundFetch = 0x80,
        RemoteNotifications = 0x100
    }

    public enum APSEnvironment
    {
        Development,
        Production
    }

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

        [Tooltip("If enabled, adds the BackgroundData capability to the XCode project. The RemoteNotifications background mode is required for CloudKit Subscriptions to work")]
        public bool EnableCloudKitNotifications = true;

        [Tooltip("The Apple Push Notification (APS) environment. This setting only effects MacOS.")]
        public APSEnvironment apsEnvironment;

        [Tooltip("If you use subscriptions - remote notifications must be enabled")]
        public BackgroundModes BackgroundModes = BackgroundModes.RemoteNotifications;
    }
}