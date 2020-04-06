//
//  CKContainer.cs
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
    /// An interface into the applications sandboxed contents
    /// </summary>
    /// <remarks>
    /// A CKContainer represents an app-sandbox of sorts. In addition to sandboxing application data, a container also sandboxes communication with an applications public and private cloud databases. You can set up custom containers that can be shared between different apps. Containers are worth reading up on - they are not very straightforward.
    /// </remarks>
    public class CKContainer : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKContainer_defaultContainer(
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKContainer_containerWithIdentifier(
            string containerIdentifier,
            out IntPtr exceptionPtr);
        

        

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKContainer_databaseWithDatabaseScope(
            HandleRef ptr, 
            long databaseScope,
            out IntPtr exceptionPtr);
        

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKContainer_fetchAllLongLivedOperationIDsWithCompletionHandler(
            HandleRef ptr, 
            ulong invocationId, CKLongLivedOperationIdsDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKContainer_fetchUserRecordIDWithCompletionHandler(
            HandleRef ptr, 
            ulong invocationId, CKRecordIDDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKContainer_discoverUserIdentityWithEmailAddress_completionHandler(
            HandleRef ptr, 
            string email,
            ulong invocationId, UserIdentityDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKContainer_fetchShareParticipantWithEmailAddress_completionHandler(
            HandleRef ptr, 
            string emailAddress,
            ulong invocationId, CKShareParticipantDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKContainer_fetchShareParticipantWithPhoneNumber_completionHandler(
            HandleRef ptr, 
            string phoneNumber,
            ulong invocationId, CKShareParticipantDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKContainer_fetchShareParticipantWithUserRecordID_completionHandler(
            HandleRef ptr, 
            IntPtr userRecordID,
            ulong invocationId, CKShareParticipantDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKContainer_fetchLongLivedOperationWithID_completionHandler(
            HandleRef ptr, 
            string operationID,
            ulong invocationId, CKLongLivedOperationDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKContainer_acceptShareMetadata_completionHandler(
            HandleRef ptr, 
            IntPtr metadata,
            ulong invocationId, CKShareDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKContainer_requestApplicationPermission_completionHandler(
            HandleRef ptr, 
            long applicationPermission,
            ulong invocationId, CKApplicationPermissionsDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKContainer_accountStatusWithCompletionHandler(
            HandleRef ptr, 
            ulong invocationId, CKAccountStatusDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKContainer_statusForApplicationPermission_completionHandler(
            HandleRef ptr, 
            long applicationPermission,
            ulong invocationId, CKApplicationPermissionsDelegate completionHandler,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKContainer_addOperation(
            HandleRef ptr, 
            IntPtr operation,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKContainer_fetchShareMetadataWithURL_completionHandler(
            HandleRef ptr, 
            IntPtr url,
            ulong invocationId, CKShareMetadataDelegate completionHandler,
            out IntPtr exceptionPtr);
        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKContainer_GetPropPrivateCloudDatabase(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKContainer_GetPropPublicCloudDatabase(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKContainer_GetPropSharedCloudDatabase(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKContainer_GetPropContainerIdentifier(HandleRef ptr);
        

        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr AddCKAccountChangedNotificationObserver(NotificationDelegate handler, ref IntPtr exceptionPtr);

        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void RemoveCKAccountChangedNotificationObserver(HandleRef observerHandle, ref IntPtr exceptionPtr);
        

        #endregion

        internal CKContainer(IntPtr ptr) : base(ptr) {}
        
        
        /// <summary>
        /// </summary>
        /// 
        /// <returns>val</returns>
        public static CKContainer DefaultContainer()
        { 
            var val = CKContainer_defaultContainer(out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val == IntPtr.Zero ? null : new CKContainer(val);
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="containerIdentifier"></param>
        /// <returns>val</returns>
        public static CKContainer ContainerWithIdentifier(
            string containerIdentifier)
        { 
            if(containerIdentifier == null)
                throw new ArgumentNullException(nameof(containerIdentifier));
            
            var val = CKContainer_containerWithIdentifier(
                containerIdentifier, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val == IntPtr.Zero ? null : new CKContainer(val);
        }
        

        
        
        


        
        /// <summary>
        /// </summary>
        /// <param name="databaseScope"></param>
        /// <returns>val</returns>
        public CKDatabase DatabaseWithDatabaseScope(
            CKDatabaseScope databaseScope)
        { 
            
            var val = CKContainer_databaseWithDatabaseScope(
                Handle, 
                (long) databaseScope, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val == IntPtr.Zero ? null : new CKDatabase(val);
        }
        

        
        
        /// <summary>
        /// </summary>
        /// <param name="completionHandler"></param>
        /// <returns>void</returns>
        public void FetchAllLongLivedOperationIDsWithCompletionHandler(
            Action<string[],NSError> completionHandler)
        { 
            
            var completionHandlerCall = new InvocationRecord(Handle);
            FetchAllLongLivedOperationIDsWithCompletionHandlerCallbacks[completionHandlerCall] = completionHandler;
            
            CKContainer_fetchAllLongLivedOperationIDsWithCompletionHandler(
                Handle, 
                completionHandlerCall.id, FetchAllLongLivedOperationIDsWithCompletionHandlerCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<string[],NSError>> FetchAllLongLivedOperationIDsWithCompletionHandlerCallbacks = new Dictionary<InvocationRecord,Action<string[],NSError>>();

        [MonoPInvokeCallback(typeof(CKLongLivedOperationIdsDelegate))]
        private static void FetchAllLongLivedOperationIDsWithCompletionHandlerCallback(IntPtr thisPtr, ulong invocationId, IntPtr[] outstandingOperationIDs,
		long outstandingOperationIDsCount, IntPtr error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = FetchAllLongLivedOperationIDsWithCompletionHandlerCallbacks[invocation];
            FetchAllLongLivedOperationIDsWithCompletionHandlerCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    outstandingOperationIDs == null ? null : outstandingOperationIDs.Select(x => Marshal.PtrToStringAuto(x)).ToArray(),
                    error == IntPtr.Zero ? null : new NSError(error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="completionHandler"></param>
        /// <returns>void</returns>
        public void FetchUserRecordIDWithCompletionHandler(
            Action<CKRecordID,NSError> completionHandler)
        { 
            
            var completionHandlerCall = new InvocationRecord(Handle);
            FetchUserRecordIDWithCompletionHandlerCallbacks[completionHandlerCall] = completionHandler;
            
            CKContainer_fetchUserRecordIDWithCompletionHandler(
                Handle, 
                completionHandlerCall.id, FetchUserRecordIDWithCompletionHandlerCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKRecordID,NSError>> FetchUserRecordIDWithCompletionHandlerCallbacks = new Dictionary<InvocationRecord,Action<CKRecordID,NSError>>();

        [MonoPInvokeCallback(typeof(CKRecordIDDelegate))]
        private static void FetchUserRecordIDWithCompletionHandlerCallback(IntPtr thisPtr, ulong invocationId, IntPtr _recordID, IntPtr _error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = FetchUserRecordIDWithCompletionHandlerCallbacks[invocation];
            FetchUserRecordIDWithCompletionHandlerCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    _recordID == IntPtr.Zero ? null : new CKRecordID(_recordID),
                    _error == IntPtr.Zero ? null : new NSError(_error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="email"></param><param name="completionHandler"></param>
        /// <returns>void</returns>
        public void DiscoverUserIdentityWithEmailAddress(
            string email, 
            Action<CKUserIdentity,NSError> completionHandler)
        { 
            if(email == null)
                throw new ArgumentNullException(nameof(email));
            
            
            var completionHandlerCall = new InvocationRecord(Handle);
            DiscoverUserIdentityWithEmailAddressCallbacks[completionHandlerCall] = completionHandler;
            
            CKContainer_discoverUserIdentityWithEmailAddress_completionHandler(
                Handle, 
                email, 
                
                completionHandlerCall.id, DiscoverUserIdentityWithEmailAddressCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKUserIdentity,NSError>> DiscoverUserIdentityWithEmailAddressCallbacks = new Dictionary<InvocationRecord,Action<CKUserIdentity,NSError>>();

        [MonoPInvokeCallback(typeof(UserIdentityDelegate))]
        private static void DiscoverUserIdentityWithEmailAddressCallback(IntPtr thisPtr, ulong invocationId, IntPtr userIdentity, IntPtr error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = DiscoverUserIdentityWithEmailAddressCallbacks[invocation];
            DiscoverUserIdentityWithEmailAddressCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    userIdentity == IntPtr.Zero ? null : new CKUserIdentity(userIdentity),
                    error == IntPtr.Zero ? null : new NSError(error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="emailAddress"></param><param name="completionHandler"></param>
        /// <returns>void</returns>
        public void FetchShareParticipantWithEmailAddress(
            string emailAddress, 
            Action<CKShareParticipant,NSError> completionHandler)
        { 
            if(emailAddress == null)
                throw new ArgumentNullException(nameof(emailAddress));
            
            
            var completionHandlerCall = new InvocationRecord(Handle);
            FetchShareParticipantWithEmailAddressCallbacks[completionHandlerCall] = completionHandler;
            
            CKContainer_fetchShareParticipantWithEmailAddress_completionHandler(
                Handle, 
                emailAddress, 
                
                completionHandlerCall.id, FetchShareParticipantWithEmailAddressCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKShareParticipant,NSError>> FetchShareParticipantWithEmailAddressCallbacks = new Dictionary<InvocationRecord,Action<CKShareParticipant,NSError>>();

        [MonoPInvokeCallback(typeof(CKShareParticipantDelegate))]
        private static void FetchShareParticipantWithEmailAddressCallback(IntPtr thisPtr, ulong invocationId, IntPtr shareParticipant, IntPtr error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = FetchShareParticipantWithEmailAddressCallbacks[invocation];
            FetchShareParticipantWithEmailAddressCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    shareParticipant == IntPtr.Zero ? null : new CKShareParticipant(shareParticipant),
                    error == IntPtr.Zero ? null : new NSError(error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="phoneNumber"></param><param name="completionHandler"></param>
        /// <returns>void</returns>
        public void FetchShareParticipantWithPhoneNumber(
            string phoneNumber, 
            Action<CKShareParticipant,NSError> completionHandler)
        { 
            if(phoneNumber == null)
                throw new ArgumentNullException(nameof(phoneNumber));
            
            
            var completionHandlerCall = new InvocationRecord(Handle);
            FetchShareParticipantWithPhoneNumberCallbacks[completionHandlerCall] = completionHandler;
            
            CKContainer_fetchShareParticipantWithPhoneNumber_completionHandler(
                Handle, 
                phoneNumber, 
                
                completionHandlerCall.id, FetchShareParticipantWithPhoneNumberCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKShareParticipant,NSError>> FetchShareParticipantWithPhoneNumberCallbacks = new Dictionary<InvocationRecord,Action<CKShareParticipant,NSError>>();

        [MonoPInvokeCallback(typeof(CKShareParticipantDelegate))]
        private static void FetchShareParticipantWithPhoneNumberCallback(IntPtr thisPtr, ulong invocationId, IntPtr shareParticipant, IntPtr error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = FetchShareParticipantWithPhoneNumberCallbacks[invocation];
            FetchShareParticipantWithPhoneNumberCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    shareParticipant == IntPtr.Zero ? null : new CKShareParticipant(shareParticipant),
                    error == IntPtr.Zero ? null : new NSError(error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="userRecordID"></param><param name="completionHandler"></param>
        /// <returns>void</returns>
        public void FetchShareParticipantWithUserRecordID(
            CKRecordID userRecordID, 
            Action<CKShareParticipant,NSError> completionHandler)
        { 
            if(userRecordID == null)
                throw new ArgumentNullException(nameof(userRecordID));
            
            
            var completionHandlerCall = new InvocationRecord(Handle);
            FetchShareParticipantWithUserRecordIDCallbacks[completionHandlerCall] = completionHandler;
            
            CKContainer_fetchShareParticipantWithUserRecordID_completionHandler(
                Handle, 
                userRecordID != null ? HandleRef.ToIntPtr(userRecordID.Handle) : IntPtr.Zero, 
                
                completionHandlerCall.id, FetchShareParticipantWithUserRecordIDCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKShareParticipant,NSError>> FetchShareParticipantWithUserRecordIDCallbacks = new Dictionary<InvocationRecord,Action<CKShareParticipant,NSError>>();

        [MonoPInvokeCallback(typeof(CKShareParticipantDelegate))]
        private static void FetchShareParticipantWithUserRecordIDCallback(IntPtr thisPtr, ulong invocationId, IntPtr shareParticipant, IntPtr error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = FetchShareParticipantWithUserRecordIDCallbacks[invocation];
            FetchShareParticipantWithUserRecordIDCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    shareParticipant == IntPtr.Zero ? null : new CKShareParticipant(shareParticipant),
                    error == IntPtr.Zero ? null : new NSError(error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="operationID"></param><param name="completionHandler"></param>
        /// <returns>void</returns>
        public void FetchLongLivedOperationWithID(
            string operationID, 
            Action<CKOperation,NSError> completionHandler)
        { 
            if(operationID == null)
                throw new ArgumentNullException(nameof(operationID));
            
            
            var completionHandlerCall = new InvocationRecord(Handle);
            FetchLongLivedOperationWithIDCallbacks[completionHandlerCall] = completionHandler;
            
            CKContainer_fetchLongLivedOperationWithID_completionHandler(
                Handle, 
                operationID, 
                
                completionHandlerCall.id, FetchLongLivedOperationWithIDCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKOperation,NSError>> FetchLongLivedOperationWithIDCallbacks = new Dictionary<InvocationRecord,Action<CKOperation,NSError>>();

        [MonoPInvokeCallback(typeof(CKLongLivedOperationDelegate))]
        private static void FetchLongLivedOperationWithIDCallback(IntPtr thisPtr, ulong invocationId, IntPtr operationID, IntPtr error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = FetchLongLivedOperationWithIDCallbacks[invocation];
            FetchLongLivedOperationWithIDCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    operationID == IntPtr.Zero ? null : new CKOperation(operationID),
                    error == IntPtr.Zero ? null : new NSError(error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="metadata"></param><param name="completionHandler"></param>
        /// <returns>void</returns>
        public void AcceptShareMetadata(
            CKShareMetadata metadata, 
            Action<CKShare,NSError> completionHandler)
        { 
            if(metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            
            
            var completionHandlerCall = new InvocationRecord(Handle);
            AcceptShareMetadataCallbacks[completionHandlerCall] = completionHandler;
            
            CKContainer_acceptShareMetadata_completionHandler(
                Handle, 
                metadata != null ? HandleRef.ToIntPtr(metadata.Handle) : IntPtr.Zero, 
                
                completionHandlerCall.id, AcceptShareMetadataCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKShare,NSError>> AcceptShareMetadataCallbacks = new Dictionary<InvocationRecord,Action<CKShare,NSError>>();

        [MonoPInvokeCallback(typeof(CKShareDelegate))]
        private static void AcceptShareMetadataCallback(IntPtr thisPtr, ulong invocationId, IntPtr acceptedShare, IntPtr error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = AcceptShareMetadataCallbacks[invocation];
            AcceptShareMetadataCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    acceptedShare == IntPtr.Zero ? null : new CKShare(acceptedShare),
                    error == IntPtr.Zero ? null : new NSError(error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="applicationPermission"></param><param name="completionHandler"></param>
        /// <returns>void</returns>
        public void RequestApplicationPermission(
            CKApplicationPermissions applicationPermission, 
            Action<CKApplicationPermissionStatus,NSError> completionHandler)
        { 
            
            
            var completionHandlerCall = new InvocationRecord(Handle);
            RequestApplicationPermissionCallbacks[completionHandlerCall] = completionHandler;
            
            CKContainer_requestApplicationPermission_completionHandler(
                Handle, 
                (long) applicationPermission, 
                
                completionHandlerCall.id, RequestApplicationPermissionCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKApplicationPermissionStatus,NSError>> RequestApplicationPermissionCallbacks = new Dictionary<InvocationRecord,Action<CKApplicationPermissionStatus,NSError>>();

        [MonoPInvokeCallback(typeof(CKApplicationPermissionsDelegate))]
        private static void RequestApplicationPermissionCallback(IntPtr thisPtr, ulong invocationId, CKApplicationPermissionStatus applicationPermissionsStatus, IntPtr error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = RequestApplicationPermissionCallbacks[invocation];
            RequestApplicationPermissionCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    applicationPermissionsStatus,
                    error == IntPtr.Zero ? null : new NSError(error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="completionHandler"></param>
        /// <returns>void</returns>
        public void AccountStatusWithCompletionHandler(
            Action<CKAccountStatus,NSError> completionHandler)
        { 
            
            var completionHandlerCall = new InvocationRecord(Handle);
            AccountStatusWithCompletionHandlerCallbacks[completionHandlerCall] = completionHandler;
            
            CKContainer_accountStatusWithCompletionHandler(
                Handle, 
                completionHandlerCall.id, AccountStatusWithCompletionHandlerCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKAccountStatus,NSError>> AccountStatusWithCompletionHandlerCallbacks = new Dictionary<InvocationRecord,Action<CKAccountStatus,NSError>>();

        [MonoPInvokeCallback(typeof(CKAccountStatusDelegate))]
        private static void AccountStatusWithCompletionHandlerCallback(IntPtr thisPtr, ulong invocationId, CKAccountStatus accountStatus, IntPtr error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = AccountStatusWithCompletionHandlerCallbacks[invocation];
            AccountStatusWithCompletionHandlerCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    accountStatus,
                    error == IntPtr.Zero ? null : new NSError(error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="applicationPermission"></param><param name="completionHandler"></param>
        /// <returns>void</returns>
        public void StatusForApplicationPermission(
            CKApplicationPermissions applicationPermission, 
            Action<CKApplicationPermissionStatus,NSError> completionHandler)
        { 
            
            
            var completionHandlerCall = new InvocationRecord(Handle);
            StatusForApplicationPermissionCallbacks[completionHandlerCall] = completionHandler;
            
            CKContainer_statusForApplicationPermission_completionHandler(
                Handle, 
                (long) applicationPermission, 
                
                completionHandlerCall.id, StatusForApplicationPermissionCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKApplicationPermissionStatus,NSError>> StatusForApplicationPermissionCallbacks = new Dictionary<InvocationRecord,Action<CKApplicationPermissionStatus,NSError>>();

        [MonoPInvokeCallback(typeof(CKApplicationPermissionsDelegate))]
        private static void StatusForApplicationPermissionCallback(IntPtr thisPtr, ulong invocationId, CKApplicationPermissionStatus applicationPermissionsStatus, IntPtr error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = StatusForApplicationPermissionCallbacks[invocation];
            StatusForApplicationPermissionCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    applicationPermissionsStatus,
                    error == IntPtr.Zero ? null : new NSError(error)));
        }

        

        
        /// <summary>
        /// </summary>
        /// <param name="operation"></param>
        /// <returns>void</returns>
        public void AddOperation(
            CKOperation operation)
        { 
            if(operation == null)
                throw new ArgumentNullException(nameof(operation));
            
            CKContainer_addOperation(
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
        /// <param name="url"></param><param name="completionHandler"></param>
        /// <returns>void</returns>
        public void FetchShareMetadataWithURL(
            NSURL url, 
            Action<CKShareMetadata,NSError> completionHandler)
        { 
            if(url == null)
                throw new ArgumentNullException(nameof(url));
            
            
            var completionHandlerCall = new InvocationRecord(Handle);
            FetchShareMetadataWithURLCallbacks[completionHandlerCall] = completionHandler;
            
            CKContainer_fetchShareMetadataWithURL_completionHandler(
                Handle, 
                url != null ? HandleRef.ToIntPtr(url.Handle) : IntPtr.Zero, 
                
                completionHandlerCall.id, FetchShareMetadataWithURLCallback, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        
        private static readonly Dictionary<InvocationRecord,Action<CKShareMetadata,NSError>> FetchShareMetadataWithURLCallbacks = new Dictionary<InvocationRecord,Action<CKShareMetadata,NSError>>();

        [MonoPInvokeCallback(typeof(CKShareMetadataDelegate))]
        private static void FetchShareMetadataWithURLCallback(IntPtr thisPtr, ulong invocationId, IntPtr metadata, IntPtr error)
        {
            var invocation = new InvocationRecord(thisPtr, invocationId);
            var callback = FetchShareMetadataWithURLCallbacks[invocation];
            FetchShareMetadataWithURLCallbacks.Remove(invocation);
            
            Dispatcher.Instance.EnqueueOnMainThread(() =>
                callback(
                    metadata == IntPtr.Zero ? null : new CKShareMetadata(metadata),
                    error == IntPtr.Zero ? null : new NSError(error)));
        }

        

        
        
        /// <value>PrivateCloudDatabase</value>
        public CKDatabase PrivateCloudDatabase
        {
            get 
            { 
                IntPtr privateCloudDatabase = CKContainer_GetPropPrivateCloudDatabase(Handle);
                return privateCloudDatabase == IntPtr.Zero ? null : new CKDatabase(privateCloudDatabase);
            }
        }

        
        /// <value>PublicCloudDatabase</value>
        public CKDatabase PublicCloudDatabase
        {
            get 
            { 
                IntPtr publicCloudDatabase = CKContainer_GetPropPublicCloudDatabase(Handle);
                return publicCloudDatabase == IntPtr.Zero ? null : new CKDatabase(publicCloudDatabase);
            }
        }

        
        /// <value>SharedCloudDatabase</value>
        public CKDatabase SharedCloudDatabase
        {
            get 
            { 
                IntPtr sharedCloudDatabase = CKContainer_GetPropSharedCloudDatabase(Handle);
                return sharedCloudDatabase == IntPtr.Zero ? null : new CKDatabase(sharedCloudDatabase);
            }
        }

        
        /// <value>ContainerIdentifier</value>
        public string ContainerIdentifier
        {
            get 
            { 
                IntPtr containerIdentifier = CKContainer_GetPropContainerIdentifier(Handle);
                return Marshal.PtrToStringAuto(containerIdentifier);
            }
        }

        

        
        private static readonly Dictionary<IntPtr,Action<NSNotification>> AccountChangedNotificationHandlers = new Dictionary<IntPtr,Action<NSNotification>>();

        [MonoPInvokeCallback(typeof(NotificationDelegate))]
        private static void CKAccountChangedNotificationStaticHandler(IntPtr ptr, IntPtr notification)
        {
            Action<NSNotification> handler = null;
            if(AccountChangedNotificationHandlers.TryGetValue(ptr, out handler))
            {
                Dispatcher.Instance.EnqueueOnMainThread(() => {
                    handler.Invoke(ptr == IntPtr.Zero ? null : new NSNotification(notification));
                });
            }
        }

        public static Unsubscriber AddAccountChangedNotificationObserver(Action<NSNotification> observer)
        {
            IntPtr exceptionPtr = IntPtr.Zero;

            IntPtr observerHandle = AddCKAccountChangedNotificationObserver(CKAccountChangedNotificationStaticHandler, ref exceptionPtr);
            
            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            AccountChangedNotificationHandlers[observerHandle] = observer;

            return observerHandle == IntPtr.Zero ? null : new Unsubscriber(observerHandle);
        }
        
        public static void RemoveAccountChangedNotificationObserver(Unsubscriber unsubscriber)
        {
            IntPtr exceptionPtr = IntPtr.Zero;
            RemoveCKAccountChangedNotificationObserver(unsubscriber.Handle, ref exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            AccountChangedNotificationHandlers.Remove(HandleRef.ToIntPtr(unsubscriber.Handle));
        }
        

        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKContainer_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKContainer Dispose");
                CKContainer_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKContainer()
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
