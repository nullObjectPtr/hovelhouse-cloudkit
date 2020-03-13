using UnityEngine;
using HovelHouse.CloudKit;
using System.Text;

/// <summary>
/// This example shows the basic database operations of Save, Fetch and Delete
/// </summary>
public class Example2_BasicDatabaseOperations : MonoBehaviour
{
    private CKDatabase database;
    private CKRecord record;

    // Start is called before the first frame update
    void Start()
    {
        Run();
    }

    private void Run()
    {
        Debug.Log("Example 2 - Basic Database Operations");

        database = CKContainer.DefaultContainer().PrivateCloudDatabase;

        record = new CKRecord("MyType");
        record.SetString("An Example", "MyField");

        Debug.Log("Saving a record");
        database.SaveRecord(record, OnRecordSaved);
    }

    private void OnRecordSaved(CKRecord record, NSError error)
    {
        Debug.Log("OnRecordSaved");
        
        if (error != null)
        {
            Debug.LogError(error.LocalizedDescription);
        }
        else
        {
            Debug.Log(string.Format("record saved with name: '{0}'",
                record.RecordID.RecordName));

            // Let's fetch what we just saved
            FetchRecord(record.RecordID.RecordName);
        }
    }

    private void FetchRecord(string recordName)
    {
        // Most operations are done via using a recordID as a handle to the
        // record itself. If you don't have an instance of the record laying
        // around to get it's record ID from. This is how you would go about
        // creating one using a known record name...

        var recordId = new CKRecordID(recordName);
        database.FetchRecordWithID(recordId, OnRecordFetched);

        // if you don't specify a record name when you create the record one is
        // automatically created for you. If you want to manually set one you
        // can do it like this...
        // var record = new CKRecord("MyType", new CKRecordID("ExampleRecordName"))
    }

    private void OnRecordFetched(CKRecord record, NSError error)
    {
        if(error != null)
        {
            Debug.LogError(error.LocalizedDescription);
        }
        else
        {
            Debug.Log(string.Format("Record fetched"));

            // CKRecords have a bunch of useful metadata
            Debug.Log("CreationDate: " + record.CreationDate);
            Debug.Log("ModificationDate: " + record.ModificationDate);

            // The record change tag is an id that helps you keep track of the
            // most up to date version 
            Debug.Log("RecordChangeTag: " + record.RecordChangeTag);

            // The parent of this record (if any). Parent child relationships
            // can be created using SetRefereneceForKey
            Debug.Log("Parent: " + record.Parent);

            Debug.Log("Type: " + record.RecordType);
            Debug.Log("Share: " + record.Share);

            // The user id of the account to create/modify this record
            // in a private DB I think this is just always the current user
            Debug.Log("CreatorUserRecordID: " + record.CreatorUserRecordID);
            Debug.Log("Last Modified User Record Id: " + record.LastModifiedUserRecordID);

            ModifyRecord();
        }
    }

    private void ModifyRecord()
    {
        // There's no "modify record" operation in cloud-kit. To modify a record
        // you just re-save a record whose 'RecordChangeTag' matches the tag on
        // the server. This ensures that you are always modifying the most recent
        // version of the record.

        // For a single client application, this should always be the same

        // So let's change a few values and re-save the record

        // Cloudkit stores both fixed and floating percision types in the same
        // Number type
        record.SetInt(UnityEngine.Random.Range(0, int.MaxValue), "MyNumber");
        record.SetDouble(UnityEngine.Random.Range(0f, float.MaxValue), "MyNumber");

        // You can also store arbitrary byte data
        byte[] bytes = Encoding.ASCII.GetBytes("Test Buffer");
        record.SetBuffer(bytes, "TestBufferKey");

        // You can't change the type of a field you set previously
        // This would generate an error...
        // record.SetInt(0, "MyField");

        database.SaveRecord(record, OnRecordModified);
    }

    private void OnRecordModified(CKRecord record, NSError error)
    {
        if (error != null)
        {
            Debug.LogError(error.LocalizedDescription);
        }
        else
        {
            // Retrieve values you set using the "Object"ForKey methods...
            Debug.Log(string.Format("record '{0}' MyNumber is:{1}",
                record.RecordID.RecordName, record.StringForKey("MyField")));

            // Notice the record change tag has changed after you modification
            Debug.Log("RecordChangeTag: " + record.RecordChangeTag);

            database.DeleteRecordWithID(record.RecordID, OnRecordDeleted);
        }
    }

    private void OnRecordDeleted(CKRecordID recordId, NSError error)
    {
        if (error != null)
        {
            Debug.LogError(error.LocalizedDescription);
        }
        else
        {
            Debug.Log(string.Format("record: '{0}' deleted", recordId.RecordName));
        }

        Debug.Log("Done");
    }
}

