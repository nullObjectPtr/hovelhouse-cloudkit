//
//  CKDatabase.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 04/08/2020
//  Copyright © 2020 HovelHouseApps. All rights reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential
//

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using HovelHouse.CloudKit;
using System;

public class CKDatabaseTests
{
    const float DefaultTimeout = 5f;

    [Test]
    public void Can_add_operation()
    {
        var op = new CKModifyRecordsOperation();
        var database = CKContainer.DefaultContainer().PrivateCloudDatabase;

        TestDelegate sut = () => database.AddOperation(op);

        Assert.DoesNotThrow(sut);
    }

    [Test]
    public void Adding_operation_sets_datbase_property_on_operation()
    {
        var op = new CKModifyRecordsOperation();
        var database = CKContainer.DefaultContainer().PrivateCloudDatabase;

        database.AddOperation(op);

        Assert.AreEqual(op.Database, database);
    }

    [UnityTest]
    public IEnumerator Can_fetch_a_record()
    {
        var database = CKContainer.DefaultContainer().PrivateCloudDatabase;
        var wasCalled = false;

        database.FetchRecordWithID(new CKRecordID("somerecordId"), (record, error) =>
        {
            wasCalled = true;
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
    }

    [UnityTest]
    public IEnumerator Can_save_a_record()
    {
        var database = CKContainer.DefaultContainer().PrivateCloudDatabase;
        var wasCalled = false;
        var record = new CKRecord("somerecordId");
        CKRecord savedRecord = null;

        database.SaveRecord(record, (r, error) =>
        {
            wasCalled = true;
            savedRecord = r;
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
        Assert.AreEqual(savedRecord, record);
    }

    [UnityTest]
    public IEnumerator Can_delete_a_record()
    {
        var database = CKContainer.DefaultContainer().PrivateCloudDatabase;
        var wasCalled = false;

        var record = new CKRecord("testrecord");
        CKRecordID deletedRecordId = null;

        database.SaveRecord(record, (record2, error) => {
            database.DeleteRecordWithID(record2.RecordID, (r2, error2) => {
                deletedRecordId = r2;
                wasCalled = true;
            });
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.AreEqual(record.RecordID, deletedRecordId);
    }

    [UnityTest]
    public IEnumerator Can_fetch_a_record_zone_by_its_id()
    {
        var database = CKContainer.DefaultContainer().PrivateCloudDatabase;
        var wasCalled = false;

        var recordZoneId = new CKRecordZoneID("zonename", "me");
        CKRecordZone returnedZone = null;

        database.FetchRecordZoneWithID(recordZoneId, (zone, error) => {
            wasCalled = true;
            returnedZone = zone;
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
        Assert.AreEqual(recordZoneId, returnedZone.ZoneID);
    }

    [UnityTest]
    public IEnumerator Can_save_a_record_zone()
    {
        var database = CKContainer.DefaultContainer().PrivateCloudDatabase;
        var wasCalled = false;

        var zone = new CKRecordZone(new CKRecordZoneID("zonename", "me"));
        CKRecordZone returnedZone = null;
        NSError returnedError = null;

        database.SaveRecordZone(zone, (z, error) => {
            wasCalled = true;
            returnedZone = z;
            returnedError = error;
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
        Assert.IsNull(returnedError);
        Assert.AreEqual(zone, returnedZone);
    }

    [UnityTest]
    public IEnumerator Can_delete_a_record_zone_by_its_id()
    {
        var database = CKContainer.DefaultContainer().PrivateCloudDatabase;
        var wasCalled = false;

        var zone = new CKRecordZone(new CKRecordZoneID("zonename", "me"));
        CKRecordZoneID deletedZoneId = null;
        NSError returnedError = null;

        database.SaveRecordZone(zone, (zone2, error) => {
            database.DeleteRecordZoneWithID(zone2.ZoneID, (recordZoneId, error2) => {
                wasCalled = true;
                deletedZoneId = recordZoneId;
            });
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
        Assert.IsNull(returnedError);
        Assert.AreEqual(zone.ZoneID, deletedZoneId);
    }

    [UnityTest]
    public IEnumerator Can_fetch_subscription_by_its_id()
    {
        var database = CKContainer.DefaultContainer().PrivateCloudDatabase;
        var wasCalled = false;

        database.FetchSubscriptionWithID("subid", (subscription, error) => {
            wasCalled = true;
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
    }

    [UnityTest]
    public IEnumerator Can_save_subscription()
    {
        var database = CKContainer.DefaultContainer().PrivateCloudDatabase;
        var wasCalled = false;
        CKSubscription subscriptionToSave = null;

        database.SaveSubscription(subscriptionToSave, (sub, error) => {

        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
    }

    [UnityTest]
    public IEnumerator Can_delete_subscription_by_its_id()
    {
        var database = CKContainer.DefaultContainer().PrivateCloudDatabase;
        var wasCalled = false;
        CKSubscription subscriptionToSave = null;

        database.DeleteSubscriptionWithID(subscriptionToSave.SubscriptionID, (sub, error) => {
            wasCalled = true;
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
    }

    [UnityTest]
    public IEnumerator Can_fetch_all_subscriptions()
    {
        var database = CKContainer.DefaultContainer().PrivateCloudDatabase;
        var wasCalled = false;

        database.FetchAllSubscriptionsWithCompletionHandler((subscriptions, error) => {
            wasCalled = true;
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
    }

    [UnityTest]
    public IEnumerator Can_fetch_all_record_zones()
    {
        var database = CKContainer.DefaultContainer().PrivateCloudDatabase;
        var wasCalled = false;

        database.FetchAllRecordZonesWithCompletionHandler((zones, error) => {
            wasCalled = true;
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
    }

    [UnityTest]
    public IEnumerator Can_run_query()
    {
        var database = CKContainer.DefaultContainer().PrivateCloudDatabase;
        var query = new CKQuery("mytype", NSPredicate.PredicateWithValue(true));
        var wasCalled = false;
        var zoneId = new CKRecordZoneID("zoneid", "me");

        database.PerformQuery(query, zoneId, (records, error) => {
            wasCalled = true;
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
    }

    [Test]
    public void Database_has_correct_scope(
        [Values(CKDatabaseScope.Private, CKDatabaseScope.Public, CKDatabaseScope.Shared)]
        CKDatabaseScope scope)
    {
        var database = CKContainer.DefaultContainer().DatabaseWithDatabaseScope(scope);

        Assert.AreEqual(database.DatabaseScope, scope);
    }

    private IEnumerator WaitUntilWithTimeout(Func<bool> condition, float timeLimit)
    {
        float timeElapsed = 0.0f;

        while (!condition() && timeElapsed < timeLimit)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        yield break;
    }
}