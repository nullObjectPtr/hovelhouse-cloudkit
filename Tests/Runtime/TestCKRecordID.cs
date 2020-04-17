//
//  CKRecordID.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 04/08/2020
//  Copyright © 2020 HovelHouseApps. All rights reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential
//

using NUnit.Framework;

using HovelHouse.CloudKit;
using System;

public class CKRecordIDTests
{
    [Test]
    public void Can_create_record_id_with_name()
    {
        var recordName = "record_name";
        var record = new CKRecordID(recordName);
        Assert.AreEqual(record.RecordName, recordName);
    }

    [Test]
    public void Cant_create_record_id_with_invalid_name(
        [Values("2numberfirst", "", "_underscorefirst")]
        string invalidName)
    {
        TestDelegate sut = () => { var recordId = new CKRecordID(invalidName); };

        Assert.Throws<CloudKitException>(sut);
    }

    [Test]
    public void Cant_create_record_id_with_no_name()
    {
        TestDelegate sut = () => { var recordId = new CKRecordID(null); };

        Assert.Throws<ArgumentNullException>(sut);
    }

    [Test]
    public void Can_create_record_id_with_zone_id()
    {
        var zoneId = new CKRecordZoneID("zone_id", "zone_owner");
        var recordId = new CKRecordID("record_name", zoneId);

        Assert.AreEqual(recordId.ZoneID, zoneId);
    }
}