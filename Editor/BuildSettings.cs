using UnityEngine;

namespace HovelHouse.CloudKit
{
    public class BuildSettings : ScriptableObject
    {
        public bool EnablePostProcessBuild = true;
        public bool EnableCloudKit = true;
        public bool EnableKeyVaueStorage = true;
        public bool EnableDocumentStorage;
        public bool AddDefaultContainers = true;
        public string[] CustomContainers;
    }
}