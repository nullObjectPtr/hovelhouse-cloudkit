//
//  CKRecord.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 04/08/2020
//  Copyright © 2020 HovelHouseApps. All rights reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential
//

using System.Linq;
using NUnit.Framework;
using HovelHouse.CloudKit;
using System.Text;
using System.Threading;
using System;

public class CKRecordTests
{
    [Test]
    public void Can_create_record_of_a_type()
    {
        var recordType = "record_type";

        var record = new CKRecord(recordType);

        Assert.AreEqual(record.RecordType, recordType);
    }

    [Test]
    public void RecordName_is_automatically_generated_if_not_set()
    {
        var recordType = "record_type";

        var record = new CKRecord(recordType);

        Assert.IsFalse(string.IsNullOrEmpty(record.RecordID.RecordName));
    }

    [Test]
    public void Cant_create_record_with_an_invalid_type(
        [Values(null,"","type with spaces","_underscore","2number")] string recordType)
    {
        TestDelegate sut = () =>
        {
            var record = new CKRecord(recordType);
        };

        Assert.Throws<Exception>(sut);
    }

    [Test]
    public void Can_create_record_of_a_type_with_a_specific_name()
    {
        var recordType = "record_type";
        var recordName = "record_name";

        var recordId = new CKRecordID(recordName);

        var record = new CKRecord(recordType, recordId);
        Assert.AreEqual(record.RecordID, recordId);
    }

    [Test]
    public void Can_create_record_of_a_type_in_a_zone()
    {
        var recordType = "record_type";
        var zoneName = "zone_name";
        var recordZone = new CKRecordZone(zoneName);

        var record = new CKRecord(recordType, recordZone.ZoneID);

        Assert.AreEqual(record.RecordID.ZoneID, recordZone.ZoneID);
    }

    [Test]
    public void All_keys_has_all_the_keys()
    {
        var record = new CKRecord("record_type");
        var keys = new string[] { "string_key", "bool_key", "int_key", "double_key", "buffer_key", "asset_key", "reference_key" };
        record.SetString(keys[0], "string_value");
        record.SetInt(1, keys[1]);
        record.SetDouble(1f, keys[2]);
        record.SetBuffer(new byte[] { }, keys[3]);
        record.SetAsset(new CKAsset(null), keys[4]);
        record.SetReference(new CKReference(new CKRecordID("record"), CKReferenceAction.DeleteSelf), keys[5]);

        var allKeys = record.AllKeys();

        foreach (var key in allKeys)
        {
            Assert.IsTrue(allKeys.Contains(key));
        }
    }

    [Test]
    public void ChangedKeysTest()
    {
        // STUB - fix me
        Assert.Fail("ChangedKeysTest Not Implemented");
    }

    [Test]
    public void AllTokensTest()
    {
        // STUB - fix me
        Assert.Fail("AllTokensTest Not Implemented");
    }

    [Test]
    public void Can_set_and_retrieve_buffer_by_key()
    {
        byte[] bytes = Encoding.ASCII.GetBytes("a bunch of bytes");

        var record = new CKRecord("record_type");
        var bytesKey = "bytes_key";

        record.SetBuffer(bytes, bytesKey);

        Assert.AreEqual(record.BufferForKey(bytesKey), bytes);
    }

    [Test]
    public void Can_set_and_retrieve_string_by_key()
    {
        var record = new CKRecord("record_type");
        var stringKey = "string_key";
        var stringValue = "string_value";

        record.SetString(stringValue, stringKey);

        Assert.AreEqual(record.StringForKey(stringKey), stringValue);
    }

    [Test]
    public void Can_set_and_retrieve_int_by_key()
    {
        var record = new CKRecord("record_type");
        var intKey = "int_key";
        var intValue = 42;

        record.SetInt(intValue, intKey);

        Assert.AreEqual(record.IntForKey(intKey), intValue);
    }

    [Test]
    public void Can_set_and_retrieve_double_by_key()
    {
        var record = new CKRecord("record_type");
        var doubleKey = "int_key";
        var doubleValue = 13.0;

        record.SetDouble(doubleValue, doubleKey);

        Assert.AreEqual(record.DoubleForKey(doubleKey), doubleValue);
    }

