//
//  CKFetchShareMetadataOperation.cs
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
    public class CKFetchShareMetadataOperation : CKObject
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKFetchShareMetadataOperation_init();
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKFetchShareMetadataOperation_initWithShareURLs(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 2)]
            IntPtr[] shareURLs,
			int shareURLsCount);
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern bool CKFetchShareMetadataOperation_GetPropShouldFetchRootRecord(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchShareMetadataOperation_SetPropShouldFetchRootRecord(HandleRef ptr, bool shouldFetchRootRecord);
        // TODO: DLLPROPERTYSTRINGARRAY
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchShareMetadataOperation_GetPropShareURLs(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchShareMetadataOperation_SetPropShareURLs(HandleRef ptr, IntPtr[] shareURLs,
			int shareURLsCount);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchShareMetadataOperation_SetPropFetchShareMetadataCompletionHandler(HandleRef ptr, FetchShareMetadataCompletionDelegate fetchShareMetadataCompletionHandler);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchShareMetadataOperation_SetPropPerShareMetadataHandler(HandleRef ptr, PerShareMetadataDelegate perShareMetadataHandler);
        
        #endregion

        internal CKFetchShareMetadataOperation(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKFetchShareMetadataOperation init(
        ){
            
            IntPtr ptr = CKFetchShareMetadataOperation_init();
            return new CKFetchShareMetadataOperation(ptr);
        }
        
        
        public static CKFetchShareMetadataOperation initWithShareURLs(
            NSURL[] shareURLs
        ){
            if(shareURLs == null)
                throw new ArgumentNullException(nameof(shareURLs));
            
            IntPtr ptr = CKFetchShareMetadataOperation_initWithShareURLs(
                shareURLs == null ? null : shareURLs.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				shareURLs == null ? 0 : shareURLs.Length);
            return new CKFetchShareMetadataOperation(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public bool ShouldFetchRootRecord 
        {
            get 
            { 
                bool shouldFetchRootRecord = CKFetchShareMetadataOperation_GetPropShouldFetchRootRecord(Handle);
                return shouldFetchRootRecord;
            }
            set
            {
                CKFetchShareMetadataOperation_SetPropShouldFetchRootRecord(Handle, value);
            }
        }
        
        // TODO: PROPERTYSTRINGARRAY
        
        public NSURL[] ShareURLs 
        {
            get 
            { 
                IntPtr bufferPtr = IntPtr.Zero;
                long bufferLen = 0;

                CKFetchShareMetadataOperation_GetPropShareURLs(Handle, ref bufferPtr, ref bufferLen);

                var shareURLs = new NSURL[bufferLen];

                for (int i = 0; i < bufferLen; i++)
                {
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * 8));
                    shareURLs[i] = ptr2 == IntPtr.Zero ? null : new NSURL(ptr2);
                }

                Marshal.FreeHGlobal(bufferPtr);

                return shareURLs;
            }
            set
            {
                CKFetchShareMetadataOperation_SetPropShareURLs(Handle, value == null ? null : value.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				value == null ? 0 : value.Length);
            }
        }

        
        public Action<NSError> FetchShareMetadataCompletionHandler 
        {
            get 
            {
                Action<NSError> value;
                FetchShareMetadataCompletionHandlerCallbacks.TryGetValue(HandleRef.ToIntPtr(Handle), out value);
                return value;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    FetchShareMetadataCompletionHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    FetchShareMetadataCompletionHandlerCallbacks[myPtr] = value;
                }
                CKFetchShareMetadataOperation_SetPropFetchShareMetadataCompletionHandler(Handle, FetchShareMetadataCompletionHandlerCallback);
            }
        }

        private static readonly Dictionary<IntPtr,Action<NSError>> FetchShareMetadataCompletionHandlerCallbacks = new Dictionary<IntPtr,Action<NSError>>();

        [MonoPInvokeCallback(typeof(FetchShareMetadataCompletionDelegate))]
        private static void FetchShareMetadataCompletionHandlerCallback(IntPtr thisPtr, IntPtr _operationError)
        {
            if(FetchShareMetadataCompletionHandlerCallbacks.TryGetValue(thisPtr, out Action<NSError> callback))
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

        
        public Action<NSURL,CKShareMetadata,NSError> PerShareMetadataHandler 
        {
            get 
            {
                Action<NSURL,CKShareMetadata,NSError> value;
                PerShareMetadataHandlerCallbacks.TryGetValue(HandleRef.ToIntPtr(Handle), out value);
                return value;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    PerShareMetadataHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    PerShareMetadataHandlerCallbacks[myPtr] = value;
                }
                CKFetchShareMetadataOperation_SetPropPerShareMetadataHandler(Handle, PerShareMetadataHandlerCallback);
            }
        }

        private static readonly Dictionary<IntPtr,Action<NSURL,CKShareMetadata,NSError>> PerShareMetadataHandlerCallbacks = new Dictionary<IntPtr,Action<NSURL,CKShareMetadata,NSError>>();

        [MonoPInvokeCallback(typeof(PerShareMetadataDelegate))]
        private static void PerShareMetadataHandlerCallback(IntPtr thisPtr, IntPtr _shareURL, IntPtr _shareMetadata, IntPtr _error)
        {
            if(PerShareMetadataHandlerCallbacks.TryGetValue(thisPtr, out Action<NSURL,CKShareMetadata,NSError> callback))
            {
                try
                {
                    callback(
                        _shareURL == IntPtr.Zero ? null : new NSURL(_shareURL),
                        _shareMetadata == IntPtr.Zero ? null : new CKShareMetadata(_shareMetadata),
                        _error == IntPtr.Zero ? null : new NSError(_error));
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
