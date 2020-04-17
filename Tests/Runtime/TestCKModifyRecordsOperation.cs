//
//  CKModifyRecordsOperation.cs
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

public class CKModifyRecordsOperationTests
{
    [Test]
    public void Can_create_CKModifyRecordsOperation()
    {
        var sut = new CKModifyRecordsOperation();

        Assert.IsNotNull(sut);
    }

    [Test]
    public void Can_Create_CKModifyRecordsOperation_With_Records_To_Save_And_RecordsIds_To_Delete()
    {
        var sut = new CKModifyRecordsOperation(new CKRecord[] { }, new CKRecordID[] { });

        Assert.IsNotNull(sut);
    }

    [Test]
    public void Can_set_and_retrieve_records_to_save()
    {
        // STUB - fix me
        Assert.Fail("RecordsToSaveTest Not Implemented");
    }

    [Test]
    public void Can_set_and_retrieve_records_to_delete()
    {
        // STUB - fix me
        Assert.Fail("RecordIDsToDeleteTest Not Implemented");
    }

    [Test]
    public void Can_set_and_get_save_policy(
        [Values(CKRecordSavePolicy.SaveAllKeys, CKRecordSavePolicy.SaveChangedKeys, CKRecordSavePolicy.SaveIfServerRecordUnchanged)]
        CKRecordSavePolicy savePolicy
        )
    {
        var op = new CKModifyRecordsOperation();
        op.SavePolicy = savePolicy;

        Assert.AreEqual(op.SavePolicy, savePolicy);
    }

    [Test]
    public void AtomicTest()
    {
        // STUB - fix me
        Assert.Fail("AtomicTest Not Implemented");
    }

    [Test]
    public void ModifyRecordsCompletionBlockTest()
    {
        // STUB - fix me
        Assert.Fail("ModifyRecordsCompletionBlockTest Not Implemented");
    }

    [Test]
    public void PerRecordCompletionBlockTest()
    {
        // STUB - fix me
        Assert.Fail("PerRecordCompletionBlockTest Not Implemented");
    }

    [Test]
    public void PerRecordProgressBlockTest()
    {
        // STUB - fix me
        Assert.Fail("PerRecordProgressBlockTest Not Implemented");
    }



}