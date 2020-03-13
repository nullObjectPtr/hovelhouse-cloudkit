using UnityEngine;
using HovelHouse.CloudKit;
using System.Linq;

public class Example5_Zones : MonoBehaviour
{
    CKDatabase database;
    CKRecordZone customZone;

    // Start is called before the first frame update
    void Start()
    {
        Run();
    }

    private void Run()
    {
        Debug.Log("Example 5 - Zones");

        // Apple arcade recommends using zones for different GC users
        customZone = new CKRecordZone("GameCenterUser1");
        database = CKContainer.DefaultContainer().PrivateCloudDatabase;

        database.SaveRecordZone(customZone, OnRecordZoneCreated);
    }

    private void OnRecordZoneCreated(CKRecordZone zone, NSError error)
    {
        Debug.Log(string.Format("Created record zone with name {0}", zone.ZoneID.ZoneName));

        // records you want to save to a custom zone are initialized with the zoneId
        var save1 = new CKRecord("SaveGame", customZone.ZoneID);

        // or if you need a custom record name, initialize with a recordID with the zoneId
        var save2 = new CKRecord("SaveGame", new CKRecordID("MySave", customZone.ZoneID));

        var op = new CKModifyRecordsOperation(new CKRecord[] { save1, save2 }, null);

        op.Configuration.QualityOfService = NSQualityOfService.UserInitiated;
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
            var zoneName = savedRecords.First().RecordID.ZoneID.ZoneName;
            Debug.Log(string.Format("Saved {0} records to zone: {1}", savedRecords.Length, zoneName));
            DeleteZone();
        }
    }

    private void DeleteZone()
    {
        // you can delete and eniter zone, thereby deleting every record within it
        Debug.Log("Deleting Record Zone...");
        var op = new CKModifyRecordZonesOperation(null, new CKRecordZoneID[] { customZone.ZoneID });
        op.Configuration.QualityOfService = NSQualityOfService.UserInitiated;
        op.ModifyRecordZonesCompletionHandler = OnZoneDeleted;
        database.AddOperation(op);
    }

    private void OnZoneDeleted(CKRecordZone[] savedZones, CKRecordZoneID[] deletedZoneIds, NSError error)
    { 
        if (error != null)
        {
            Debug.LogError(error.LocalizedDescription);
        }
        else
        {
            Debug.Log(string.Format("zone {0} successfully deleted", deletedZoneIds[0].ZoneName));
        }

        Debug.Log("Done");
    }
}
