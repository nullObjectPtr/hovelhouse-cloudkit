//
//  NSUbiquitousKeyValueStore.cs
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

public class NSUbiquitousKeyValueStoreTests
{
    [Test]
    public void Default_store_exists()
    {
        var kvs = NSUbiquitousKeyValueStore.DefaultStore;

        Assert.IsNotNull(kvs);
    }

    [Test]
    public void Can_set_and_retrieve_bool(
        [Values(true, false)] bool value
        )
    {
        var kvs = NSUbiquitousKeyValueStore.DefaultStore;
        var boolKey = "bool_key";

        kvs.SetBool(value, boolKey);

        Assert.AreEqual(kvs.BoolForKey(boolKey), value);
    }

    [Test]
    public void LongLongForKeyTest(
        [Values(long.MaxValue, long.MinValue, 0)]
        long value
        )
    {
        var kvs = NSUbiquitousKeyValueStore.DefaultStore;
        var longKey = "long_key";

        kvs.SetLongLong(value, longKey);

        Assert.AreEqual(kvs.LongLongForKey(longKey), value);
    }

    [Test]
    public void DoubleForKeyTest(
        [Values(double.MinValue, double.MaxValue, double.NegativeInfinity, 0.0, double.PositiveInfinity, double.NaN)]
        double value
        )
    {
        var kvs = NSUbiquitousKeyValueStore.DefaultStore;
        var doubleKey = "double_key";

        kvs.SetDouble(value, doubleKey);

        Assert.AreEqual(kvs.DoubleForKey(doubleKey), value);
    }

    [Test]
    public void StringForKeyTest()
    {
        // STUB - fix me
        Assert.Fail("StringForKeyTest Not Implemented");
    }

    [Test]
    public void SetBoolTest()
    {
        // STUB - fix me
        Assert.Fail("SetBoolTest Not Implemented");
    }

    [Test]
    public void SetDoubleTest()
    {
        // STUB - fix me
        Assert.Fail("SetDoubleTest Not Implemented");
    }

    [Test]
    public void SetLongLongTest()
    {
        // STUB - fix me
        Assert.Fail("SetLongLongTest Not Implemented");
    }

    [Test]
    public void SetStringTest()
    {
        // STUB - fix me
        Assert.Fail("SetStringTest Not Implemented");
    }

    [Test]
    public void RemoveObjectForKeyTest()
    {
        // STUB - fix me
        Assert.Fail("RemoveObjectForKeyTest Not Implemented");
    }

    [Test]
    public void SynchronizeTest()
    {
        // STUB - fix me
        Assert.Fail("SynchronizeTest Not Implemented");
    }

    [Test]
    public void BufferForKeyTest()
    {
        // STUB - fix me
        Assert.Fail("BufferForKeyTest Not Implemented");
    }

    [Test]
    public void BufferForKeyTest2()
    {
        // STUB - fix me
        Assert.Fail("BufferForKeyTest Not Implemented");
    }

    [Test]
    public void BufferForKeyTest3()
    {
        // STUB - fix me
        Assert.Fail("BufferForKeyTest Not Implemented");
    }
}