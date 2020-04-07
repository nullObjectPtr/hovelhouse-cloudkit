using System.Collections;
using UnityEngine;
using HovelHouse.CloudKit;
using UnityEngine.Networking;
using System.IO;
using System.Text;

public class Example4_CKAsset : MonoBehaviour
{
    private CKDatabase database;

    // Start is called before the first frame update
    void Start()
    {
        Run();
    }

    void Run()
    {
        Debug.Log("Example 4 - CKAsset");
        database = CKContainer.DefaultContainer().PrivateCloudDatabase;

        var record = new CKRecord("MyType");

        // CloudKit recommends if your data is larger than 1MB that you store
        // it as an asset...
        // our example won't be anything close to 1MB, but it'll illustrate the
        // technique

#if UNITY_TVOS
        string path = Path.Combine(Application.temporaryCachePath, "Asset.bytes");
#else
        string path = Path.Combine(Application.persistentDataPath, "Asset.bytes");
#endif

        byte[] bytes = Encoding.ASCII.GetBytes("AssetData");
        File.WriteAllBytes(path, bytes);

        // Assets have to be files, so you pass it the filepath to something
        // in the user's data directory
        // the asset will be stored in cloudkit, with a URL for retrieval

        var fileurl = NSURL.FileURLWithPath(path);
        record.SetAsset(new CKAsset(fileurl), "MyAsset");

        database.SaveRecord(record, OnRecordSaved);
    }

    private void OnRecordSaved(CKRecord record, NSError error)
    {
        if (error != null)
        {
            Debug.LogError(error.LocalizedDescription);
        }
        else
        {
            Debug.Log(string.Format("Record saved with name: {0}", record.RecordID.RecordName));
            // Once saved, the FileURL may (but may not) point to a URL with the
            // asset contents. It may still point to the local filesys
            // See: https://developer.apple.com/documentation/cloudkit/ckasset/1515050-fileurl?language=objc

            CKAsset asset = record.AssetForKey("MyAsset");
            Debug.Log("Asset data is now at: " + asset.FileURL.AbsoluteString);
            StartCoroutine(GetRequest(asset.FileURL.AbsoluteString));
        }
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            Debug.Log("Grabbing asset data...");

            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log("Received: " + webRequest.downloadHandler.text);
            }

            Debug.Log("Done");
        }
    }
}