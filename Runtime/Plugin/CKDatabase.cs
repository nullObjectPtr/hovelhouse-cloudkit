//
//  CKDatabase.cs
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
    /// An instance of (one of) your apps databases
    /// </summary>
    /// <remarks>
    /// Each app has access to two databases. One public and one private. A public database stores information that can be made publically writable or readable by you (the app developer) or users in large. Data stored in the public database counts toward to your app-quota, and you (the developer) may be charged for it&apos;s usage. The private database counts toward the current iCloud users quota and you will not be charged for it. The private database may not be a reliable way to store information if the user has no iCloud storage space left. If you are coming from other databases such as Mongo or MySQL, You do not &apos;connect&apos; to these databases. CloudKit is a transport technology, and it handles communication with the Cloud for you
    /// </remarks>
    public class CKDatabase : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        

        

        

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_addOperation(
            HandleRef ptr, 
            IntPtr operation,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_fetchRecordWithID_completionHandler(
            HandleRef ptr, 
            IntPtr recordID,
            ulong invocationId, CKRecordDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_saveRecord_completionHandler(
            HandleRef ptr, 
            IntPtr record,
            ulong invocationId, CKRecordDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_deleteRecordWithID_completionHandler(
            HandleRef ptr, 
            IntPtr recordID,
            ulong invocationId, CKRecordIDDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_fetchRecordZoneWithID_completionHandler(
            HandleRef ptr, 
            IntPtr zoneID,
            ulong invocationId, CKRecordZoneDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_saveRecordZone_completionHandler(
            HandleRef ptr, 
            IntPtr zone,
            ulong invocationId, CKRecordZoneDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_deleteRecordZoneWithID_completionHandler(
            HandleRef ptr, 
            IntPtr zoneID,
            ulong invocationId, CKRecordZoneIDDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_fetchSubscriptionWithID_completionHandler(
            HandleRef ptr, 
            string subscriptionID,
            ulong invocationId, CKSubscriptionDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_saveSubscription_completionHandler(
            HandleRef ptr, 
            IntPtr subscription,
            ulong invocationId, CKSubscriptionDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_deleteSubscriptionWithID_completionHandler(
            HandleRef ptr, 
            string subscriptionID,
            ulong invocationId, NSStringDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_fetchAllSubscriptionsWithCompletionHandler(
            HandleRef ptr, 
            ulong invocationId, CKSubscriptionArrayDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_fetchAllRecordZonesWithCompletionHandler(
            HandleRef ptr, 
            ulong invocationId, CKRecordZoneListDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_performQuery_inZoneWithID_completionHandler(
            HandleRef ptr, 
            IntPtr query,
            IntPtr zoneID,
            ulong invocationId, CKRecordListDelegate completionHandler,
            out IntPtr exceptionPtr);
        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern CKDatabaseScope CKDatabase_GetPropDatabaseScope(HandleRef ptr);
        

        #endregion

        internal CKDatabase(IntPtr ptr) : base(ptr) {}
        
        
        
        


        
        
        /// <summary>
        /// </summary>
        /// <param name="operation"></param>
        /// <returns>void</returns>
        public void AddOperation(
            CKDatabaseOperation operation)
        { 
            if(operation == null)
                throw new ArgumentNullException(nameof(operation));
            
            CKDatabase_addOperation(
                Handle, 
                operation != null ? HandleRef.ToIntPtr(operation.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="recordID"></param><param name="completionHandler"></param>
        /// <returns>void</returns>
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
                
                completionHandlerCall.id, FetchRecordWithIDCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKRecord,NSError>> FetchRecordWithIDCallbacks = new Dictionary<InvocationRecord,Action<CKRecord,NSError>>();

        [MonoPInvokeCallback(typeof(CKRecordDelegate))]
        private static void FetchRecordWithIDCallback(IntPtr thisPtr, ulong invocationId, IntPtr _record, IntPtr _error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = FetchRecordWithIDCallbacks[invocation];
            FetchRecordWithIDCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    _record == IntPtr.Zero ? null : new CKRecord(_record),
                    _error == IntPtr.Zero ? null : new NSError(_error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="record"></param><param name="completionHandler"></param>
        /// <returns>void</returns>
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
                
                completionHandlerCall.id, SaveRecordCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKRecord,NSError>> SaveRecordCallbacks = new Dictionary<InvocationRecord,Action<CKRecord,NSError>>();

        [MonoPInvokeCallback(typeof(CKRecordDelegate))]
        private static void SaveRecordCallback(IntPtr thisPtr, ulong invocationId, IntPtr _record, IntPtr _error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = SaveRecordCallbacks[invocation];
            SaveRecordCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    _record == IntPtr.Zero ? null : new CKRecord(_record),
                    _error == IntPtr.Zero ? null : new NSError(_error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="recordID"></param><param name="completionHandler"></param>
        /// <returns>void</returns>
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
                
                completionHandlerCall.id, DeleteRecordWithIDCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKRecordID,NSError>> DeleteRecordWithIDCallbacks = new Dictionary<InvocationRecord,Action<CKRecordID,NSError>>();

        [MonoPInvokeCallback(typeof(CKRecordIDDelegate))]
        private static void DeleteRecordWithIDCallback(IntPtr thisPtr, ulong invocationId, IntPtr _recordID, IntPtr _error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = DeleteRecordWithIDCallbacks[invocation];
            DeleteRecordWithIDCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    _recordID == IntPtr.Zero ? null : new CKRecordID(_recordID),
                    _error == IntPtr.Zero ? null : new NSError(_error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="zoneID"></param><param name="completionHandler"></param>
        /// <returns>void</returns>
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
                
                completionHandlerCall.id, FetchRecordZoneWithIDCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKRecordZone,NSError>> FetchRecordZoneWithIDCallbacks = new Dictionary<InvocationRecord,Action<CKRecordZone,NSError>>();

        [MonoPInvokeCallback(typeof(CKRecordZoneDelegate))]
        private static void FetchRecordZoneWithIDCallback(IntPtr thisPtr, ulong invocationId, IntPtr recordZone, IntPtr error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = FetchRecordZoneWithIDCallbacks[invocation];
            FetchRecordZoneWithIDCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    recordZone == IntPtr.Zero ? null : new CKRecordZone(recordZone),
                    error == IntPtr.Zero ? null : new NSError(error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="zone"></param><param name="completionHandler"></param>
        /// <returns>void</returns>
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
                
                completionHandlerCall.id, SaveRecordZoneCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKRecordZone,NSError>> SaveRecordZoneCallbacks = new Dictionary<InvocationRecord,Action<CKRecordZone,NSError>>();

        [MonoPInvokeCallback(typeof(CKRecordZoneDelegate))]
        private static void SaveRecordZoneCallback(IntPtr thisPtr, ulong invocationId, IntPtr recordZone, IntPtr error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = SaveRecordZoneCallbacks[invocation];
            SaveRecordZoneCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    recordZone == IntPtr.Zero ? null : new CKRecordZone(recordZone),
                    error == IntPtr.Zero ? null : new NSError(error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="zoneID"></param><param name="completionHandler"></param>
        /// <returns>void</returns>
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
                
                completionHandlerCall.id, DeleteRecordZoneWithIDCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKRecordZoneID,NSError>> DeleteRecordZoneWithIDCallbacks = new Dictionary<InvocationRecord,Action<CKRecordZoneID,NSError>>();

        [MonoPInvokeCallback(typeof(CKRecordZoneIDDelegate))]
        private static void DeleteRecordZoneWithIDCallback(IntPtr thisPtr, ulong invocationId, IntPtr recordZoneID, IntPtr error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = DeleteRecordZoneWithIDCallbacks[invocation];
            DeleteRecordZoneWithIDCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    recordZoneID == IntPtr.Zero ? null : new CKRecordZoneID(recordZoneID),
                    error == IntPtr.Zero ? null : new NSError(error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="subscriptionID"></param><param name="completionHandler"></param>
        /// <returns>void</returns>
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
                
                completionHandlerCall.id, FetchSubscriptionWithIDCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKSubscription,NSError>> FetchSubscriptionWithIDCallbacks = new Dictionary<InvocationRecord,Action<CKSubscription,NSError>>();

        [MonoPInvokeCallback(typeof(CKSubscriptionDelegate))]
        private static void FetchSubscriptionWithIDCallback(IntPtr thisPtr, ulong invocationId, IntPtr _subscription, IntPtr _error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = FetchSubscriptionWithIDCallbacks[invocation];
            FetchSubscriptionWithIDCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    _subscription == IntPtr.Zero ? null : new CKSubscription(_subscription),
                    _error == IntPtr.Zero ? null : new NSError(_error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="subscription"></param><param name="completionHandler"></param>
        /// <returns>void</returns>
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
                
                completionHandlerCall.id, SaveSubscriptionCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKSubscription,NSError>> SaveSubscriptionCallbacks = new Dictionary<InvocationRecord,Action<CKSubscription,NSError>>();

        [MonoPInvokeCallback(typeof(CKSubscriptionDelegate))]
        private static void SaveSubscriptionCallback(IntPtr thisPtr, ulong invocationId, IntPtr _subscription, IntPtr _error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = SaveSubscriptionCallbacks[invocation];
            SaveSubscriptionCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    _subscription == IntPtr.Zero ? null : new CKSubscription(_subscription),
                    _error == IntPtr.Zero ? null : new NSError(_error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="subscriptionID"></param><param name="completionHandler"></param>
        /// <returns>void</returns>
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
                
                completionHandlerCall.id, DeleteSubscriptionWithIDCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<string,NSError>> DeleteSubscriptionWithIDCallbacks = new Dictionary<InvocationRecord,Action<string,NSError>>();

        [MonoPInvokeCallback(typeof(NSStringDelegate))]
        private static void DeleteSubscriptionWithIDCallback(IntPtr thisPtr, ulong invocationId, IntPtr str, IntPtr error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = DeleteSubscriptionWithIDCallbacks[invocation];
            DeleteSubscriptionWithIDCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    Marshal.PtrToStringAuto(str),
                    error == IntPtr.Zero ? null : new NSError(error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="completionHandler"></param>
        /// <returns>void</returns>
        public void FetchAllSubscriptionsWithCompletionHandler(
            Action<CKSubscription[],NSError> completionHandler)
        { 
            
            var completionHandlerCall = new InvocationRecord(Handle);
            FetchAllSubscriptionsWithCompletionHandlerCallbacks[completionHandlerCall] = completionHandler;
            
            CKDatabase_fetchAllSubscriptionsWithCompletionHandler(
                Handle, 
                completionHandlerCall.id, FetchAllSubscriptionsWithCompletionHandlerCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKSubscription[],NSError>> FetchAllSubscriptionsWithCompletionHandlerCallbacks = new Dictionary<InvocationRecord,Action<CKSubscription[],NSError>>();

        [MonoPInvokeCallback(typeof(CKSubscriptionArrayDelegate))]
        private static void FetchAllSubscriptionsWithCompletionHandlerCallback(IntPtr thisPtr, ulong invocationId, IntPtr[] _subscriptionArr,
		long _subscriptionArrCount, IntPtr _error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = FetchAllSubscriptionsWithCompletionHandlerCallbacks[invocation];
            FetchAllSubscriptionsWithCompletionHandlerCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    _subscriptionArr == null ? null : _subscriptionArr.Select(x => new CKSubscription(x)).ToArray(),
                    _error == IntPtr.Zero ? null : new NSError(_error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="completionHandler"></param>
        /// <returns>void</returns>
        public void FetchAllRecordZonesWithCompletionHandler(
            Action<CKRecordZone[],NSError> completionHandler)
        { 
            
            var completionHandlerCall = new InvocationRecord(Handle);
            FetchAllRecordZonesWithCompletionHandlerCallbacks[completionHandlerCall] = completionHandler;
            
            CKDatabase_fetchAllRecordZonesWithCompletionHandler(
                Handle, 
                completionHandlerCall.id, FetchAllRecordZonesWithCompletionHandlerCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKRecordZone[],NSError>> FetchAllRecordZonesWithCompletionHandlerCallbacks = new Dictionary<InvocationRecord,Action<CKRecordZone[],NSError>>();

        [MonoPInvokeCallback(typeof(CKRecordZoneListDelegate))]
        private static void FetchAllRecordZonesWithCompletionHandlerCallback(IntPtr thisPtr, ulong invocationId, IntPtr[] _recordZones,
		long _recordZonesCount, IntPtr _error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = FetchAllRecordZonesWithCompletionHandlerCallbacks[invocation];
            FetchAllRecordZonesWithCompletionHandlerCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    _recordZones == null ? null : _recordZones.Select(x => new CKRecordZone(x)).ToArray(),
                    _error == IntPtr.Zero ? null : new NSError(_error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="query"></param><param name="zoneID"></param><param name="completionHandler"></param>
        /// <returns>void</returns>
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
                
                completionHandlerCall.id, PerformQueryCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKRecord[],NSError>> PerformQueryCallbacks = new Dictionary<InvocationRecord,Action<CKRecord[],NSError>>();

        [MonoPInvokeCallback(typeof(CKRecordListDelegate))]
        private static void PerformQueryCallback(IntPtr thisPtr, ulong invocationId, IntPtr[] _recordID,
		long _recordIDCount, IntPtr _error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = PerformQueryCallbacks[invocation];
            PerformQueryCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    _recordID == null ? null : _recordID.Select(x => new CKRecord(x)).ToArray(),
                    _error == IntPtr.Zero ? null : new NSError(_error)));
        }

        

        
        
        /// <value>DatabaseScope</value>
        public CKDatabaseScope DatabaseScope
        {
            get 
            { 
                CKDatabaseScope databaseScope = CKDatabase_GetPropDatabaseScope(Handle);
                return databaseScope;
            }
        }

        

        

        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDatabase_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKDatabase Dispose");
                CKDatabase_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKDatabase()
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
