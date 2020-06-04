//
//  CKDiscoverUserIdentitiesOperation.cs
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
using System;

public class CKDiscoverUserIdentitiesOperationTests
{
    [Test]
    public void Can_create_discover_user_identities_operation()
    {
        var op = new CKDiscoverUserIdentitiesOperation();
        Assert.IsNotNull(op);
    }

    [Test]
    public void Can_create_discover_user_identities_operation_with_lookup_info()
    {
        var lookupInfo = new CKUserIdentityLookupInfo[] { };
        var op = new CKDiscoverUserIdentitiesOperation(lookupInfo);

        Assert.AreSame(lookupInfo, op.UserIdentityLookupInfos);
    }

    [Test]
    public void Cant_create_discover_user_identities_operation_with__null_lookup_info()
    {
        TestDelegate sut = () => new CKDiscoverUserIdentitiesOperation(null);
        Assert.Throws<CloudKitException>(sut);
    }

    //[Test]
    //public void Can_discover_user_identites()
    //{
    //    var op = new CKDiscoverUserIdentitiesOperation();
    //    op.Configuration.QualityOfService = NSQualityOfService.UserInitiated;
    //    op.UserIdentityDiscoveredHandler = OnUserIdentityDiscovered;

    //    var container = CKContainer.DefaultContainer();
    //    container.AddOperation(op);

    //    Assert.Fail("Test not finished");
    //}

    //private void OnUserIdentityDiscovered(CKUserIdentity arg1, CKUserIdentityLookupInfo arg2)
    //{
    //    throw new NotImplementedException();
    //}
}