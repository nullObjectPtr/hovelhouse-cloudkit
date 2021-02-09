//
//  CKRecord.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 05/28/2020
//  Copyright © 2021 HovelHouseApps. All rights reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential
//

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace HovelHouse.CloudKit
{
    /// <summary>
    /// A single record inside a CloudKit database
    /// </summary>
    /// <remarks>
    /// Everything stored in a database is stored as a CKRecord. All CKRecords are of a &apos;type&apos; which you specify during construction. Records contain one or more fields of a certain limited set of types: strings, bools, ints, doubles, dates, locations, references or assets. All records are uniquely identified by a recordID which you may (optionally) specify. If you don&apos;t specify a record id, one is automatically generated for you. Once a record is saved to the database it will contain useful metadata, such as creation time, last modified time, and change tokens.
    /// </remarks>
    public class CKRecord : CKObject, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        
        [DllImport(dll)]
        private static extern IntPtr CKRecord_initWithRecordType(
            string recordType, 
            out IntPtr exceptionPtr
            );
        
        [DllImport(dll)]
        private static extern IntPtr CKRecord_initWithRecordType_zoneID(
            string recordType, 
            IntPtr zoneID, 
            out IntPtr exceptionPtr
            );
        
        [DllImport(dll)]
        private static extern IntPtr CKRecord_initWithRecordType_recordID(
            string recordType, 
            IntPtr recordID, 
            out IntPtr exceptionPtr
            );
        

        
        
        [DllImport(dll)]
        private static extern void CKRecord_allKeys(
            HandleRef ptr,
            ref IntPtr bufferPtr,
            ref long count);
        

        
        
        [DllImport(dll)]
        private static extern void CKRecord_changedKeys(
            HandleRef ptr,
            ref IntPtr bufferPtr,
            ref long count);
        

        
        
        [DllImport(dll)]
        private static extern void CKRecord_allTokens(
            HandleRef ptr,
            ref IntPtr bufferPtr,
            ref long count);

        [DllImport(dll)]
        private static extern IntPtr CKRecord_bufferForKey(
            HandleRef ptr,
            string key,
            ref IntPtr source,
            ref long length);

        
        [DllImport(dll)]
        private static extern IntPtr CKRecord_stringForKey(
            HandleRef ptr, 
            string key,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern long CKRecord_intForKey(
            HandleRef ptr, 
            string key,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern double CKRecord_doubleForKey(
            HandleRef ptr, 
            string key,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr CKRecord_assetForKey(
            HandleRef ptr, 
            string key,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr CKRecord_referenceForKey(
            HandleRef ptr, 
            string key,
            out IntPtr exceptionPtr);

        [DllImport(dll)]
        private static extern double CKRecord_dateForKey(
            HandleRef ptr, 
            string key,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern void CKRecord_encodeSystemFieldsWithCoder(
            HandleRef ptr, 
            IntPtr coder,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern void CKRecord_setParentReferenceFromRecord(
            HandleRef ptr, 
            IntPtr parentRecord,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern void CKRecord_setParentReferenceFromRecordID(
            HandleRef ptr, 
            IntPtr parentRecordID,
            out IntPtr exceptionPtr);

        [DllImport(dll)]
        private static extern void CKRecord_setBuffer_forKey(
            HandleRef ptr,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)]
            byte[] bytes,
            long length,
            string key);

        
        [DllImport(dll)]
        private static extern void CKRecord_setReference_forKey(
            HandleRef ptr, 
            IntPtr reference,
            string key,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern void CKRecord_setAsset_forKey(
            HandleRef ptr, 
            IntPtr obj,
            string key,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern void CKRecord_setString_forKey(
            HandleRef ptr, 
            string obj,
            string key,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern void CKRecord_setInt_forKey(
            HandleRef ptr, 
            long obj,
            string key,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern void CKRecord_setDouble_forKey(
            HandleRef ptr, 
            double obj,
            string key,
            out IntPtr exceptionPtr);

        [DllImport(dll)]
        private static extern void CKRecord_setDate_forKey(
            HandleRef ptr, 
            double timeSince1970,
            string key,
            out IntPtr exceptionPtr);

        // Properties
        
        [DllImport(dll)]
        private static extern IntPtr CKRecord_GetPropRecordID(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr CKRecord_GetPropRecordType(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern double CKRecord_GetPropCreationDate(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr CKRecord_GetPropCreatorUserRecordID(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern double CKRecord_GetPropModificationDate(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr CKRecord_GetPropLastModifiedUserRecordID(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr CKRecord_GetPropRecordChangeTag(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr CKRecord_GetPropParent(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKRecord_SetPropParent(HandleRef ptr, IntPtr parent, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr CKRecord_GetPropShare(HandleRef ptr);

        

        #endregion

        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0,DateTimeKind.Utc);

        internal CKRecord(IntPtr ptr) : base(ptr) {}
        internal CKRecord(){}
        
        
        
        
        public CKRecord(
            string recordType
            )
        {
            if(recordType == null)
                throw new ArgumentNullException(nameof(recordType));
            
            IntPtr ptr = CKRecord_initWithRecordType(
                recordType, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKRecord(
            string recordType, 
            CKRecordZoneID zoneID
            )
        {
            if(recordType == null)
                throw new ArgumentNullException(nameof(recordType));
            if(zoneID == null)
                throw new ArgumentNullException(nameof(zoneID));
            
            IntPtr ptr = CKRecord_initWithRecordType_zoneID(
                recordType, 
                zoneID != null ? HandleRef.ToIntPtr(zoneID.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKRecord(
            string recordType, 
            CKRecordID recordID
            )
        {
            if(recordType == null)
                throw new ArgumentNullException(nameof(recordType));
            if(recordID == null)
                throw new ArgumentNullException(nameof(recordID));
            
            IntPtr ptr = CKRecord_initWithRecordType_recordID(
                recordType, 
                recordID != null ? HandleRef.ToIntPtr(recordID.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        /// <summary>
        /// </summary>
        /// 
        /// <returns>val</returns>
    
    public string[] AllKeys()
    {
           
        
        IntPtr bufferPtr = IntPtr.Zero;
        long bufferLen = 0;

        CKRecord_allKeys(
            Handle,
            ref bufferPtr,
            ref bufferLen);
            
        string[] val = new string[bufferLen];

        for (int i = 0; i < bufferLen; i++)
        {
            IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * IntPtr.Size));
            val[i] = Marshal.PtrToStringAuto(ptr2);
        }

        Marshal.FreeHGlobal(bufferPtr);

        return val;
        
    }
    

        
        /// <summary>
        /// </summary>
        /// 
        /// <returns>val</returns>
    
    public string[] ChangedKeys()
    {
           
        
        IntPtr bufferPtr = IntPtr.Zero;
        long bufferLen = 0;

        CKRecord_changedKeys(
            Handle,
            ref bufferPtr,
            ref bufferLen);
            
        string[] val = new string[bufferLen];

        for (int i = 0; i < bufferLen; i++)
        {
            IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * IntPtr.Size));
            val[i] = Marshal.PtrToStringAuto(ptr2);
        }

        Marshal.FreeHGlobal(bufferPtr);

        return val;
        
    }
    

        
        /// <summary>
        /// </summary>
        /// 
        /// <returns>val</returns>
    
    public string[] AllTokens()
    {
           
        
        IntPtr bufferPtr = IntPtr.Zero;
        long bufferLen = 0;

        CKRecord_allTokens(
            Handle,
            ref bufferPtr,
            ref bufferLen);
            
        string[] val = new string[bufferLen];

        for (int i = 0; i < bufferLen; i++)
        {
            IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * IntPtr.Size));
            val[i] = Marshal.PtrToStringAuto(ptr2);
        }

        Marshal.FreeHGlobal(bufferPtr);

        return val;
        
    }
    

        
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns>val</returns>
        public byte[] BufferForKey(
            string key)
        {
            IntPtr source = IntPtr.Zero;
            long length = 0;

            CKRecord_bufferForKey(
                Handle,
                key,
                ref source,
                ref length);

            byte[] bytes = new byte[length];
            Marshal.Copy(source, bytes, 0, (int) length);
            return bytes;
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns>val</returns>
        public string StringForKey(
            string key)
        { 
            if(key == null)
                throw new ArgumentNullException(nameof(key));
            
            var val = CKRecord_stringForKey(
                Handle, 
                key, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return Marshal.PtrToStringAuto(val);
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns>val</returns>
        public long IntForKey(
            string key)
        { 
            if(key == null)
                throw new ArgumentNullException(nameof(key));
            
            var val = CKRecord_intForKey(
                Handle, 
                key, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val;
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns>val</returns>
        public double DoubleForKey(
            string key)
        { 
            if(key == null)
                throw new ArgumentNullException(nameof(key));
            
            var val = CKRecord_doubleForKey(
                Handle, 
                key, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val;
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns>val</returns>
        public CKAsset AssetForKey(
            string key)
        { 
            if(key == null)
                throw new ArgumentNullException(nameof(key));
            
            var val = CKRecord_assetForKey(
                Handle, 
                key, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val == IntPtr.Zero ? null : new CKAsset(val);
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns>val</returns>
        public CKReference ReferenceForKey(
            string key)
        { 
            if(key == null)
                throw new ArgumentNullException(nameof(key));
            
            var val = CKRecord_referenceForKey(
                Handle, 
                key, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val == IntPtr.Zero ? null : new CKReference(val);
        }
        
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns>val</returns>
        public DateTime DateForKey(
            string key)
        { 
            if(key == null)
                throw new ArgumentNullException(nameof(key));
            
            var timeSince1970 = CKRecord_dateForKey(
                Handle, 
                key, 
                out var exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            return epoch.AddSeconds(timeSince1970);
        }

        
        
        /// <summary>
        /// </summary>
        /// <param name="coder"></param>
        /// <returns>void</returns>
        public void EncodeSystemFieldsWithCoder(
            NSCoder coder)
        { 
            if(coder == null)
                throw new ArgumentNullException(nameof(coder));
            
            CKRecord_encodeSystemFieldsWithCoder(
                Handle, 
                coder != null ? HandleRef.ToIntPtr(coder.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="parentRecord"></param>
        /// <returns>void</returns>
        public void SetParentReferenceFromRecord(
            CKRecord parentRecord)
        { 
            
            CKRecord_setParentReferenceFromRecord(
                Handle, 
                parentRecord != null ? HandleRef.ToIntPtr(parentRecord.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="parentRecordID"></param>
        /// <returns>void</returns>
        public void SetParentReferenceFromRecordID(
            CKRecordID parentRecordID)
        { 
            
            CKRecord_setParentReferenceFromRecordID(
                Handle, 
                parentRecordID != null ? HandleRef.ToIntPtr(parentRecordID.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="obj"></param><param name="key"></param>
        /// <returns>void</returns>
public void SetBuffer(
            byte[] bytes, 
            string key)
        {
            CKRecord_setBuffer_forKey(
                Handle,
                bytes,
                bytes.Length,
                key);
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="reference"></param><param name="key"></param>
        /// <returns>void</returns>
        public void SetReference(
            CKReference reference, 
            string key)
        { 
            
            if(key == null)
                throw new ArgumentNullException(nameof(key));
            
            CKRecord_setReference_forKey(
                Handle, 
                reference != null ? HandleRef.ToIntPtr(reference.Handle) : IntPtr.Zero, 
                
                key, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="obj"></param><param name="key"></param>
        /// <returns>void</returns>
        public void SetAsset(
            CKAsset obj, 
            string key)
        { 
            
            if(key == null)
                throw new ArgumentNullException(nameof(key));
            
            CKRecord_setAsset_forKey(
                Handle, 
                obj != null ? HandleRef.ToIntPtr(obj.Handle) : IntPtr.Zero, 
                
                key, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="obj"></param><param name="key"></param>
        /// <returns>void</returns>
        public void SetString(
            string obj, 
            string key)
        { 
            
            if(key == null)
                throw new ArgumentNullException(nameof(key));
            
            CKRecord_setString_forKey(
                Handle, 
                obj,
                key, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="obj"></param><param name="key"></param>
        /// <returns>void</returns>
        public void SetInt(
            long obj, 
            string key)
        { 
            
            if(key == null)
                throw new ArgumentNullException(nameof(key));
            
            CKRecord_setInt_forKey(
                Handle, 
                obj, 
                
                key, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="obj"></param><param name="key"></param>
        /// <returns>void</returns>
        public void SetDouble(
            double obj, 
            string key)
        { 
            
            if(key == null)
                throw new ArgumentNullException(nameof(key));
            
            CKRecord_setDouble_forKey(
                Handle, 
                obj, 
                
                key, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }

        public void SetDate(
            DateTime date,
            string key)
        {
            if(key == null)
                throw new ArgumentNullException(nameof(key));

            var timeSince1970 = date.Subtract(epoch).TotalSeconds;
            
            CKRecord_setDate_forKey(
                Handle,
                timeSince1970,
                key, 
                out var exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
        }
        
        
        /// <value>RecordID</value>
        public CKRecordID RecordID
        {
            get 
            { 
                IntPtr recordID = CKRecord_GetPropRecordID(Handle);
                return recordID == IntPtr.Zero ? null : new CKRecordID(recordID);
            }
        }

        
        /// <value>RecordType</value>
        public string RecordType
        {
            get 
            { 
                IntPtr recordType = CKRecord_GetPropRecordType(Handle);
                return Marshal.PtrToStringAuto(recordType);
            }
        }

        
        /// <value>CreationDate</value>
        public DateTime CreationDate
        {
            get 
            { 
                double creationDate = CKRecord_GetPropCreationDate(Handle);
                return epoch.AddSeconds(creationDate);
            }
        }

        
        /// <value>CreatorUserRecordID</value>
        public CKRecordID CreatorUserRecordID
        {
            get 
            { 
                IntPtr creatorUserRecordID = CKRecord_GetPropCreatorUserRecordID(Handle);
                return creatorUserRecordID == IntPtr.Zero ? null : new CKRecordID(creatorUserRecordID);
            }
        }

        
        /// <value>ModificationDate</value>
        public DateTime ModificationDate
        {
            get 
            { 
                double modificationDate = CKRecord_GetPropModificationDate(Handle);
                return epoch.AddSeconds(modificationDate);
            }
        }

        
        /// <value>LastModifiedUserRecordID</value>
        public CKRecordID LastModifiedUserRecordID
        {
            get 
            { 
                IntPtr lastModifiedUserRecordID = CKRecord_GetPropLastModifiedUserRecordID(Handle);
                return lastModifiedUserRecordID == IntPtr.Zero ? null : new CKRecordID(lastModifiedUserRecordID);
            }
        }

        
        /// <value>RecordChangeTag</value>
        public string RecordChangeTag
        {
            get 
            { 
                IntPtr recordChangeTag = CKRecord_GetPropRecordChangeTag(Handle);
                return Marshal.PtrToStringAuto(recordChangeTag);
            }
        }

        
        /// <value>Parent</value>
        public CKReference Parent
        {
            get 
            { 
                IntPtr parent = CKRecord_GetPropParent(Handle);
                return parent == IntPtr.Zero ? null : new CKReference(parent);
            }
            set
            {
                CKRecord_SetPropParent(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>Share</value>
        public CKReference Share
        {
            get 
            { 
                IntPtr share = CKRecord_GetPropShare(Handle);
                return share == IntPtr.Zero ? null : new CKReference(share);
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKRecord_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKRecord Dispose");
                CKRecord_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKRecord()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
        
    }
}
