//
//  CKQuery.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 04/08/2020
//  Copyright © 2020 HovelHouseApps. All rights reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential
//

using NUnit.Framework;
using HovelHouse.CloudKit;

public class CKQueryTests
{
    [Test]
    public void Can_create_CKQuery()
    {
        var recordType = "record_type";
        var predicate = NSPredicate.PredicateWithValue(false);
        var query = new CKQuery(recordType, predicate);

        Assert.AreEqual(query.RecordType, recordType);
        Assert.AreEqual(query.Predicate, predicate);
    }

    [Test]
    public void Cant_create_CKQuery_with_invalid_record_type(
        [Values(null, "")]
        string recordType
        )
    {
        TestDelegate sut = () => {
            var predicate = NSPredicate.PredicateWithValue(false);
            var query = new CKQuery(recordType, predicate);
        };

        Assert.Throws<CloudKitException>(sut);
    }

    [Test]
    public void Cant_create_CKQuery_with_no_predicate()
    {
        NSPredicate predicate = null;

        TestDelegate sut = () => new CKQuery("record_type", predicate);

        Assert.Throws<CloudKitException>(sut);
    }

    [Test]
    public void SortDescriptorsTest()
    {
        // STUB - fix me
        Assert.Fail("SortDescriptorsTest Not Implemented");
    }
}