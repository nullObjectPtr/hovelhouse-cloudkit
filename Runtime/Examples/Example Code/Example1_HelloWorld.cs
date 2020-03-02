using UnityEngine;
using HovelHouse.CloudKit;

public class Example1_HelloWorld : AbstractExample
{
    private CKDatabase database;

    // Start is called before the first frame update
    void Start()
    {
        Run();
    }

    private void Run()
    {
        Debug.Log("Example1 - Hello World");

        database = CKContainer.defaultContainer().PrivateCloudDatabase;

        var record = CKRecord.initWithRecordType("Hello");
        record.SetString("Hello World", "Greeting");

        database.SaveRecord(record, OnRecordSaved);
    }

    private void OnRecordSaved(CKRecord record, NSError error)
    {
        EnqueueOnMainThread(() => {
            Debug.Log("OnRecordSaved");
            if (error != null)
            {
                Debug.LogError("Could not save record: " + error.LocalizedDescription);
            }

            database.FetchRecordWithID(record.RecordID, OnRecordFetched);
        });
    }

    private void OnRecordFetched(CKRecord record, NSError error)
    {
        EnqueueOnMainThread(() =>
        {
            if (error != null)
            {
                Debug.LogError("Could not fetch record: " + error.LocalizedDescription);
            }
            else
            {
                Debug.Log(string.Format("Record fetched. Greeting is {0}", record.StringForKey("Greeting")));
            }

            database.DeleteRecordWithID(record.RecordID, OnRecordDeleted);
        });
    }

    private void OnRecordDeleted(CKRecordID recordId, NSError error)
    {
        EnqueueOnMainThread(() =>
        {
            if(error != null)
            {
                Debug.LogError(error.LocalizedDescription);
            }

            Debug.Log("Done");
        });
    }
}
