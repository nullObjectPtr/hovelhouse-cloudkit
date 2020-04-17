//
//  CKRecordZone.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 04/08/2020
//  Copyright © 2020 HovelHouseApps. All rights reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential
//

using NUnit.Framework;

using HovelHouse.CloudKit;
using System;

public class CKRecordZoneTests
{

    [Test]
    public void Default_record_zone_exists()
    {
        var zone = CKRecordZone.DefaultRecordZone();
        Assert.NotNull(zone);
    }

    [Test]
    public void Can_create_record_zone_with_zone_id()
    {
        var zoneId = new CKRecordZoneID("zoneName", "ownerName");
        var zone = new CKRecordZone(zoneId);

        Assert.AreEqual(zoneId, zone.ZoneID);
    }

    [Test]
    public void Cant_create_record_with_no_zone_id()
    {
        TestDelegate sut = () => {
            CKRecordZoneID zoneId = null;
            var zone = new CKRecordZone(zoneId);
        };

        Assert.Throws<ArgumentNullException>(sut);
    }

    [Test]
    public void Can_create_record_zone_with_zone_name()
    {
        var zoneName = "zoneName";
        var zone = new CKRecordZone(zoneName);

        Assert.AreEqual(zone.ZoneID.ZoneName, zoneName);
    }

    [Test]
    public void Cant_create_record_zone_with_invalid_zone_name(
        [Values("", "_underscore", "2numbers")] string zoneName
        )
    {
        TestDelegate sut = () => {
            var zone = new CKRecordZone(zoneName);
        };

        Assert.Throws<CloudKitException>(sut);
    }

    [Test]
    public void Cant_create_record_zone_with_no_zone_name()
    {
        TestDelegate sut = () =>
        {
            string zoneName = null;
            var zone = new CKRecordZone(zoneName);
        };

        Assert.Throws<ArgumentNullException>(sut);
    }

    [Test]
    public void Can_access_zone_capabilities()
    {
        TestDelegate sut = () =>
        {
            var zone = new CKRecordZone("name");
            var capability = zone.Capabilities;
        };
        
        Assert.DoesNotThrow(sut);
    }
}