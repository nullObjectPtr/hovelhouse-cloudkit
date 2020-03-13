using UnityEngine;
using HovelHouse.CloudKit;
using System;
using System.Linq;

/// <summary>
/// Shows an example of sending large files and the progress callback
/// </summary>
public class Example6_Progress : MonoBehaviour
{
    int numFiles = 5;
    CKRecord[] records = new CKRecord[5];
    CKDatabase database;

    // Start is called before the first frame update
    void Start()
    {
        Run();
    }

    private void Run()
    {
        Debug.Log("Example 6 - Per Record Progress");

        database = CKContainer.DefaultContainer().PrivateCloudDatabase;

        // Let's create 5 files at 1MB a piece
        // So we have enough data to see some upload progress

        // BTW CloudKit recommends that if your data exceeds 1MB you should put
        // it inside a CKAsset

        for (int i = 0; i < numFiles; i++)
        {
            var record = new CKRecord("BigFile");
            byte[] bytes = new byte[1000000];
            record.SetBuffer(bytes, "bytes");

            records[i] = record;
        }

        var op = new CKModifyRecordsOperation(records, null);
        op.Configuration.QualityOfService = NSQualityOfService.UserInitiated;

        op.PerRecordProgressBlock = OnPerRecordProgress;
        op.PerRecordCompletionBlock = OnPerRecordComplete;
        op.ModifyRecordsCompletionBlock = OnRecordsSaved;

        database.AddOperation(op);
    }

    private void OnRecordsSaved(CKRecord[] savedRecords, CKRecordID[] deletedRecordIds, NSError error)
    {
        if (error != null)
        {
            Debug.LogError(error.LocalizedDescription);
        }
        else
        {
            Debug.Log("All records saved");
            DeleteRecords();
        }
    }

    private void OnPerRecordComplete(CKRecord record, NSError error)
    {   
        if (error != null)
        {
            Debug.LogError(error.LocalizedDescription);
        }
        else
        {
            Debug.Log(string.Format("{0} has finished uploading", record.RecordID.RecordName));
        }
    }

    private void OnPerRecordProgress(CKRecord record, double progress)
    {
        Debug.Log(string.Format("{0} {1}% complete", record.RecordID.RecordName, Math.Round(progress * 100f)));
    }

    private void DeleteRecords()
    {
        Debug.Log("Cleaning Up");
        var op = new CKModifyRecordsOperation(null, records.Select(r => r.RecordID).ToArray());
        op.Configuration.QualityOfService = NSQualityOfService.UserInitiated;
        op.ModifyRecordsCompletionBlock = OnRecordsDeleted;
        database.AddOperation(op);
    }

    private void OnRecordsDeleted(CKRecord[] savedRecords, CKRecordID[] deletedRecordIds, NSError error)
    {
        if (error != null)
        {
            Debug.LogError(error.LocalizedDescription);
        }
        else
        {
            Debug.Log(string.Format("Sucessfully deleted {0} records", deletedRecordIds.Length));
        }

        Debug.Log("Done");
    }
}
