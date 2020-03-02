//
//  CKDiscoverUserIdentitiesOperation.cs
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
    public class CKDiscoverUserIdentitiesOperation : CKObject
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKDiscoverUserIdentitiesOperation_init();
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKDiscoverUserIdentitiesOperation_initWithUserIdentityLookupInfos(
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
        private static extern void CKDiscoverUserIdentitiesOperation_GetPropUserIdentityLookupInfos(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDiscoverUserIdentitiesOperation_SetPropUserIdentityLookupInfos(HandleRef ptr, IntPtr[] userIdentityLookupInfos,
			int userIdentityLookupInfosCount);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKDiscoverUserIdentitiesOperation_SetPropDiscoverUserIdentitiesCompletionHandler(HandleRef ptr, DiscoverUserIdentitiesCompletionDelegate discoverUserIdentitiesCompletionHandler);
        
        #endregion

        internal CKDiscoverUserIdentitiesOperation(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKDiscoverUserIdentitiesOperation init(
        ){
            
            IntPtr ptr = CKDiscoverUserIdentitiesOperation_init();
            return new CKDiscoverUserIdentitiesOperation(ptr);
        }
        
        
        public static CKDiscoverUserIdentitiesOperation initWithUserIdentityLookupInfos(
            CKUserIdentityLookupInfo[] userIdentityLookupInfos
        ){
            if(userIdentityLookupInfos == null)
                throw new ArgumentNullException(nameof(userIdentityLookupInfos));
            
            IntPtr ptr = CKDiscoverUserIdentitiesOperation_initWithUserIdentityLookupInfos(
                userIdentityLookupInfos == null ? null : userIdentityLookupInfos.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				userIdentityLookupInfos == null ? 0 : userIdentityLookupInfos.Length);
            return new CKDiscoverUserIdentitiesOperation(ptr);
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

                CKDiscoverUserIdentitiesOperation_GetPropUserIdentityLookupInfos(Handle, ref bufferPtr, ref bufferLen);

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
                CKDiscoverUserIdentitiesOperation_SetPropUserIdentityLookupInfos(Handle, value == null ? null : value.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				value == null ? 0 : value.Length);
            }
        }

        
        public Action<NSError> DiscoverUserIdentitiesCompletionHandler 
        {
            get 
            {
                Action<NSError> value;
                DiscoverUserIdentitiesCompletionHandlerCallbacks.TryGetValue(HandleRef.ToIntPtr(Handle), out value);
                return value;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    DiscoverUserIdentitiesCompletionHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    DiscoverUserIdentitiesCompletionHandlerCallbacks[myPtr] = value;
                }
                CKDiscoverUserIdentitiesOperation_SetPropDiscoverUserIdentitiesCompletionHandler(Handle, DiscoverUserIdentitiesCompletionHandlerCallback);
            }
        }

        private static readonly Dictionary<IntPtr,Action<NSError>> DiscoverUserIdentitiesCompletionHandlerCallbacks = new Dictionary<IntPtr,Action<NSError>>();

        [MonoPInvokeCallback(typeof(DiscoverUserIdentitiesCompletionDelegate))]
        private static void DiscoverUserIdentitiesCompletionHandlerCallback(IntPtr thisPtr, IntPtr _operationError)
        {
            if(DiscoverUserIdentitiesCompletionHandlerCallbacks.TryGetValue(thisPtr, out Action<NSError> callback))
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

        
        #endregion
    }
}
