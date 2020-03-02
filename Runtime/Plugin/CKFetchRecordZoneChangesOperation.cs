//
//  CKFetchRecordZoneChangesOperation.cs
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
    public class CKFetchRecordZoneChangesOperation : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKFetchRecordZoneChangesOperation_init();
        

        // Instance Methods
        

        

        // Properties
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchRecordZoneChangesOperation_SetPropRecordWithIDWasDeletedHandler(HandleRef ptr, RecordWithIDWasDeletedDelegate recordWithIDWasDeletedHandler);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern bool CKFetchRecordZoneChangesOperation_GetPropFetchAllChanges(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchRecordZoneChangesOperation_SetPropFetchAllChanges(HandleRef ptr, bool fetchAllChanges);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchRecordZoneChangesOperation_GetPropRecordZoneIDs(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchRecordZoneChangesOperation_SetPropRecordZoneIDs(HandleRef ptr, IntPtr[] recordZoneIDs,
			int recordZoneIDsCount);
        
        #endregion

        internal CKFetchRecordZoneChangesOperation(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKFetchRecordZoneChangesOperation init(
        ){
            
            IntPtr ptr = CKFetchRecordZoneChangesOperation_init();
            return new CKFetchRecordZoneChangesOperation(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public Action<CKRecordID,string> RecordWithIDWasDeletedHandler 
        {
            get 
            {
                Action<CKRecordID,string> value;
                RecordWithIDWasDeletedHandlerCallbacks.TryGetValue(HandleRef.ToIntPtr(Handle), out value);
                return value;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    RecordWithIDWasDeletedHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    RecordWithIDWasDeletedHandlerCallbacks[myPtr] = value;
                }
                CKFetchRecordZoneChangesOperation_SetPropRecordWithIDWasDeletedHandler(Handle, RecordWithIDWasDeletedHandlerCallback);
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKRecordID,string>> RecordWithIDWasDeletedHandlerCallbacks = new Dictionary<IntPtr,Action<CKRecordID,string>>();

        [MonoPInvokeCallback(typeof(RecordWithIDWasDeletedDelegate))]
        private static void RecordWithIDWasDeletedHandlerCallback(IntPtr thisPtr, IntPtr _recordID, IntPtr _recordType)
        {
            if(RecordWithIDWasDeletedHandlerCallbacks.TryGetValue(thisPtr, out Action<CKRecordID,string> callback))
            {
                Dispatcher.Instance.EnqueueOnMainThread(() => 
                    callback(
                        _recordID == IntPtr.Zero ? null : new CKRecordID(_recordID),
                        Marshal.PtrToStringAuto(_recordType)));
            }
        }

        
        public bool FetchAllChanges 
        {
            get 
            { 
                bool fetchAllChanges = CKFetchRecordZoneChangesOperation_GetPropFetchAllChanges(Handle);
                return fetchAllChanges;
            }
            set
            {
                CKFetchRecordZoneChangesOperation_SetPropFetchAllChanges(Handle, value);
            }
        }
        
        public CKRecordZoneID[] RecordZoneIDs 
        {
            get 
            { 
                IntPtr bufferPtr = IntPtr.Zero;
                long bufferLen = 0;

                CKFetchRecordZoneChangesOperation_GetPropRecordZoneIDs(Handle, ref bufferPtr, ref bufferLen);

                var recordZoneIDs = new CKRecordZoneID[bufferLen];

                for (int i = 0; i < bufferLen; i++)
                {
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * 8));
                    recordZoneIDs[i] = ptr2 == IntPtr.Zero ? null : new CKRecordZoneID(ptr2);
                }

                Marshal.FreeHGlobal(bufferPtr);

                return recordZoneIDs;
            }
            set
            {
                CKFetchRecordZoneChangesOperation_SetPropRecordZoneIDs(Handle, value == null ? null : value.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				value == null ? 0 : value.Length);
            }
        }

        
        #endregion
        
        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchRecordZoneChangesOperation_Dispose(HandleRef handle);
            
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
                
                //Debug.Log("CKFetchRecordZoneChangesOperation Dispose");
                CKFetchRecordZoneChangesOperation_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKFetchRecordZoneChangesOperation()
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
