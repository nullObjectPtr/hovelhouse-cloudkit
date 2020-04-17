//
//  CKContainer.cs
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

public class CKContainerTests
{

    [Test]
    public void Default_container_exists()
    {
        var container = CKContainer.DefaultContainer();
        Assert.NotNull(container);
    }

    [Test]
    public void Can_retrieve_container_by_identifier()
    {
        var defaultContainer = CKContainer.DefaultContainer();
        var identifier = defaultContainer.ContainerIdentifier;

        Assert.AreEqual(defaultContainer, CKContainer.ContainerWithIdentifier(identifier));
    }

    [Test]
    public void Can_retrieve_database_by_scope(
        [Values(CKDatabaseScope.Private, CKDatabaseScope.Public, CKDatabaseScope.Shared)]
        CKDatabaseScope scope)
    {
        Assert.NotNull(CKContainer.DefaultContainer().DatabaseWithDatabaseScope(scope));
    }

    [Test]
    public void FetchAllLongLivedOperationIDsWithCompletionHandlerTest()
    {
        // STUB - fix me
        Assert.Fail("FetchAllLongLivedOperationIDsWithCompletionHandlerTest Not Implemented");
    }

    [Test]
    public void FetchUserRecordIDWithCompletionHandlerTest()
    {
        // STUB - fix me
        Assert.Fail("FetchUserRecordIDWithCompletionHandlerTest Not Implemented");
    }

    [Test]
    public void DiscoverUserIdentityWithEmailAddressTest()
    {
        // STUB - fix me
        Assert.Fail("DiscoverUserIdentityWithEmailAddressTest Not Implemented");
    }

    [Test]
    public void FetchShareParticipantWithEmailAddressTest()
    {
        // STUB - fix me
        Assert.Fail("FetchShareParticipantWithEmailAddressTest Not Implemented");
    }

    [Test]
    public void FetchShareParticipantWithPhoneNumberTest()
    {
        // STUB - fix me
        Assert.Fail("FetchShareParticipantWithPhoneNumberTest Not Implemented");
    }

    [Test]
    public void FetchShareParticipantWithUserRecordIDTest()
    {
        // STUB - fix me
        Assert.Fail("FetchShareParticipantWithUserRecordIDTest Not Implemented");
    }

    [Test]
    public void FetchLongLivedOperationWithIDTest()
    {
        // STUB - fix me
        Assert.Fail("FetchLongLivedOperationWithIDTest Not Implemented");
    }

    [Test]
    public void AcceptShareMetadataTest()
    {
        // STUB - fix me
        Assert.Fail("AcceptShareMetadataTest Not Implemented");
    }

    [Test]
    public void RequestApplicationPermissionTest()
    {
        // STUB - fix me
        Assert.Fail("RequestApplicationPermissionTest Not Implemented");
    }

    [Test]
    public void AccountStatusWithCompletionHandlerTest()
    {
        // STUB - fix me
        Assert.Fail("AccountStatusWithCompletionHandlerTest Not Implemented");
    }

    [Test]
    public void StatusForApplicationPermissionTest()
    {
        // STUB - fix me
        Assert.Fail("StatusForApplicationPermissionTest Not Implemented");
    }

    [Test]
    public void Can_add_container_operations()
    {
        var op = new CKDiscoverUserIdentitiesOperation();
        CKContainer.DefaultContainer().AddOperation(op);
    }

    [Test]
    public void Added_operation_sets_container_property()
    {
        var op = new CKDiscoverUserIdentitiesOperation();
        var container = CKContainer.DefaultContainer();

        container.AddOperation(op);

        Assert.AreEqual(op.Configuration.Container, container);
    }

    [Test]
    public void FetchShareMetadataWithURLTest()
    {
        // STUB - fix me
        Assert.Fail("FetchShareMetadataWithURLTest Not Implemented");
    }



    [Test]
    public void Private_database_exists()
    {
        Assert.NotNull(CKContainer.DefaultContainer().PrivateCloudDatabase);
    }

    [Test]
    public void Public_database_exists()
    {
        Assert.NotNull(CKContainer.DefaultContainer().PublicCloudDatabase);
    }

    [Test]
    public void Shared_database_exists()
    {
        Assert.NotNull(CKContainer.DefaultContainer().SharedCloudDatabase);
    }

    [Test]
    public void Containers_have_identifiers()
    {
        Assert.IsNotNull(CKContainer.DefaultContainer().ContainerIdentifier);
    }

    [Test]
    public void AccountChangedNotificationNotificationTest()
    {
        // STUB - fix me
        Assert.Fail("Test Not Implemented");
    }

}