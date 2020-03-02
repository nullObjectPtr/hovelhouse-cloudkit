//
//  CKReference.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 03/02/2020
//  Copyright © 2020 HovelHouseApps. All rights reserved.
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
    public class CKReference : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKReference_initWithRecordID_action(
            IntPtr recordID, 
            long action);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKReference_initWithRecord_action(
            IntPtr record, 
            long action);
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern CKReferenceAction CKReference_GetPropReferenceAction(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKReference_GetPropRecordID(HandleRef ptr);
        
        #endregion

        internal CKReference(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKReference initWithRecordID(
            CKRecordID recordID, 
            CKReferenceAction action
        ){
            if(recordID == null)
                throw new ArgumentNullException(nameof(recordID));
            
            IntPtr ptr = CKReference_initWithRecordID_action(
                recordID != null ? HandleRef.ToIntPtr(recordID.Handle) : IntPtr.Zero,
                (long) action);
            return new CKReference(ptr);
        }
        
        
        public static CKReference initWithRecord(
            CKRecord record, 
            CKReferenceAction action
        ){
            if(record == null)
                throw new ArgumentNullException(nameof(record));
            
            IntPtr ptr = CKReference_initWithRecord_action(
                record != null ? HandleRef.ToIntPtr(record.Handle) : IntPtr.Zero,
                (long) action);
            return new CKReference(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public CKReferenceAction ReferenceAction 
        {
            get 
            { 
                CKReferenceAction referenceAction = CKReference_GetPropReferenceAction(Handle);
                return referenceAction;
            }
        }
        
        public CKRecordID RecordID 
        {
            get 
            { 
                IntPtr recordID = CKReference_GetPropRecordID(Handle);
                return recordID == IntPtr.Zero ? null : new CKRecordID(recordID);
            }
        }
        
        #endregion
        
        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKReference_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        // No base.Dispose() needed
        // All we ever do is decrement the reference count in managed code
        
        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKReference Dispose");
                CKReference_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKReference()
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
