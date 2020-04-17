//
//  CKShare.cs
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

public class CKShareTests
{
    [Test]
    public void Can_create_CKShare()
    {
        var record = new CKRecord("root_record_type");
        var share = new CKShare(record);

        Assert.AreEqual(share.RecordID, record.RecordID);
    }

    [Test]
    public void Can_create_CKShare_with_coder()
    {
        Assert.Fail("API incomplete");
    }

    [Test]
    public void Cant_create_CKShare_with_null_coder()
    {
        NSCoder coder = null;

        TestDelegate sut = () => new CKShare(coder);

        Assert.Throws<CloudKitException>(sut);
    }

    [Test]
    public void Can_create_CKShare_with_root_record_and_share_id()
    {
        var shareId = new CKRecordID("shareId");
        var rootRecord = new CKRecord("root_record_type");

        var share = new CKShare(rootRecord, shareId);

        Assert.AreEqual(share.RecordID, shareId);
    }

    [Test]
    public void Cant_create_CKShare_with_null_share_id()
    {
        CKRecordID shareId = null;
        var rootRecord = new CKRecord("rootRecord");

        TestDelegate sut = () => new CKShare(rootRecord, shareId);

        Assert.Throws<CloudKitException>(sut);
    }

    [Test]
    public void Cant_create_CKShare_with_no_record()
    {
        CKRecord record = null;
        TestDelegate sut = () => new CKShare(record);

        Assert.Throws<CloudKitException>(sut);
    }

    [Test]
    public void AddParticipantTest()
    {
        // STUB - fix me
        Assert.Fail("AddParticipantTest Not Implemented");
    }

    [Test]
    public void RemoveParticipantTest()
    {
        // STUB - fix me
        Assert.Fail("RemoveParticipantTest Not Implemented");
    }

    [Test]
    public void PublicPermissionTest()
    {
        // STUB - fix me
        Assert.Fail("PublicPermissionTest Not Implemented");
    }

    [Test]
    public void URLTest()
    {
        // STUB - fix me
        Assert.Fail("URLTest Not Implemented");
    }

    [Test]
    public void CurrentUserParticipantTest()
    {
        // STUB - fix me
        Assert.Fail("CurrentUserParticipantTest Not Implemented");
    }

    [Test]
    public void OwnerTest()
    {
        // STUB - fix me
        Assert.Fail("OwnerTest Not Implemented");
    }

    [Test]
    public void ParticipantsTest()
    {
        // STUB - fix me
        Assert.Fail("ParticipantsTest Not Implemented");
    }
}