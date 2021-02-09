//
//  CKFetchRecordZoneChangesOperation.cs
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
    /// A database operation that fetches changes to one or more record zones
    /// </summary>
    public class CKFetchRecordZoneChangesOperation : CKDatabaseOperation, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        
        [DllImport(dll)]
        private static extern IntPtr CKFetchRecordZoneChangesOperation_init(
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        [DllImport(dll)]
        private static extern void CKFetchRecordZoneChangesOperation_SetPropRecordChangedHandler(HandleRef ptr, RecordChangedDelegate recordChangedHandler, out IntPtr exceptionPtr);

        [DllImport(dll)]
        private static extern void CKFetchRecordZoneChangesOperation_SetPropFetchRecordZoneChangesCompletionHandler(HandleRef ptr, FetchRecordZoneChangesCompletionDelegate fetchRecordZoneChangesCompletionHandler, out IntPtr exceptionPtr);

        [DllImport(dll)]
        private static extern void CKFetchRecordZoneChangesOperation_SetPropRecordWithIDWasDeletedHandler(HandleRef ptr, RecordWithIDWasDeletedDelegate recordWithIDWasDeletedHandler, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern bool CKFetchRecordZoneChangesOperation_GetPropFetchAllChanges(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKFetchRecordZoneChangesOperation_SetPropFetchAllChanges(HandleRef ptr, bool fetchAllChanges, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern void CKFetchRecordZoneChangesOperation_GetPropRecordZoneIDs(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        [DllImport(dll)]
        private static extern void CKFetchRecordZoneChangesOperation_SetPropRecordZoneIDs(HandleRef ptr, IntPtr[] recordZoneIDs,
			int recordZoneIDsCount, out IntPtr exceptionPtr);

        

        #endregion

        internal CKFetchRecordZoneChangesOperation(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKFetchRecordZoneChangesOperation(
            )
        {
            
            IntPtr ptr = CKFetchRecordZoneChangesOperation_init(
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        
        /// <value>RecordChangedHandler</value>
        public Action<CKRecord> RecordChangedHandler
        {
            get 
            {
                RecordChangedHandlerCallbacks.TryGetValue(
                    HandleRef.ToIntPtr(Handle), 
                    out ExecutionContext<CKRecord> value);
                return value.Callback;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    RecordChangedHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    RecordChangedHandlerCallbacks[myPtr] = new ExecutionContext<CKRecord>(value);
                }
                CKFetchRecordZoneChangesOperation_SetPropRecordChangedHandler(Handle, RecordChangedHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,ExecutionContext<CKRecord>> RecordChangedHandlerCallbacks = new Dictionary<IntPtr,ExecutionContext<CKRecord>>();

        [MonoPInvokeCallback(typeof(RecordChangedDelegate))]
        private static void RecordChangedHandlerCallback(IntPtr thisPtr, IntPtr _record)
        {
            if(RecordChangedHandlerCallbacks.TryGetValue(thisPtr, out ExecutionContext<CKRecord> callback))
            {
                callback.Invoke(
                        _record == IntPtr.Zero ? null : new CKRecord(_record));
            }
        }

        
        /// <value>FetchRecordZoneChangesCompletionHandler</value>
        public Action<NSError> FetchRecordZoneChangesCompletionHandler
        {
            get 
            {
                FetchRecordZoneChangesCompletionHandlerCallbacks.TryGetValue(
                    HandleRef.ToIntPtr(Handle), 
                    out ExecutionContext<NSError> value);
                return value.Callback;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    FetchRecordZoneChangesCompletionHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    FetchRecordZoneChangesCompletionHandlerCallbacks[myPtr] = new ExecutionContext<NSError>(value);
                }
                CKFetchRecordZoneChangesOperation_SetPropFetchRecordZoneChangesCompletionHandler(Handle, FetchRecordZoneChangesCompletionHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,ExecutionContext<NSError>> FetchRecordZoneChangesCompletionHandlerCallbacks = new Dictionary<IntPtr,ExecutionContext<NSError>>();

        [MonoPInvokeCallback(typeof(FetchRecordZoneChangesCompletionDelegate))]
        private static void FetchRecordZoneChangesCompletionHandlerCallback(IntPtr thisPtr, IntPtr _operationError)
        {
            if(FetchRecordZoneChangesCompletionHandlerCallbacks.TryGetValue(thisPtr, out ExecutionContext<NSError> callback))
            {
                callback.Invoke(
                        _operationError == IntPtr.Zero ? null : new NSError(_operationError));
            }
        }

        
        /// <value>RecordWithIDWasDeletedHandler</value>
        public Action<CKRecordID,string> RecordWithIDWasDeletedHandler
        {
            get 
            {
                RecordWithIDWasDeletedHandlerCallbacks.TryGetValue(
                    HandleRef.ToIntPtr(Handle), 
                    out ExecutionContext<CKRecordID,string> value);
                return value.Callback;
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
                    RecordWithIDWasDeletedHandlerCallbacks[myPtr] = new ExecutionContext<CKRecordID,string>(value);
                }
                CKFetchRecordZoneChangesOperation_SetPropRecordWithIDWasDeletedHandler(Handle, RecordWithIDWasDeletedHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,ExecutionContext<CKRecordID,string>> RecordWithIDWasDeletedHandlerCallbacks = new Dictionary<IntPtr,ExecutionContext<CKRecordID,string>>();

        [MonoPInvokeCallback(typeof(RecordWithIDWasDeletedDelegate))]
        private static void RecordWithIDWasDeletedHandlerCallback(IntPtr thisPtr, IntPtr _recordID, IntPtr _recordType)
        {
            if(RecordWithIDWasDeletedHandlerCallbacks.TryGetValue(thisPtr, out ExecutionContext<CKRecordID,string> callback))
            {
                callback.Invoke(
                        _recordID == IntPtr.Zero ? null : new CKRecordID(_recordID),
                        Marshal.PtrToStringAuto(_recordType));
            }
        }

        
        /// <value>FetchAllChanges</value>
        public bool FetchAllChanges
        {
            get 
            { 
                bool fetchAllChanges = CKFetchRecordZoneChangesOperation_GetPropFetchAllChanges(Handle);
                return fetchAllChanges;
            }
            set
            {
                CKFetchRecordZoneChangesOperation_SetPropFetchAllChanges(Handle, value, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>RecordZoneIDs</value>
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
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * IntPtr.Size));
                    recordZoneIDs[i] = ptr2 == IntPtr.Zero ? null : new CKRecordZoneID(ptr2);
                }

                Marshal.FreeHGlobal(bufferPtr);

                return recordZoneIDs;
            }
            set
            {
                CKFetchRecordZoneChangesOperation_SetPropRecordZoneIDs(Handle, value == null ? null : value.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				value == null ? 0 : value.Length, out IntPtr exceptionPtr);
                
                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKFetchRecordZoneChangesOperation_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected override void Dispose(bool disposing)
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
        public new void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
        
    }
}