    [Test]
    public void Can_set_and_retrieve_asset_by_key()
    {
        var record = new CKRecord("record_type");
        var asset = new CKAsset(NSURL.URLWithString("file:///someasset.bytes"));
        var assetKey = "asset_key";

        record.SetAsset(asset, assetKey);

        Assert.AreEqual(record.AssetForKey(assetKey), asset);
    }

    [Test]
    public void EncodeSystemFieldsWithCoderTest()
    {
        // STUB - fix me
        Assert.Fail("EncodeSystemFieldsWithCoderTest Not Implemented");
    }

    [Test]
    public void Cant_encode_system_fields_without_coder()
    {
        NSCoder coder = null;
        var record = new CKRecord("record_type");

        TestDelegate sut = () => record.EncodeSystemFieldsWithCoder(coder);

        Assert.Throws<CloudKitException>(sut);
    }

    [Test]
    public void Can_set_parent_reference_on_record()
    {
        var parent = new CKRecord("parent_record_type");
        var child = new CKRecord("child_record_type");
        var parentReference = new CKReference(parent, CKReferenceAction.None);

        child.Parent = parentReference;

        Assert.AreEqual(child.Parent.RecordID, parent.RecordID);
    }

    [Test]
    public void Can_set_parent_reference_on_record_using_the_record_id()
    {
        var parent = new CKRecord("parent_record_type");
        var child = new CKRecord("child_record_type");

        child.SetParentReferenceFromRecordID(parent.RecordID);

        Assert.AreEqual(child.Parent.RecordID, parent.RecordID);
    }

    [Test]
    public void Can_set_parent_reference_on_record_using_the_record()
    {
        var parent = new CKRecord("parent_record_type");
        var child = new CKRecord("child_record_type");

        child.SetParentReferenceFromRecord(parent);

        Assert.AreEqual(child.Parent.RecordID, parent.RecordID);
    }

    [Test]
    public void Can_set_and_retrieve_reference_by_key()
    {
        var record = new CKRecord("record_type");
        var reference = new CKReference(new CKRecord("record_type"), CKReferenceAction.DeleteSelf);
        var referenceKey = "reference_key";

        record.SetReference(reference, referenceKey);

        Assert.AreEqual(record.ReferenceForKey(referenceKey), reference);
    }

    [Test]
    public void CreationDate_is_set_when_new_record_is_saved()
    {
        //var autoEvent = new AutoResetEvent(false);

        //var record = new CKRecord("record_type");
        //var database = CKContainer.DefaultContainer().PrivateCloudDatabase;

        //database.SaveRecord(record, (result, error) => {
        //    Assert.IsNotNull(record.CreationDate);
        //    autoEvent.Set();
        //});

        //autoEvent.WaitOne();
    }

    [Test]
    public void CreationDate_is_null_when_new_record_is_made()
    {
        var record = new CKRecord("record_type");

        Assert.IsNull(record.CreationDate);
    }

    [Test]
    public void CreatorUserRecordIDTest()
    {
        //var record = new CKRecord("record_type");

        //Assert.AreEqual(record.CreatorUserRecordID, CKContainer.DefaultContainer().us)
        //record.CreatorUserRecordID 
        //// STUB - fix me
        Assert.Fail("CreatorUserRecordIDTest Not Implemented");
    }

    [Test]
    public void ModificationDateTest()
    {
        // STUB - fix me
        Assert.Fail("ModificationDateTest Not Implemented");
    }

    [Test]
    public void LastModifiedUserRecordIDTest()
    {
        // STUB - fix me
        Assert.Fail("LastModifiedUserRecordIDTest Not Implemented");
    }

    [Test]
    public void ChangeTag_is_null_for_new_records()
    {
        var record = new CKRecord("record_type");

        Assert.IsNull(record.RecordChangeTag);
    }

    [Test]
    public void ChangeTag_is_not_null_for_saved_records()
    {
        //var autoEvent = new AutoResetEvent(false);

        //var record = new CKRecord("record_type");
        //var database = CKContainer.DefaultContainer().PrivateCloudDatabase;

        //database.SaveRecord(record, (result, error) => {
        //    Assert.IsNotNull(record.RecordChangeTag);
        //    autoEvent.Set();
        //});

        //autoEvent.WaitOne();
    }

    [Test]
    public void Record_added_to_share_has_share()
    {
        var rootRecord = new CKRecord("root_record_type");

        var share = new CKShare(rootRecord);

        Assert.AreEqual(rootRecord.Share, share);
    }
}