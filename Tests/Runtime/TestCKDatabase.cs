//
//  CKDatabase.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 04/08/2020
//  Copyright © 2020 HovelHouseApps. All rights reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential
//

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using HovelHouse.CloudKit;

public class CKDatabaseTests
{
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

    [Test]
    public void Can_fetch_a_record()
    {
        // STUB - fix me
        Assert.Fail("FetchRecordWithIDTest Not Implemented");
    }

    [Test]
    public void Can_save_a_record()
    {
        // STUB - fix me
        Assert.Fail("SaveRecordTest Not Implemented");
    }

    [Test]
    public void Can_delete_a_record()
    {
        // STUB - fix me
        Assert.Fail("DeleteRecordWithIDTest Not Implemented");
    }

    [Test]
    public void Can_fetch_a_record_zone_by_its_id()
    {
        // STUB - fix me
        Assert.Fail("FetchRecordZoneWithIDTest Not Implemented");
    }

    [Test]
    public void Can_create_a_record_zone()
    {
        // STUB - fix me
        Assert.Fail("SaveRecordZoneTest Not Implemented");
    }

    [Test]
    public void Can_delete_a_record_zone_by_its_id()
    {
        // STUB - fix me
        Assert.Fail("DeleteRecordZoneWithIDTest Not Implemented");
    }

    [Test]
    public void FetchSubscriptionWithIDTest()
    {
        // STUB - fix me
        Assert.Fail("FetchSubscriptionWithIDTest Not Implemented");
    }

    [Test]
    public void SaveSubscriptionTest()
    {
        // STUB - fix me
        Assert.Fail("SaveSubscriptionTest Not Implemented");
    }

    [Test]
    public void DeleteSubscriptionWithIDTest()
    {
        // STUB - fix me
        Assert.Fail("DeleteSubscriptionWithIDTest Not Implemented");
    }

    [Test]
    public void FetchAllSubscriptionsWithCompletionHandlerTest()
    {
        // STUB - fix me
        Assert.Fail("FetchAllSubscriptionsWithCompletionHandlerTest Not Implemented");
    }

    [Test]
    public void FetchAllRecordZonesWithCompletionHandlerTest()
    {
        // STUB - fix me
        Assert.Fail("FetchAllRecordZonesWithCompletionHandlerTest Not Implemented");
    }

    [Test]
    public void PerformQueryTest()
    {
        // STUB - fix me
        Assert.Fail("PerformQueryTest Not Implemented");
    }



    [Test]
    public void Database_has_correct_scope(
        [Values(CKDatabaseScope.Private, CKDatabaseScope.Public, CKDatabaseScope.Shared)]
        CKDatabaseScope scope)
    {
        var database = CKContainer.DefaultContainer().DatabaseWithDatabaseScope(scope);

        Assert.AreEqual(database.DatabaseScope, scope);
    }
}