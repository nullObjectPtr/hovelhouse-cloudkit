using UnityEngine;
using HovelHouse.CloudKit;
using System;
using System.Linq;

public class Example3_Querying : MonoBehaviour
{
    CKDatabase database;
    string[] names;
    CKRecord[] recordsToSearch;

    // Start is called before the first frame update
    void Start()
    {
        Run();
    }

    private void Run()
    {
        Debug.Log("Example 3 - Querying");
        database = CKContainer.DefaultContainer().PrivateCloudDatabase;

        // We need a bunch of records to search through
        names = new string[] {
            "Alice",
            "Bob",
            "Charles",
            "Danni",
            "Exavier"
        };

        // Make an array of CKRecords and set the name field to each of the
        // names in the array above...
        //
        recordsToSearch = names.Select(name => {
            var record = new CKRecord("Person");
            record.SetString(name, "name");
            return record;
        }).ToArray();

        // CloudKit uses a CKModifyRecrodsOperation for both saving and deleting
        // (which is sorta wierd). The first parameter is records to save, the
        // second is record id's to delete
        //
        Debug.Log("Creating records");
        CKModifyRecordsOperation op = new CKModifyRecordsOperation(
            recordsToSearch,
            null
            );

        // Important to set quality of service to UserInitiated or cloudkit
        // may run your query a LONG time from now. Like minutes from now
        // (seriously). The default value of NSQualityOfServiceUtility is insane
        // You can read more about QoS here:
        // https://developer.apple.com/library/archive/documentation/Performance/Conceptual/EnergyGuide-iOS/PrioritizeWorkWithQoS.html
        op.Configuration.QualityOfService = NSQualityOfService.UserInitiated;

        // The modify records completion block is a callback function that's
        // invoked when the operation is complete
        op.ModifyRecordsCompletionBlock = OnRecordsSaved;

        database.AddOperation(op);

        var op2 = new CKFetchRecordsOperation(recordsToSearch.Select(x => x.RecordID).ToArray());
        op2.FetchRecordsCompletionHandler = (dictionary, error) =>
        {
            Debug.Log("Fetch records complete");
            //foreach (var kvp in dictionary)
            //{
            //    Debug.Log(string.Format("key:{0} value:{1}", kvp.Key.RecordName, kvp.Value));
            //}
        };
        database.AddOperation(op2);
    }

    private void OnRecordsSaved(CKRecord[] savedRecords, CKRecordID[] deletedRecordIds, NSError error)
    {
        if (error != null)
        {
            Debug.LogError(error.LocalizedDescription);
        }
        else
        {
            Debug.Log(string.Format("Saved {0} records with names: {1}",
                savedRecords.Length,
                string.Join(",\n", savedRecords.Select(record => record.RecordID.RecordName))));
            RunQuery();
        }
    }

    private void RunQuery()
    {
        // The objective-c version takes a format string and a variadic argument
        // list for substititon, but we only support sending an unformatted string
        // down. Marshalling variadic arguments is tricky....
        // Just do your formatting on the C# side.

        // Note that passing a bad predicate string will throw an exception

        // Pick a random name out of the hat
        var queryStr = string.Format("name = '{0}'", names[UnityEngine.Random.Range(0, names.Length)]);
        Debug.Log(string.Format("Running query with predicate ({0})", queryStr));

        // Queries are made with NSPredicate which sorta like SQL and sorta
        // like regular expressions.
        // See https://nshipster.com/nspredicate/ for a quick intro to the kinds
        // of queries you can run. Were just going to do a simple key,value search
        CKQuery query = new CKQuery("Person", NSPredicate.PredicateWithFormat(queryStr));

        // The second argument here is the container to search in. Unless you
        // are using custom containers, you'll want to pass null for the
        // default container

        database.PerformQuery(query, null, OnQueryComplete);
    }

    private void OnQueryComplete(CKRecord[] records, NSError error)
    {
        Debug.Log("OnQueryComplete");

        if (error != null)
        {
            Debug.LogError(error.LocalizedDescription);
        }
        else
        {
            var result = records.FirstOrDefault();
            if (result != null)
            {
                Debug.Log(string.Format("found record: '{0}' with name: '{1}'",
                    result.RecordID.RecordName, result.StringForKey("name")));
            }

            // Let's be tidy. Delete all the records we created
            // create an array of record id's from the records we saved
            var recordIds = recordsToSearch.Select(record => record.RecordID).ToArray();

            Debug.Log("Cleaning up, deleting the records we created....");
            var op = new CKModifyRecordsOperation(null, recordIds);

            op.ModifyRecordsCompletionBlock = OnRecordsDeleted;
            op.Configuration.QualityOfService = NSQualityOfService.UserInitiated;

            database.AddOperation(op);
        }
    }

    private void OnRecordsDeleted(CKRecord[] savedRecords, CKRecordID[] deletedRecordIds, NSError error)
    {
        Debug.Log("Records deleted");
        if (error != null)
        {
            Debug.LogError(error.LocalizedDescription);
        }
        else
        {
            Debug.Log("Deleted records");
            var str = string.Join(",\n", deletedRecordIds.Select(id => id.RecordName).ToArray());
            Debug.Log(string.Format("Deleted records: {0}", str));
        }

        Debug.Log("Done");
    }
}
