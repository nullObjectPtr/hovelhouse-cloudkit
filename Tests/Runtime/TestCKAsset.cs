//
//  CKAsset.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 04/08/2020
//  Copyright © 2020 HovelHouseApps. All rights reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential
//

using NUnit.Framework;
using HovelHouse.CloudKit;
using System;

public class CKAssetTests
{
    [Test]
    public void Can_create_CKAsset()
    {
        var url = NSURL.URLWithString("file:///readme.txt");
        var asset = new CKAsset(url);

        Assert.Equals(asset.FileURL, url);
    }

    [Test]
    public void Cant_create_CKAsset_with_no_url()
    {
        NSURL url = null;
        void sut() => new CKAsset(url);

        Assert.Throws<ArgumentNullException>(sut);
    }

    [Test]
    public void Cant_create_CKAsset_with_nonfile_url()
    {
        var url = NSURL.URLWithString("http://www.hovelhouse.com");
        void sut() => new CKAsset(url);

        Assert.Throws<CloudKitException>(sut);
    }
}