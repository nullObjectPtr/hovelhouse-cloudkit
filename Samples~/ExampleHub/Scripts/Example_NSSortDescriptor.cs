using System.Collections;
using System.Linq;
using System.Text;
using HovelHouse.CloudKit;
using UnityEngine;

public class Example_NSSortDescriptor : MonoBehaviour
{
    private CKDatabase database;

    private const string recordType = "Hello";
    private const string primaryFieldKey = "primary";
    private const string secondaryFieldKey = "secondary";

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Example - NSSortDescriptor");
        
        Debug.Log("container is: " + CKContainer.DefaultContainer().ContainerIdentifier);

        database = CKContainer.DefaultContainer().PrivateCloudDatabase;

        var n = 10;
        
        Debug.Log($"making {n} records");
        var records = new CKRecord[n];
        
        for (var i = 0; i < records.Length; i++)
        {
            records[i] = new CKRecord(recordType);
        }

        // Make some records share the same field value
        // so we can test primary/secondary sorting
        for (var i = 1; i < n; i += 2)
        {
            var field = RandomString(8);
            records[i-1].SetString(field, primaryFieldKey);
            records[i].SetString(field, primaryFieldKey);
        }

        for (var i = 0; i < n; i++)
        {
            var field = RandomString(4);
            Debug.Log(field);
            records[i].SetString( field, secondaryFieldKey);
        }

        Debug.Log("Saving records");
        var op = new CKModifyRecordsOperation(records, null);
        op.Configuration.QualityOfService = NSQualityOfService.UserInitiated;
        op.ModifyRecordsCompletionBlock = OnRecordsSaved;
        database.AddOperation(op);
        
        
    }

    private void OnRecordsSaved(CKRecord[] arg1, CKRecordID[] arg2, NSError arg3)
    {
        if (arg3 != null)
        {
            Debug.LogError(arg3);
            return;
        }

        Debug.Log("Records saved");
        DebugLogRecords(arg1);
        
        // wait...some number of seconds to let the database finish indexing
        // this is insane
        // https://developer.apple.com/documentation/cloudkit/ckqueryoperation/1515283-recordfetchedblock?language=objc

        StartCoroutine(RunQueryCo());
    }

    private IEnumerator RunQueryCo()
    {
        Debug.Log("waiting 5 seconds before running query...");
        yield return new WaitForSeconds(5f);
        
        Debug.Log("fetching sorted...");

        // Sort - alphabetically for the primary key, reverse alphabetically for the second
        var primarySort = new CKQuery(recordType, NSPredicate.PredicateWithValue(true));
        primarySort.SortDescriptors = new NSSortDescriptor[]
        {
            new NSSortDescriptor(primaryFieldKey, true),
            new NSSortDescriptor(secondaryFieldKey, false)
        };

        database.PerformQuery(primarySort, null, OnQuery);
    }

    private void DebugLogRecords(CKRecord[] ckRecords)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < ckRecords.Length; i++)
        {
            var record = ckRecords[i];

            var recordName = record.RecordID.RecordName;
            var primary = record.StringForKey(primaryFieldKey);
            var secondary = record.StringForKey(secondaryFieldKey);

            sb.AppendLine($"{i}:{recordName} - primary:'{primary}' secondary:'{secondary}'");
        }
        
        Debug.Log(sb.ToString());
    }

    private void OnQuery(CKRecord[] records, NSError error)
    {
        Debug.Log("query finished");
        
        if (error != null)
        {
            Debug.LogError(error.LocalizedDescription);
            return;
        }
        
        Debug.Log($"got {records.Length} records");
        DebugLogRecords(records);

        Debug.Log("cleaning up...deleting records");
        var deleteOp = new CKModifyRecordsOperation(null, records.Select(r => r.RecordID).ToArray());
        deleteOp.Configuration.QualityOfService = NSQualityOfService.UserInitiated;
        deleteOp.ModifyRecordsCompletionBlock = (ckRecords, ids, arg3) =>
        {
            if (error != null)
            {
                Debug.LogError(error.LocalizedDescription);
            }
            else
            {
                Debug.Log("records deleted, you can exit the sample");
            }
        };
        database.AddOperation(deleteOp);
    }

    private string RandomString(int len)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < len; i++)
            sb.Append((char)('a' + Random.Range(0, 26)));
        return sb.ToString();
    }
}
