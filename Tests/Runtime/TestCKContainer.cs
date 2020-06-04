//
//  TestCKContainer.cs
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

public class CKContainerTests
{
    private const float DefaultTimeout = 5.0f;

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
        var otherContainer = CKContainer.ContainerWithIdentifier(identifier);

        Assert.AreEqual(identifier, otherContainer.ContainerIdentifier);

        //These are not the same object?
        //Assert.AreEqual(defaultContainer, otherContainer);
    }

    [Test]
    public void Can_retrieve_database_by_scope(
        [Values(CKDatabaseScope.Private, CKDatabaseScope.Public, CKDatabaseScope.Shared)]
        CKDatabaseScope scope)
    {
        Assert.NotNull(CKContainer.DefaultContainer().DatabaseWithDatabaseScope(scope));
    }

    [UnityTest]
    public IEnumerator Can_fetch_all_long_lived_operationIDs()
    {
        var wasCalled = false;
        CKContainer.DefaultContainer().FetchAllLongLivedOperationIDsWithCompletionHandler((operationIds, error) => {
            wasCalled = true;
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
    }

    [UnityTest]
    public IEnumerator Can_fetch_user_recordID()
    {
        CKRecordID CKRecordID = null;
        var wasCalled = false;

        CKContainer.DefaultContainer().FetchUserRecordIDWithCompletionHandler((ckRecordId, error) => {
            CKRecordID = ckRecordId;
            wasCalled = true;
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
        Assert.IsNotNull(CKRecordID);
    }

    [UnityTest]
    public IEnumerator Can_discover_user_identity_with_email_address()
    {
        var email = "support@hovelhouse.com";
        var wasCalled = false;

        CKContainer.DefaultContainer().DiscoverUserIdentityWithEmailAddress(email, (identity, error) => {
            wasCalled = true;
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
    }

    [UnityTest]
    public IEnumerator Can_fetch_share_participant_with_email_address()
    {
        var email = "support@hovelhouse.com";
        var wasCalled = false;

        CKContainer.DefaultContainer().FetchShareParticipantWithEmailAddress(email, (participant, error) => {
            wasCalled = true;
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
    }

    [UnityTest]
    public IEnumerator Can_fetch_share_participant_with_phone_number()
    {
        var number = "1-800-888-8888";
        var wasCalled = false;

        CKContainer.DefaultContainer().FetchShareParticipantWithPhoneNumber(number, (participant, error) => {
            wasCalled = true;
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
    }

    [UnityTest]
    public IEnumerator Can_fetch_share_participant_with_user_recordID()
    {
        var recordId = new CKRecordID("recordName");
        var wasCalled = false;

        CKContainer.DefaultContainer().FetchShareParticipantWithUserRecordID(recordId, (participant, error) => {
            wasCalled = true;
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
    }

    [UnityTest]
    public IEnumerator Can_fetch_long_lived_operation_by_operation_id()
    {
        var operationId = "operationId";
        var wasCalled = false;

        CKContainer.DefaultContainer().FetchLongLivedOperationWithID(operationId, (operation, error) => {
            wasCalled = true;
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
    }

    [Test]
    public void AcceptShareMetadataTest()
    {
        // STUB - fix me
        Assert.Fail("AcceptShareMetadataTest Not Implemented");
    }

    [UnityTest]
    public IEnumerator Can_request_application_permission()
    {
        var wasCalled = false;

        CKContainer.DefaultContainer().RequestApplicationPermission(CKApplicationPermissions.UserDiscoverability, (status, error) => {
            wasCalled = true;
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
    }

    [UnityTest]
    public IEnumerator Can_get_account_status()
    {
        var wasCalled = false;
        CKContainer.DefaultContainer().AccountStatusWithCompletionHandler((status, error) => {
            wasCalled = true;
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
    }

    [UnityTest]
    public IEnumerator Can_get_status_for_application_permission()
    {
        var wasCalled = false;

        var permission = CKApplicationPermissions.UserDiscoverability;
        CKContainer.DefaultContainer().StatusForApplicationPermission(permission, (status, error) => {
            wasCalled = true;
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
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

    [UnityTest]
    public IEnumerator Can_fetch_share_metadata_by_URL()
    {
        var url = NSURL.URLWithString("http://www.hovelhouse.com");
        var wasCalled = false;

        CKContainer.DefaultContainer().FetchShareMetadataWithURL(url, (shareMetadata, error) => {
            wasCalled = true;
        });

        yield return WaitUntilWithTimeout(() => wasCalled, DefaultTimeout);

        Assert.IsTrue(wasCalled);
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
    public void Can_add_account_changed_notification_observer()
    {
        var unsub = CKContainer.AddAccountChangedNotificationObserver((notification) => { });
        Assert.IsNotNull(unsub);
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