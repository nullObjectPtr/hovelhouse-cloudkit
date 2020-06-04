//
//  CKAcceptSharesOperation.cs
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
using System.Linq;

public class CKAcceptSharesOperationTests
{
    static CKShareMetadata[][] testCases = new[]
    {
        null,
        new CKShareMetadata[] {},
    };

    [Test]
    public void Can_create_accept_shares_operation()
    {
        var sut = new CKAcceptSharesOperation();
        Assert.IsNotNull(sut);
    }

    [Test]
    public void Can_create_accept_shares_operation_with_metadata()
    {
        var metadata = new CKShareMetadata[] { };
        var sut = new CKAcceptSharesOperation( metadata );

        Assert.IsNull(sut);
    }

    [Test, TestCaseSource("testCases")]
    public void Cant_create_accept_shared_operation_with_invalid_metadata(CKShareMetadata[] metadata)
    {
        TestDelegate sut = () => new CKAcceptSharesOperation(metadata);

        Assert.Throws<CloudKitException>(sut);
    }

    [Test]
    public void Can_set_and_get_share_metadatas()
    {
        //CKShareMetadata[] metadatas = new CKShareMetadata[]
        //{
        //};

        //var operation = new CKAcceptSharesOperation();
        //operation.ShareMetadatas = metadatas;

        //var gotMetadatas = operation.ShareMetadatas;
        //var hasAllMetadata = metadatas.All(x => gotMetadatas.Contains(x));

        //Assert.IsTrue(hasAllMetadata);

        Assert.Fail("Test not implemented");
    }

    [Test]
    public void AcceptSharesHandlerTest()
    {
        // STUB - fix me
        Assert.Fail("AcceptSharesHandlerTest Not Implemented");
    }

    [Test]
    public void PerShareCompletionHandlerTest()
    {
        // STUB - fix me
        Assert.Fail("PerShareCompletionHandlerTest Not Implemented");
    }
}