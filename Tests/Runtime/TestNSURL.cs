//
//  NSURL.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 04/08/2020
//  Copyright © 2020 HovelHouseApps. All rights reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential
//

using NUnit.Framework;
using HovelHouse.CloudKit;


public class NSURLTests
{

    [Test]
    public void Can_create_url(
        [Values("http://www.hovelhouse.com")]
        string urlString)
    {
        var url = NSURL.URLWithString(urlString);
        Assert.AreEqual(url.AbsoluteString, urlString);
    }

    [Test]
    public void Can_create_file_url()
    {
        var urlString = "/somefile.bytes";
        var url = NSURL.FileURLWithPath(urlString);

        Assert.AreEqual(url.AbsoluteString, urlString);
    }

    // TODO what other kinds of values would cause NSURL to throw?
    [Test]
    public void Cant_create_invalid_url(
        [Values(null, "")]
        string urlString
        )
    {
        TestDelegate sut = () =>
        {
            var url = NSURL.URLWithString(urlString);
        };

        Assert.Throws<CloudKitException>(sut);
    }
}