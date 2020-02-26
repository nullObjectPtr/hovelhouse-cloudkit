//
//  CKDatabase.cs
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
    public class CKDatabase : CKObject
    {
        #region dll

        // Class Methods
        

        // Constructors
        

        // Instance Methods
        

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_addOperation(
            HandleRef ptr,
            IntPtr operation);
        
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_fetchRecordWithID_completionHandler(
            HandleRef ptr,
            IntPtr recordID,
            ulong invocationId, CKRecordDelegate completionHandler);
        
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_saveRecord_completionHandler(
            HandleRef ptr,
            IntPtr record,
            ulong invocationId, CKRecordDelegate completionHandler);
        
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_deleteRecordWithID_completionHandler(
            HandleRef ptr,
            IntPtr recordID,
            ulong invocationId, CKRecordIDDelegate completionHandler);
        
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_fetchRecordZoneWithID_completionHandler(
            HandleRef ptr,
            IntPtr zoneID,
            ulong invocationId, CKRecordZoneDelegate completionHandler);
        
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_saveRecordZone_completionHandler(
            HandleRef ptr,
            IntPtr zone,
            ulong invocationId, CKRecordZoneDelegate completionHandler);
        
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_deleteRecordZoneWithID_completionHandler(
            HandleRef ptr,
            IntPtr zoneID,
            ulong invocationId, CKRecordZoneIDDelegate completionHandler);
        
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_fetchSubscriptionWithID_completionHandler(
            HandleRef ptr,
            string subscriptionID,
            ulong invocationId, CKSubscriptionDelegate completionHandler);
        
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_saveSubscription_completionHandler(
            HandleRef ptr,
            IntPtr subscription,
            ulong invocationId, CKSubscriptionDelegate completionHandler);
        
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_deleteSubscriptionWithID_completionHandler(
            HandleRef ptr,
            string subscriptionID,
            ulong invocationId, NSStringDelegate completionHandler);
        
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_fetchAllSubscriptionsWithCompletionHandler(
            HandleRef ptr,
            ulong invocationId, CKSubscriptionArrayDelegate completionHandler);
        
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_fetchAllRecordZonesWithCompletionHandler(
            HandleRef ptr,
            ulong invocationId, CKRecordZoneListDelegate completionHandler);
        
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_performQuery_inZoneWithID_completionHandler(
            HandleRef ptr,
            IntPtr query,
            IntPtr zoneID,
            ulong invocationId, CKRecordListDelegate completionHandler);
        
        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern CKDatabaseScope CKDatabase_GetPropDatabaseScope(HandleRef ptr);
        
        #endregion

        internal CKDatabase(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        #endregion


        #region Methods
        
        
        
        public void AddOperation(
            CKDatabaseOperation operation)
        {
            if(operation == null)
                throw new ArgumentNullException(nameof(operation));
                            
            ;
            CKDatabase_addOperation(
                Handle,
                operation != null ? HandleRef.ToIntPtr(operation.Handle) : IntPtr.Zero);
            
        }
        

        
        
        public void FetchRecordWithID(
            CKRecordID recordID, 
            Action<CKRecord,NSError> completionHandler)
        {
            if(recordID == null)
                throw new ArgumentNullException(nameof(recordID));
                            
            var completionHandlerCall = new InvocationRecord(Handle);
            FetchRecordWithIDCallbacks[completionHandlerCall] = completionHandler;
            CKDatabase_fetchRecordWithID_completionHandler(
                Handle,
                recordID != null ? HandleRef.ToIntPtr(recordID.Handle) : IntPtr.Zero,
                completionHandlerCall.id, FetchRecordWithIDCallback);
            
        }
        
        private static Dictionary<InvocationRecord,Action<CKRecord,NSError>> FetchRecordWithIDCallbacks = new Dictionary<InvocationRecord,Action<CKRecord,NSError>>();

        [MonoPInvokeCallback(typeof(CKRecordDelegate))]
        private static void FetchRecordWithIDCallback(IntPtr thisPtr, ulong invocationId, IntPtr _record, IntPtr _error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = FetchRecordWithIDCallbacks[invocation];
            FetchRecordWithIDCallbacks.Remove(invocation);
            
            try
            {
                callback(
                    _record == IntPtr.Zero ? null : new CKRecord(_record),
                    _error == IntPtr.Zero ? null : new NSError(_error));
            }
            catch(Exception exc)
            {
                Debug.LogError(exc);
            }
        }

        

        
        
        public void SaveRecord(
            CKRecord record, 
            Action<CKRecord,NSError> completionHandler)
        {
            if(record == null)
                throw new ArgumentNullException(nameof(record));
                            
            var completionHandlerCall = new InvocationRecord(Handle);
            SaveRecordCallbacks[completionHandlerCall] = completionHandler;
            CKDatabase_saveRecord_completionHandler(
                Handle,
                record != null ? HandleRef.ToIntPtr(record.Handle) : IntPtr.Zero,
                completionHandlerCall.id, SaveRecordCallback);
            
        }
        
        private static Dictionary<InvocationRecord,Action<CKRecord,NSError>> SaveRecordCallbacks = new Dictionary<InvocationRecord,Action<CKRecord,NSError>>();

        [MonoPInvokeCallback(typeof(CKRecordDelegate))]
        private static void SaveRecordCallback(IntPtr thisPtr, ulong invocationId, IntPtr _record, IntPtr _error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = SaveRecordCallbacks[invocation];
            SaveRecordCallbacks.Remove(invocation);
            
            try
            {
                callback(
                    _record == IntPtr.Zero ? null : new CKRecord(_record),
                    _error == IntPtr.Zero ? null : new NSError(_error));
            }
            catch(Exception exc)
            {
                Debug.LogError(exc);
            }
        }

        

        
        
        public void DeleteRecordWithID(
            CKRecordID recordID, 
            Action<CKRecordID,NSError> completionHandler)
        {
            if(recordID == null)
                throw new ArgumentNullException(nameof(recordID));
                            
            var completionHandlerCall = new InvocationRecord(Handle);
            DeleteRecordWithIDCallbacks[completionHandlerCall] = completionHandler;
            CKDatabase_deleteRecordWithID_completionHandler(
                Handle,
                recordID != null ? HandleRef.ToIntPtr(recordID.Handle) : IntPtr.Zero,
                completionHandlerCall.id, DeleteRecordWithIDCallback);
            
        }
        
        private static Dictionary<InvocationRecord,Action<CKRecordID,NSError>> DeleteRecordWithIDCallbacks = new Dictionary<InvocationRecord,Action<CKRecordID,NSError>>();

        [MonoPInvokeCallback(typeof(CKRecordIDDelegate))]
        private static void DeleteRecordWithIDCallback(IntPtr thisPtr, ulong invocationId, IntPtr _recordID, IntPtr _error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = DeleteRecordWithIDCallbacks[invocation];
            DeleteRecordWithIDCallbacks.Remove(invocation);
            
            try
            {
                callback(
                    _recordID == IntPtr.Zero ? null : new CKRecordID(_recordID),
                    _error == IntPtr.Zero ? null : new NSError(_error));
            }
            catch(Exception exc)
            {
                Debug.LogError(exc);
            }
        }

        

        
        
        public void FetchRecordZoneWithID(
            CKRecordZoneID zoneID, 
            Action<CKRecordZone,NSError> completionHandler)
        {
            if(zoneID == null)
                throw new ArgumentNullException(nameof(zoneID));
                            
            var completionHandlerCall = new InvocationRecord(Handle);
            FetchRecordZoneWithIDCallbacks[completionHandlerCall] = completionHandler;
            CKDatabase_fetchRecordZoneWithID_completionHandler(
                Handle,
                zoneID != null ? HandleRef.ToIntPtr(zoneID.Handle) : IntPtr.Zero,
                completionHandlerCall.id, FetchRecordZoneWithIDCallback);
            
        }
        
        private static Dictionary<InvocationRecord,Action<CKRecordZone,NSError>> FetchRecordZoneWithIDCallbacks = new Dictionary<InvocationRecord,Action<CKRecordZone,NSError>>();

        [MonoPInvokeCallback(typeof(CKRecordZoneDelegate))]
        private static void FetchRecordZoneWithIDCallback(IntPtr thisPtr, ulong invocationId, IntPtr recordZone, IntPtr error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = FetchRecordZoneWithIDCallbacks[invocation];
            FetchRecordZoneWithIDCallbacks.Remove(invocation);
            
            try
            {
                callback(
                    recordZone == IntPtr.Zero ? null : new CKRecordZone(recordZone),
                    error == IntPtr.Zero ? null : new NSError(error));
            }
            catch(Exception exc)
            {
                Debug.LogError(exc);
            }
        }

        

        
        
        public void SaveRecordZone(
            CKRecordZone zone, 
            Action<CKRecordZone,NSError> completionHandler)
        {
            if(zone == null)
                throw new ArgumentNullException(nameof(zone));
                            
            var completionHandlerCall = new InvocationRecord(Handle);
            SaveRecordZoneCallbacks[completionHandlerCall] = completionHandler;
            CKDatabase_saveRecordZone_completionHandler(
                Handle,
                zone != null ? HandleRef.ToIntPtr(zone.Handle) : IntPtr.Zero,
                completionHandlerCall.id, SaveRecordZoneCallback);
            
        }
        
        private static Dictionary<InvocationRecord,Action<CKRecordZone,NSError>> SaveRecordZoneCallbacks = new Dictionary<InvocationRecord,Action<CKRecordZone,NSError>>();

        [MonoPInvokeCallback(typeof(CKRecordZoneDelegate))]
        private static void SaveRecordZoneCallback(IntPtr thisPtr, ulong invocationId, IntPtr recordZone, IntPtr error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = SaveRecordZoneCallbacks[invocation];
            SaveRecordZoneCallbacks.Remove(invocation);
            
            try
            {
                callback(
                    recordZone == IntPtr.Zero ? null : new CKRecordZone(recordZone),
                    error == IntPtr.Zero ? null : new NSError(error));
            }
            catch(Exception exc)
            {
                Debug.LogError(exc);
            }
        }

        

        
        
        public void DeleteRecordZoneWithID(
            CKRecordZoneID zoneID, 
            Action<CKRecordZoneID,NSError> completionHandler)
        {
            if(zoneID == null)
                throw new ArgumentNullException(nameof(zoneID));
                            
            var completionHandlerCall = new InvocationRecord(Handle);
            DeleteRecordZoneWithIDCallbacks[completionHandlerCall] = completionHandler;
            CKDatabase_deleteRecordZoneWithID_completionHandler(
                Handle,
                zoneID != null ? HandleRef.ToIntPtr(zoneID.Handle) : IntPtr.Zero,
                completionHandlerCall.id, DeleteRecordZoneWithIDCallback);
            
        }
        
        private static Dictionary<InvocationRecord,Action<CKRecordZoneID,NSError>> DeleteRecordZoneWithIDCallbacks = new Dictionary<InvocationRecord,Action<CKRecordZoneID,NSError>>();

        [MonoPInvokeCallback(typeof(CKRecordZoneIDDelegate))]
        private static void DeleteRecordZoneWithIDCallback(IntPtr thisPtr, ulong invocationId, IntPtr recordZoneID, IntPtr error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = DeleteRecordZoneWithIDCallbacks[invocation];
            DeleteRecordZoneWithIDCallbacks.Remove(invocation);
            
            try
            {
                callback(
                    recordZoneID == IntPtr.Zero ? null : new CKRecordZoneID(recordZoneID),
                    error == IntPtr.Zero ? null : new NSError(error));
            }
            catch(Exception exc)
            {
                Debug.LogError(exc);
            }
        }

        

        
        
        public void FetchSubscriptionWithID(
            string subscriptionID, 
            Action<CKSubscription,NSError> completionHandler)
        {
            if(subscriptionID == null)
                throw new ArgumentNullException(nameof(subscriptionID));
                            
            var completionHandlerCall = new InvocationRecord(Handle);
            FetchSubscriptionWithIDCallbacks[completionHandlerCall] = completionHandler;
            CKDatabase_fetchSubscriptionWithID_completionHandler(
                Handle,
                subscriptionID,
                completionHandlerCall.id, FetchSubscriptionWithIDCallback);
            
        }
        
        private static Dictionary<InvocationRecord,Action<CKSubscription,NSError>> FetchSubscriptionWithIDCallbacks = new Dictionary<InvocationRecord,Action<CKSubscription,NSError>>();

        [MonoPInvokeCallback(typeof(CKSubscriptionDelegate))]
        private static void FetchSubscriptionWithIDCallback(IntPtr thisPtr, ulong invocationId, IntPtr _subscription, IntPtr _error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = FetchSubscriptionWithIDCallbacks[invocation];
            FetchSubscriptionWithIDCallbacks.Remove(invocation);
            
            try
            {
                callback(
                    _subscription == IntPtr.Zero ? null : new CKSubscription(_subscription),
                    _error == IntPtr.Zero ? null : new NSError(_error));
            }
            catch(Exception exc)
            {
                Debug.LogError(exc);
            }
        }

        

        
        
        public void SaveSubscription(
            CKSubscription subscription, 
            Action<CKSubscription,NSError> completionHandler)
        {
            if(subscription == null)
                throw new ArgumentNullException(nameof(subscription));
                            
            var completionHandlerCall = new InvocationRecord(Handle);
            SaveSubscriptionCallbacks[completionHandlerCall] = completionHandler;
            CKDatabase_saveSubscription_completionHandler(
                Handle,
                subscription != null ? HandleRef.ToIntPtr(subscription.Handle) : IntPtr.Zero,
                completionHandlerCall.id, SaveSubscriptionCallback);
            
        }
        
        private static Dictionary<InvocationRecord,Action<CKSubscription,NSError>> SaveSubscriptionCallbacks = new Dictionary<InvocationRecord,Action<CKSubscription,NSError>>();

        [MonoPInvokeCallback(typeof(CKSubscriptionDelegate))]
        private static void SaveSubscriptionCallback(IntPtr thisPtr, ulong invocationId, IntPtr _subscription, IntPtr _error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = SaveSubscriptionCallbacks[invocation];
            SaveSubscriptionCallbacks.Remove(invocation);
            
            try
            {
                callback(
                    _subscription == IntPtr.Zero ? null : new CKSubscription(_subscription),
                    _error == IntPtr.Zero ? null : new NSError(_error));
            }
            catch(Exception exc)
            {
                Debug.LogError(exc);
            }
        }

        

        
        
        public void DeleteSubscriptionWithID(
            string subscriptionID, 
            Action<string,NSError> completionHandler)
        {
            if(subscriptionID == null)
                throw new ArgumentNullException(nameof(subscriptionID));
                            
            var completionHandlerCall = new InvocationRecord(Handle);
            DeleteSubscriptionWithIDCallbacks[completionHandlerCall] = completionHandler;
            CKDatabase_deleteSubscriptionWithID_completionHandler(
                Handle,
                subscriptionID,
                completionHandlerCall.id, DeleteSubscriptionWithIDCallback);
            
        }
        
        private static Dictionary<InvocationRecord,Action<string,NSError>> DeleteSubscriptionWithIDCallbacks = new Dictionary<InvocationRecord,Action<string,NSError>>();

        [MonoPInvokeCallback(typeof(NSStringDelegate))]
        private static void DeleteSubscriptionWithIDCallback(IntPtr thisPtr, ulong invocationId, IntPtr str, IntPtr error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = DeleteSubscriptionWithIDCallbacks[invocation];
            DeleteSubscriptionWithIDCallbacks.Remove(invocation);
            
            try
            {
                callback(
                    Marshal.PtrToStringAuto(str),
                    error == IntPtr.Zero ? null : new NSError(error));
            }
            catch(Exception exc)
            {
                Debug.LogError(exc);
            }
        }

        

        
        
        public void FetchAllSubscriptionsWithCompletionHandler(
            Action<CKSubscription[],NSError> completionHandler)
        {
                            
            var completionHandlerCall = new InvocationRecord(Handle);
            FetchAllSubscriptionsWithCompletionHandlerCallbacks[completionHandlerCall] = completionHandler;
            CKDatabase_fetchAllSubscriptionsWithCompletionHandler(
                Handle,
                completionHandlerCall.id, FetchAllSubscriptionsWithCompletionHandlerCallback);
            
        }
        
        private static Dictionary<InvocationRecord,Action<CKSubscription[],NSError>> FetchAllSubscriptionsWithCompletionHandlerCallbacks = new Dictionary<InvocationRecord,Action<CKSubscription[],NSError>>();

        [MonoPInvokeCallback(typeof(CKSubscriptionArrayDelegate))]
        private static void FetchAllSubscriptionsWithCompletionHandlerCallback(IntPtr thisPtr, ulong invocationId, IntPtr[] _subscriptionArr,
		long _subscriptionArrCount, IntPtr _error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = FetchAllSubscriptionsWithCompletionHandlerCallbacks[invocation];
            FetchAllSubscriptionsWithCompletionHandlerCallbacks.Remove(invocation);
            
            try
            {
                callback(
                    _subscriptionArr == null ? null : _subscriptionArr.Select(x => new CKSubscription(x)).ToArray(),
                    _error == IntPtr.Zero ? null : new NSError(_error));
            }
            catch(Exception exc)
            {
                Debug.LogError(exc);
            }
        }

        

        
        
        public void FetchAllRecordZonesWithCompletionHandler(
            Action<CKRecordZone[],NSError> completionHandler)
        {
                            
            var completionHandlerCall = new InvocationRecord(Handle);
            FetchAllRecordZonesWithCompletionHandlerCallbacks[completionHandlerCall] = completionHandler;
            CKDatabase_fetchAllRecordZonesWithCompletionHandler(
                Handle,
                completionHandlerCall.id, FetchAllRecordZonesWithCompletionHandlerCallback);
            
        }
        
        private static Dictionary<InvocationRecord,Action<CKRecordZone[],NSError>> FetchAllRecordZonesWithCompletionHandlerCallbacks = new Dictionary<InvocationRecord,Action<CKRecordZone[],NSError>>();

        [MonoPInvokeCallback(typeof(CKRecordZoneListDelegate))]
        private static void FetchAllRecordZonesWithCompletionHandlerCallback(IntPtr thisPtr, ulong invocationId, IntPtr[] _recordZones,
		long _recordZonesCount, IntPtr _error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = FetchAllRecordZonesWithCompletionHandlerCallbacks[invocation];
            FetchAllRecordZonesWithCompletionHandlerCallbacks.Remove(invocation);
            
            try
            {
                callback(
                    _recordZones == null ? null : _recordZones.Select(x => new CKRecordZone(x)).ToArray(),
                    _error == IntPtr.Zero ? null : new NSError(_error));
            }
            catch(Exception exc)
            {
                Debug.LogError(exc);
            }
        }

        

        
        
        public void PerformQuery(
            CKQuery query, 
            CKRecordZoneID zoneID, 
            Action<CKRecord[],NSError> completionHandler)
        {
            if(query == null)
                throw new ArgumentNullException(nameof(query));
                            
            var completionHandlerCall = new InvocationRecord(Handle);
            PerformQueryCallbacks[completionHandlerCall] = completionHandler;
            CKDatabase_performQuery_inZoneWithID_completionHandler(
                Handle,
                query != null ? HandleRef.ToIntPtr(query.Handle) : IntPtr.Zero,
                zoneID != null ? HandleRef.ToIntPtr(zoneID.Handle) : IntPtr.Zero,
                completionHandlerCall.id, PerformQueryCallback);
            
        }
        
        private static Dictionary<InvocationRecord,Action<CKRecord[],NSError>> PerformQueryCallbacks = new Dictionary<InvocationRecord,Action<CKRecord[],NSError>>();

        [MonoPInvokeCallback(typeof(CKRecordListDelegate))]
        private static void PerformQueryCallback(IntPtr thisPtr, ulong invocationId, IntPtr[] _recordID,
		long _recordIDCount, IntPtr _error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = PerformQueryCallbacks[invocation];
            PerformQueryCallbacks.Remove(invocation);
            
            try
            {
                callback(
                    _recordID == null ? null : _recordID.Select(x => new CKRecord(x)).ToArray(),
                    _error == IntPtr.Zero ? null : new NSError(_error));
            }
            catch(Exception exc)
            {
                Debug.LogError(exc);
            }
        }

        

        
        #endregion

        #region Properties
        
        public CKDatabaseScope DatabaseScope 
        {
            get 
            { 
                CKDatabaseScope databaseScope = CKDatabase_GetPropDatabaseScope(Handle);
                return databaseScope;
            }
        }
        
        #endregion
    }
}
