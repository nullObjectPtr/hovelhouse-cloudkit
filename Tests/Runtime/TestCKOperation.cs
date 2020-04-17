//
//  CKOperation.cs
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


public class CKOperationTests
{
    [Test]
    public void Configuration_Exists()
    {
        var op = new CKModifyRecordsOperation();
        Assert.IsNotNull(op);
    }

    // TODO there has to be a better test than this
    // read about how the operationID is set and test that behavior
    [Test]
    public void OperationIDTest()
    {
        Assert.DoesNotThrow(() => {
            var op = new CKModifyRecordsOperation();
            var id = op.OperationID;
        });
    }

    // TODO there has to be a better test than this
    // look up how to use CKOperation.Group
    [Test]
    public void Can_access_group()
    {
        Assert.DoesNotThrow(() => {
            var op = new CKModifyRecordsOperation();
            var group = op.Group;
        });
    }
}