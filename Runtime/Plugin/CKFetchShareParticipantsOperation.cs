//
//  CKFetchShareParticipantsOperation.cs
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
    public class CKFetchShareParticipantsOperation : CKObject
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKFetchShareParticipantsOperation_init();
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKFetchShareParticipantsOperation_initWithUserIdentityLookupInfos(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 2)]
            IntPtr[] userIdentityLookupInfos,
			int userIdentityLookupInfosCount);
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchShareParticipantsOperation_GetPropUserIdentityLookupInfos(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchShareParticipantsOperation_SetPropUserIdentityLookupInfos(HandleRef ptr, IntPtr[] userIdentityLookupInfos,
			int userIdentityLookupInfosCount);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchShareParticipantsOperation_SetPropFetchShareParticipantsCompletionHandler(HandleRef ptr, FetchShareParticipantsCompletionDelegate fetchShareParticipantsCompletionHandler);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchShareParticipantsOperation_SetPropShareParticipantFetchedHandler(HandleRef ptr, ShareParticipantFetchedDelegate shareParticipantFetchedHandler);
        
        #endregion

        internal CKFetchShareParticipantsOperation(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKFetchShareParticipantsOperation init(
        ){
            
            IntPtr ptr = CKFetchShareParticipantsOperation_init();
            return new CKFetchShareParticipantsOperation(ptr);
        }
        
        
        public static CKFetchShareParticipantsOperation initWithUserIdentityLookupInfos(
            CKUserIdentityLookupInfo[] userIdentityLookupInfos
        ){
            if(userIdentityLookupInfos == null)
                throw new ArgumentNullException(nameof(userIdentityLookupInfos));
            
            IntPtr ptr = CKFetchShareParticipantsOperation_initWithUserIdentityLookupInfos(
                userIdentityLookupInfos == null ? null : userIdentityLookupInfos.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				userIdentityLookupInfos == null ? 0 : userIdentityLookupInfos.Length);
            return new CKFetchShareParticipantsOperation(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public CKUserIdentityLookupInfo[] UserIdentityLookupInfos 
        {
            get 
            { 
                IntPtr bufferPtr = IntPtr.Zero;
                long bufferLen = 0;

                CKFetchShareParticipantsOperation_GetPropUserIdentityLookupInfos(Handle, ref bufferPtr, ref bufferLen);

                var userIdentityLookupInfos = new CKUserIdentityLookupInfo[bufferLen];

                for (int i = 0; i < bufferLen; i++)
                {
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * 8));
                    userIdentityLookupInfos[i] = ptr2 == IntPtr.Zero ? null : new CKUserIdentityLookupInfo(ptr2);
                }

                Marshal.FreeHGlobal(bufferPtr);

                return userIdentityLookupInfos;
            }
            set
            {
                CKFetchShareParticipantsOperation_SetPropUserIdentityLookupInfos(Handle, value == null ? null : value.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				value == null ? 0 : value.Length);
            }
        }

        
        public Action<NSError> FetchShareParticipantsCompletionHandler 
        {
            get 
            {
                Action<NSError> value;
                FetchShareParticipantsCompletionHandlerCallbacks.TryGetValue(HandleRef.ToIntPtr(Handle), out value);
                return value;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    FetchShareParticipantsCompletionHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    FetchShareParticipantsCompletionHandlerCallbacks[myPtr] = value;
                }
                CKFetchShareParticipantsOperation_SetPropFetchShareParticipantsCompletionHandler(Handle, FetchShareParticipantsCompletionHandlerCallback);
            }
        }

        private static readonly Dictionary<IntPtr,Action<NSError>> FetchShareParticipantsCompletionHandlerCallbacks = new Dictionary<IntPtr,Action<NSError>>();

        [MonoPInvokeCallback(typeof(FetchShareParticipantsCompletionDelegate))]
        private static void FetchShareParticipantsCompletionHandlerCallback(IntPtr thisPtr, IntPtr _operationError)
        {
            if(FetchShareParticipantsCompletionHandlerCallbacks.TryGetValue(thisPtr, out Action<NSError> callback))
            {
                try
                {
                    callback(
                        _operationError == IntPtr.Zero ? null : new NSError(_operationError));
                }
                catch(Exception exc)
                {
                    Debug.LogError(exc);
                }
            }
        }

        
        public Action<CKShareParticipant> ShareParticipantFetchedHandler 
        {
            get 
            {
                Action<CKShareParticipant> value;
                ShareParticipantFetchedHandlerCallbacks.TryGetValue(HandleRef.ToIntPtr(Handle), out value);
                return value;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    ShareParticipantFetchedHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    ShareParticipantFetchedHandlerCallbacks[myPtr] = value;
                }
                CKFetchShareParticipantsOperation_SetPropShareParticipantFetchedHandler(Handle, ShareParticipantFetchedHandlerCallback);
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKShareParticipant>> ShareParticipantFetchedHandlerCallbacks = new Dictionary<IntPtr,Action<CKShareParticipant>>();

        [MonoPInvokeCallback(typeof(ShareParticipantFetchedDelegate))]
        private static void ShareParticipantFetchedHandlerCallback(IntPtr thisPtr, IntPtr _participant)
        {
            if(ShareParticipantFetchedHandlerCallbacks.TryGetValue(thisPtr, out Action<CKShareParticipant> callback))
            {
                try
                {
                    callback(
                        _participant == IntPtr.Zero ? null : new CKShareParticipant(_participant));
                }
                catch(Exception exc)
                {
                    Debug.LogError(exc);
                }
            }
        }

        
        #endregion
    }
}
