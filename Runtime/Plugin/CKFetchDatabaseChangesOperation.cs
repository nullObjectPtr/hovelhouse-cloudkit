//
//  CKFetchDatabaseChangesOperation.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 03/26/2020
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
    /// <summary>
    /// An operation that fetches changes to the database
    /// </summary>
    public class CKFetchDatabaseChangesOperation : CKDatabaseOperation, IDisposable
    {
        #region dll

        // Class Methods
        

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKFetchDatabaseChangesOperation_init(
            out IntPtr exceptionPtr
            );
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKFetchDatabaseChangesOperation_initWithPreviousServerChangeToken(
            IntPtr previousServerChangeToken, 
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchDatabaseChangesOperation_SetPropChangeTokenUpdatedHandler(HandleRef ptr, ChangeTokenUpdatedDelegate changeTokenUpdatedHandler, out IntPtr exceptionPtr);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchDatabaseChangesOperation_SetPropFetchDatabaseChangesCompletionHandler(HandleRef ptr, FetchDatabaseChangesCompletionDelegate fetchDatabaseChangesCompletionHandler, out IntPtr exceptionPtr);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchDatabaseChangesOperation_SetPropRecordZoneWithIDChangedHandler(HandleRef ptr, RecordZoneWithIDChangedDelegate recordZoneWithIDChangedHandler, out IntPtr exceptionPtr);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchDatabaseChangesOperation_SetPropRecordZoneWithIDWasDeletedHandler(HandleRef ptr, RecordZoneWithIDWasDeletedDelegate recordZoneWithIDWasDeletedHandler, out IntPtr exceptionPtr);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchDatabaseChangesOperation_SetPropRecordZoneWithIDWasPurgedHandler(HandleRef ptr, RecordZoneWithIDWasPurgedDelegate recordZoneWithIDWasPurgedHandler, out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKFetchDatabaseChangesOperation_GetPropPreviousServerChangeToken(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchDatabaseChangesOperation_SetPropPreviousServerChangeToken(HandleRef ptr, IntPtr previousServerChangeToken, out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern bool CKFetchDatabaseChangesOperation_GetPropFetchAllChanges(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchDatabaseChangesOperation_SetPropFetchAllChanges(HandleRef ptr, bool fetchAllChanges, out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern ulong CKFetchDatabaseChangesOperation_GetPropResultsLimit(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchDatabaseChangesOperation_SetPropResultsLimit(HandleRef ptr, ulong resultsLimit, out IntPtr exceptionPtr);
        

        #endregion

        internal CKFetchDatabaseChangesOperation(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKFetchDatabaseChangesOperation(
            )
        {
            
            IntPtr ptr = CKFetchDatabaseChangesOperation_init(
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKFetchDatabaseChangesOperation(
            CKServerChangeToken previousServerChangeToken
            )
        {
            
            IntPtr ptr = CKFetchDatabaseChangesOperation_initWithPreviousServerChangeToken(
                previousServerChangeToken != null ? HandleRef.ToIntPtr(previousServerChangeToken.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        
        /// <value>ChangeTokenUpdatedHandler</value>
        public Action<CKServerChangeToken> ChangeTokenUpdatedHandler
        {
            get 
            {
                Action<CKServerChangeToken> value;
                ChangeTokenUpdatedHandlerCallbacks.TryGetValue(HandleRef.ToIntPtr(Handle), out value);
                return value;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    ChangeTokenUpdatedHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    ChangeTokenUpdatedHandlerCallbacks[myPtr] = value;
                }
                CKFetchDatabaseChangesOperation_SetPropChangeTokenUpdatedHandler(Handle, ChangeTokenUpdatedHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKServerChangeToken>> ChangeTokenUpdatedHandlerCallbacks = new Dictionary<IntPtr,Action<CKServerChangeToken>>();

        [MonoPInvokeCallback(typeof(ChangeTokenUpdatedDelegate))]
        private static void ChangeTokenUpdatedHandlerCallback(IntPtr thisPtr, IntPtr _serverChangeToken)
        {
            if(ChangeTokenUpdatedHandlerCallbacks.TryGetValue(thisPtr, out Action<CKServerChangeToken> callback))
            {
                Dispatcher.Instance.EnqueueOnMainThread(() => 
                    callback(
                        _serverChangeToken == IntPtr.Zero ? null : new CKServerChangeToken(_serverChangeToken)));
            }
        }

        
        /// <value>FetchDatabaseChangesCompletionHandler</value>
        public Action<CKServerChangeToken,bool,NSError> FetchDatabaseChangesCompletionHandler
        {
            get 
            {
                Action<CKServerChangeToken,bool,NSError> value;
                FetchDatabaseChangesCompletionHandlerCallbacks.TryGetValue(HandleRef.ToIntPtr(Handle), out value);
                return value;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    FetchDatabaseChangesCompletionHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    FetchDatabaseChangesCompletionHandlerCallbacks[myPtr] = value;
                }
                CKFetchDatabaseChangesOperation_SetPropFetchDatabaseChangesCompletionHandler(Handle, FetchDatabaseChangesCompletionHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKServerChangeToken,bool,NSError>> FetchDatabaseChangesCompletionHandlerCallbacks = new Dictionary<IntPtr,Action<CKServerChangeToken,bool,NSError>>();

        [MonoPInvokeCallback(typeof(FetchDatabaseChangesCompletionDelegate))]
        private static void FetchDatabaseChangesCompletionHandlerCallback(IntPtr thisPtr, IntPtr _serverChangeToken, bool _moreComing, IntPtr _operationError)
        {
            if(FetchDatabaseChangesCompletionHandlerCallbacks.TryGetValue(thisPtr, out Action<CKServerChangeToken,bool,NSError> callback))
            {
                Dispatcher.Instance.EnqueueOnMainThread(() => 
                    callback(
                        _serverChangeToken == IntPtr.Zero ? null : new CKServerChangeToken(_serverChangeToken),
                        _moreComing,
                        _operationError == IntPtr.Zero ? null : new NSError(_operationError)));
            }
        }

        
        /// <value>RecordZoneWithIDChangedHandler</value>
        public Action<CKRecordZoneID> RecordZoneWithIDChangedHandler
        {
            get 
            {
                Action<CKRecordZoneID> value;
                RecordZoneWithIDChangedHandlerCallbacks.TryGetValue(HandleRef.ToIntPtr(Handle), out value);
                return value;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    RecordZoneWithIDChangedHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    RecordZoneWithIDChangedHandlerCallbacks[myPtr] = value;
                }
                CKFetchDatabaseChangesOperation_SetPropRecordZoneWithIDChangedHandler(Handle, RecordZoneWithIDChangedHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKRecordZoneID>> RecordZoneWithIDChangedHandlerCallbacks = new Dictionary<IntPtr,Action<CKRecordZoneID>>();

        [MonoPInvokeCallback(typeof(RecordZoneWithIDChangedDelegate))]
        private static void RecordZoneWithIDChangedHandlerCallback(IntPtr thisPtr, IntPtr _zoneID)
        {
            if(RecordZoneWithIDChangedHandlerCallbacks.TryGetValue(thisPtr, out Action<CKRecordZoneID> callback))
            {
                Dispatcher.Instance.EnqueueOnMainThread(() => 
                    callback(
                        _zoneID == IntPtr.Zero ? null : new CKRecordZoneID(_zoneID)));
            }
        }

        
        /// <value>RecordZoneWithIDWasDeletedHandler</value>
        public Action<CKRecordZoneID> RecordZoneWithIDWasDeletedHandler
        {
            get 
            {
                Action<CKRecordZoneID> value;
                RecordZoneWithIDWasDeletedHandlerCallbacks.TryGetValue(HandleRef.ToIntPtr(Handle), out value);
                return value;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    RecordZoneWithIDWasDeletedHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    RecordZoneWithIDWasDeletedHandlerCallbacks[myPtr] = value;
                }
                CKFetchDatabaseChangesOperation_SetPropRecordZoneWithIDWasDeletedHandler(Handle, RecordZoneWithIDWasDeletedHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKRecordZoneID>> RecordZoneWithIDWasDeletedHandlerCallbacks = new Dictionary<IntPtr,Action<CKRecordZoneID>>();

        [MonoPInvokeCallback(typeof(RecordZoneWithIDWasDeletedDelegate))]
        private static void RecordZoneWithIDWasDeletedHandlerCallback(IntPtr thisPtr, IntPtr _zoneID)
        {
            if(RecordZoneWithIDWasDeletedHandlerCallbacks.TryGetValue(thisPtr, out Action<CKRecordZoneID> callback))
            {
                Dispatcher.Instance.EnqueueOnMainThread(() => 
                    callback(
                        _zoneID == IntPtr.Zero ? null : new CKRecordZoneID(_zoneID)));
            }
        }

        
        /// <value>RecordZoneWithIDWasPurgedHandler</value>
        public Action<CKRecordZoneID> RecordZoneWithIDWasPurgedHandler
        {
            get 
            {
                Action<CKRecordZoneID> value;
                RecordZoneWithIDWasPurgedHandlerCallbacks.TryGetValue(HandleRef.ToIntPtr(Handle), out value);
                return value;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    RecordZoneWithIDWasPurgedHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    RecordZoneWithIDWasPurgedHandlerCallbacks[myPtr] = value;
                }
                CKFetchDatabaseChangesOperation_SetPropRecordZoneWithIDWasPurgedHandler(Handle, RecordZoneWithIDWasPurgedHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKRecordZoneID>> RecordZoneWithIDWasPurgedHandlerCallbacks = new Dictionary<IntPtr,Action<CKRecordZoneID>>();

        [MonoPInvokeCallback(typeof(RecordZoneWithIDWasPurgedDelegate))]
        private static void RecordZoneWithIDWasPurgedHandlerCallback(IntPtr thisPtr, IntPtr _zoneID)
        {
            if(RecordZoneWithIDWasPurgedHandlerCallbacks.TryGetValue(thisPtr, out Action<CKRecordZoneID> callback))
            {
                Dispatcher.Instance.EnqueueOnMainThread(() => 
                    callback(
                        _zoneID == IntPtr.Zero ? null : new CKRecordZoneID(_zoneID)));
            }
        }

        
        /// <value>PreviousServerChangeToken</value>
        public CKServerChangeToken PreviousServerChangeToken
        {
            get 
            { 
                IntPtr previousServerChangeToken = CKFetchDatabaseChangesOperation_GetPropPreviousServerChangeToken(Handle);
                return previousServerChangeToken == IntPtr.Zero ? null : new CKServerChangeToken(previousServerChangeToken);
            }
            set
            {
                CKFetchDatabaseChangesOperation_SetPropPreviousServerChangeToken(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>FetchAllChanges</value>
        public bool FetchAllChanges
        {
            get 
            { 
                bool fetchAllChanges = CKFetchDatabaseChangesOperation_GetPropFetchAllChanges(Handle);
                return fetchAllChanges;
            }
            set
            {
                CKFetchDatabaseChangesOperation_SetPropFetchAllChanges(Handle, value, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>ResultsLimit</value>
        public ulong ResultsLimit
        {
            get 
            { 
                ulong resultsLimit = CKFetchDatabaseChangesOperation_GetPropResultsLimit(Handle);
                return resultsLimit;
            }
            set
            {
                CKFetchDatabaseChangesOperation_SetPropResultsLimit(Handle, value, out IntPtr exceptionPtr);
            }
        }

        

        

        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchDatabaseChangesOperation_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKFetchDatabaseChangesOperation Dispose");
                CKFetchDatabaseChangesOperation_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKFetchDatabaseChangesOperation()
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
