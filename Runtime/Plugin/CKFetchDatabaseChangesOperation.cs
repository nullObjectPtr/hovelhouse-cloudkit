//
//  CKFetchDatabaseChangesOperation.cs
//
//  Created by Jonathan on 02/25/2020
//  Copyright © 2020 HovelHouseApps. All rights reserved.
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
    public class CKFetchDatabaseChangesOperation : CKObject
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKFetchDatabaseChangesOperation_init();
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKFetchDatabaseChangesOperation_initWithPreviousServerChangeToken(
            IntPtr previousServerChangeToken);
        

        // Instance Methods
        

        

        // Properties
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchDatabaseChangesOperation_SetPropChangeTokenUpdatedHandler(HandleRef ptr, ChangeTokenUpdatedDelegate changeTokenUpdatedHandler);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchDatabaseChangesOperation_SetPropFetchDatabaseChangesCompletionHandler(HandleRef ptr, FetchDatabaseChangesCompletionDelegate fetchDatabaseChangesCompletionHandler);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchDatabaseChangesOperation_SetPropRecordZoneWithIDChangedHandler(HandleRef ptr, RecordZoneWithIDChangedDelegate recordZoneWithIDChangedHandler);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchDatabaseChangesOperation_SetPropRecordZoneWithIDWasDeletedHandler(HandleRef ptr, RecordZoneWithIDWasDeletedDelegate recordZoneWithIDWasDeletedHandler);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchDatabaseChangesOperation_SetPropRecordZoneWithIDWasPurgedHandler(HandleRef ptr, RecordZoneWithIDWasPurgedDelegate recordZoneWithIDWasPurgedHandler);
        
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
        private static extern void CKFetchDatabaseChangesOperation_SetPropPreviousServerChangeToken(HandleRef ptr, IntPtr previousServerChangeToken);
        
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
        private static extern void CKFetchDatabaseChangesOperation_SetPropFetchAllChanges(HandleRef ptr, bool fetchAllChanges);
        
        #endregion

        internal CKFetchDatabaseChangesOperation(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKFetchDatabaseChangesOperation init(
        ){
            
            IntPtr ptr = CKFetchDatabaseChangesOperation_init();
            return new CKFetchDatabaseChangesOperation(ptr);
        }
        
        
        public static CKFetchDatabaseChangesOperation initWithPreviousServerChangeToken(
            CKServerChangeToken previousServerChangeToken
        ){
            
            IntPtr ptr = CKFetchDatabaseChangesOperation_initWithPreviousServerChangeToken(
                previousServerChangeToken != null ? HandleRef.ToIntPtr(previousServerChangeToken.Handle) : IntPtr.Zero);
            return new CKFetchDatabaseChangesOperation(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
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
                CKFetchDatabaseChangesOperation_SetPropChangeTokenUpdatedHandler(Handle, ChangeTokenUpdatedHandlerCallback);
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKServerChangeToken>> ChangeTokenUpdatedHandlerCallbacks = new Dictionary<IntPtr,Action<CKServerChangeToken>>();

        [MonoPInvokeCallback(typeof(ChangeTokenUpdatedDelegate))]
        private static void ChangeTokenUpdatedHandlerCallback(IntPtr thisPtr, IntPtr _serverChangeToken)
        {
            if(ChangeTokenUpdatedHandlerCallbacks.TryGetValue(thisPtr, out Action<CKServerChangeToken> callback))
            {
                try
                {
                    callback(
                        _serverChangeToken == IntPtr.Zero ? null : new CKServerChangeToken(_serverChangeToken));
                }
                catch(Exception exc)
                {
                    Debug.LogError(exc);
                }
            }
        }

        
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
                CKFetchDatabaseChangesOperation_SetPropFetchDatabaseChangesCompletionHandler(Handle, FetchDatabaseChangesCompletionHandlerCallback);
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKServerChangeToken,bool,NSError>> FetchDatabaseChangesCompletionHandlerCallbacks = new Dictionary<IntPtr,Action<CKServerChangeToken,bool,NSError>>();

        [MonoPInvokeCallback(typeof(FetchDatabaseChangesCompletionDelegate))]
        private static void FetchDatabaseChangesCompletionHandlerCallback(IntPtr thisPtr, IntPtr _serverChangeToken, bool _moreComing, IntPtr _operationError)
        {
            if(FetchDatabaseChangesCompletionHandlerCallbacks.TryGetValue(thisPtr, out Action<CKServerChangeToken,bool,NSError> callback))
            {
                try
                {
                    callback(
                        _serverChangeToken == IntPtr.Zero ? null : new CKServerChangeToken(_serverChangeToken),
                        _moreComing,
                        _operationError == IntPtr.Zero ? null : new NSError(_operationError));
                }
                catch(Exception exc)
                {
                    Debug.LogError(exc);
                }
            }
        }

        
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
                CKFetchDatabaseChangesOperation_SetPropRecordZoneWithIDChangedHandler(Handle, RecordZoneWithIDChangedHandlerCallback);
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKRecordZoneID>> RecordZoneWithIDChangedHandlerCallbacks = new Dictionary<IntPtr,Action<CKRecordZoneID>>();

        [MonoPInvokeCallback(typeof(RecordZoneWithIDChangedDelegate))]
        private static void RecordZoneWithIDChangedHandlerCallback(IntPtr thisPtr, IntPtr _zoneID)
        {
            if(RecordZoneWithIDChangedHandlerCallbacks.TryGetValue(thisPtr, out Action<CKRecordZoneID> callback))
            {
                try
                {
                    callback(
                        _zoneID == IntPtr.Zero ? null : new CKRecordZoneID(_zoneID));
                }
                catch(Exception exc)
                {
                    Debug.LogError(exc);
                }
            }
        }

        
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
                CKFetchDatabaseChangesOperation_SetPropRecordZoneWithIDWasDeletedHandler(Handle, RecordZoneWithIDWasDeletedHandlerCallback);
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKRecordZoneID>> RecordZoneWithIDWasDeletedHandlerCallbacks = new Dictionary<IntPtr,Action<CKRecordZoneID>>();

        [MonoPInvokeCallback(typeof(RecordZoneWithIDWasDeletedDelegate))]
        private static void RecordZoneWithIDWasDeletedHandlerCallback(IntPtr thisPtr, IntPtr _zoneID)
        {
            if(RecordZoneWithIDWasDeletedHandlerCallbacks.TryGetValue(thisPtr, out Action<CKRecordZoneID> callback))
            {
                try
                {
                    callback(
                        _zoneID == IntPtr.Zero ? null : new CKRecordZoneID(_zoneID));
                }
                catch(Exception exc)
                {
                    Debug.LogError(exc);
                }
            }
        }

        
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
                CKFetchDatabaseChangesOperation_SetPropRecordZoneWithIDWasPurgedHandler(Handle, RecordZoneWithIDWasPurgedHandlerCallback);
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKRecordZoneID>> RecordZoneWithIDWasPurgedHandlerCallbacks = new Dictionary<IntPtr,Action<CKRecordZoneID>>();

        [MonoPInvokeCallback(typeof(RecordZoneWithIDWasPurgedDelegate))]
        private static void RecordZoneWithIDWasPurgedHandlerCallback(IntPtr thisPtr, IntPtr _zoneID)
        {
            if(RecordZoneWithIDWasPurgedHandlerCallbacks.TryGetValue(thisPtr, out Action<CKRecordZoneID> callback))
            {
                try
                {
                    callback(
                        _zoneID == IntPtr.Zero ? null : new CKRecordZoneID(_zoneID));
                }
                catch(Exception exc)
                {
                    Debug.LogError(exc);
                }
            }
        }

        
        public CKServerChangeToken PreviousServerChangeToken 
        {
            get 
            { 
                IntPtr previousServerChangeToken = CKFetchDatabaseChangesOperation_GetPropPreviousServerChangeToken(Handle);
                return previousServerChangeToken == IntPtr.Zero ? null : new CKServerChangeToken(previousServerChangeToken);
            }
            set
            {
                CKFetchDatabaseChangesOperation_SetPropPreviousServerChangeToken(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero);
            }
        }
        
        public bool FetchAllChanges 
        {
            get 
            { 
                bool fetchAllChanges = CKFetchDatabaseChangesOperation_GetPropFetchAllChanges(Handle);
                return fetchAllChanges;
            }
            set
            {
                CKFetchDatabaseChangesOperation_SetPropFetchAllChanges(Handle, value);
            }
        }
        
        #endregion
    }
}
