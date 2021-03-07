using System.Collections;
using UnityEngine;
using HovelHouse.CloudKit;
using System.Linq;

public class Example_QueryCursor : MonoBehaviour
{
    CKDatabase database;
    CKRecord[] recordsToSearch;

    // Start is called before the first frame update
    void Start()
    {
        Run();
    }

    private void Run()
    {
        Debug.Log("Example - Query Cursors");
        Debug.Log(
            "To run this example, make sure the record type 'Person' is marked queryable in the database schema in the CloudKit dashboard");
        database = CKContainer.DefaultContainer().PrivateCloudDatabase;

        // Make an array of CKRecords and set the name field to each of the
        // names in the array above...
        //
        recordsToSearch = new CKRecord[7];
        for (var i = 0; i < recordsToSearch.Length; i++)
        {
            var record = new CKRecord("Person");
            record.SetString(Utils.RandomString(4), "name");
            recordsToSearch[i] = record;
        }

        // CloudKit uses a CKModifyRecordsOperation for both saving and deleting
        // (which is sorta weird). The first parameter is records to save, the
        // second is record id's to delete
        //
        Debug.Log("Creating records");
        var op = new CKModifyRecordsOperation(
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
    }

    private void OnRecordsSaved(CKRecord[] savedRecords, CKRecordID[] deletedRecordIds, NSError error)
    {
        if (ErrorHandler(error)) { return; }
        
        Debug.Log($"Saved {savedRecords.Length} records");

        StartCoroutine(DelayRunQuery(5f));
    }

    private IEnumerator DelayRunQuery(float secondsDelay)
    {
        // Why do we wait?
        // Apple says records are not indexed immediately
        // https://developer.apple.com/documentation/cloudkit/ckqueryoperation/1515283-recordfetchedblock?language=objc
        // How long is a good amount of time to wait? I don't really have any idea
        // But without a delay you will get inaccurate results having just created the records, try it out: :-)
        Debug.Log($"waiting {secondsDelay} seconds before running query");
        yield return new WaitForSecondsRealtime(secondsDelay);
        RunQuery();
    }
    
    private void RunQuery()
    {
        Debug.Log("Run Query");
        
        // This example shows how to limit the number of results you get from a query
        // And then get the next set of results using a CKQuery Cursor
        // using a CKQueryOperation. Limiting results can't be done using the convenience API
        // By default, all query results are limited to 100 records

        var query = new CKQuery("Person", NSPredicate.PredicateWithValue(true))
        {
            // We'll get our results sorted alphabetically by name
            SortDescriptors = new[] {new NSSortDescriptor("name", true)}
        };
        
        // And we'll get the results two at a time
        var initialQueryOp = new CKQueryOperation(query)
        {
            ResultsLimit = 2, 
            Configuration = {QualityOfService = NSQualityOfService.UserInitiated}
        };

        // Keep track of which cursor index were on, to show that we get these two at a time
        var queryGroupIndex = 0;
        
        // Strangely, you won't get an array of results. But this callback will be invoked for each
        // record in the query. Bizarre API design. 
        initialQueryOp.RecordFetchedHandler = (record) => OnQueryRecordFetched(record, queryGroupIndex);
        
        // This is invoked after we've gotten all records, it contains the query cursor
        // to get the next batch of results, we have to make a new request using the cursor
        // which is a bit annoying
        initialQueryOp.QueryCompletionHandler = (record, error) =>
        {
            OnQueryCursorComplete(record, error, queryGroupIndex);
        };

        database.AddOperation(initialQueryOp);
    }

    private void OnQueryRecordFetched(CKRecord record, int queryGroupIndex)
    {
        Debug.Log($"group: {queryGroupIndex} {record.RecordID} name:{record.StringForKey("name")}");
    }

    private void OnQueryCursorComplete(CKQueryCursor cursor, NSError error, int queryGroupIndex)
    {
        if(ErrorHandler(error)){ return; }
            
        // When there are no more results, the cursor will be null
        if (cursor == null)
        {
            CleanUp();
            return;
        }

        // There are still results to process so make a 
        // New operation, attach the same handlers, but increment the groupindex were trying to keep track of
        // Oh yeah, and set the limit again. It defaults back to 100
        // The limit isn't stored in the cursor. 
        var op = new CKQueryOperation(cursor)
        {
            Configuration = {QualityOfService = NSQualityOfService.UserInitiated},
            ResultsLimit = 2,
            RecordFetchedHandler = (record) => OnQueryRecordFetched(record, queryGroupIndex + 1),
            QueryCompletionHandler = (queryCursor, queryError) =>
                OnQueryCursorComplete(queryCursor, queryError, queryGroupIndex + 1)
        };

        // Run the new operation with the updated cursor
        database.AddOperation(op);
    }

    private void CleanUp()
    {
        // Let's be tidy. Delete all the records we created
        // create an array of record id's from the records we saved
        var recordIds = recordsToSearch.Select(record => record.RecordID).ToArray();

        Debug.Log("Cleaning up, deleting the records we created....");
        var op = new CKModifyRecordsOperation(null, recordIds);

        op.ModifyRecordsCompletionBlock = OnRecordsDeleted;
        op.Configuration.QualityOfService = NSQualityOfService.UserInitiated;

        database.AddOperation(op);
    }

    private void OnRecordsDeleted(CKRecord[] savedRecords, CKRecordID[] deletedRecordIds, NSError error)
    {
        Debug.Log("Records deleted");
        if (ErrorHandler(error))
        {
            return;
        }
    
        Debug.Log("Deleted records");
        var str = string.Join(",\n", deletedRecordIds.Select(id => id.RecordName).ToArray());
        Debug.Log($"Deleted records: {str}");
        Debug.Log("Done");
    }

    private bool ErrorHandler(NSError error)
    {
        if(error != null)
            Debug.LogError(error.LocalizedDescription);
        return error != null;
    }
}
