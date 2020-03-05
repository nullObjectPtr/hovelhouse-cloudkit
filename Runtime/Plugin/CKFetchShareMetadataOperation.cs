//
//  CKFetchShareMetadataOperation.cs
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
    public class CKFetchShareMetadataOperation : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKFetchShareMetadataOperation_init(
            out IntPtr exceptionPtr
            );
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKFetchShareMetadataOperation_initWithShareURLs(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 2)]
            IntPtr[] shareURLs,
			int shareURLsCount, 
            out IntPtr exceptionPtr
            );
        

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
        private static extern void CKFetchShareMetadataOperation_SetPropShouldFetchRootRecord(HandleRef ptr, bool shouldFetchRootRecord, out IntPtr exceptionPtr);
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
			int shareURLsCount, out IntPtr exceptionPtr);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchShareMetadataOperation_SetPropFetchShareMetadataCompletionHandler(HandleRef ptr, FetchShareMetadataCompletionDelegate fetchShareMetadataCompletionHandler, out IntPtr exceptionPtr);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchShareMetadataOperation_SetPropPerShareMetadataHandler(HandleRef ptr, PerShareMetadataDelegate perShareMetadataHandler, out IntPtr exceptionPtr);
        
        #endregion

        internal CKFetchShareMetadataOperation(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKFetchShareMetadataOperation init(
            )
        {
            
            IntPtr ptr = CKFetchShareMetadataOperation_init(
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            return new CKFetchShareMetadataOperation(ptr);
        }
        
        
        public static CKFetchShareMetadataOperation initWithShareURLs(
            NSURL[] shareURLs
            )
        {
            if(shareURLs == null)
                throw new ArgumentNullException(nameof(shareURLs));
            
            IntPtr ptr = CKFetchShareMetadataOperation_initWithShareURLs(
                shareURLs == null ? null : shareURLs.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				shareURLs == null ? 0 : shareURLs.Length, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

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
                CKFetchShareMetadataOperation_SetPropShouldFetchRootRecord(Handle, value, out IntPtr exceptionPtr);
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
				value == null ? 0 : value.Length, out IntPtr exceptionPtr);
                
                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
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
                CKFetchShareMetadataOperation_SetPropFetchShareMetadataCompletionHandler(Handle, FetchShareMetadataCompletionHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,Action<NSError>> FetchShareMetadataCompletionHandlerCallbacks = new Dictionary<IntPtr,Action<NSError>>();

        [MonoPInvokeCallback(typeof(FetchShareMetadataCompletionDelegate))]
        private static void FetchShareMetadataCompletionHandlerCallback(IntPtr thisPtr, IntPtr _operationError)
        {
            if(FetchShareMetadataCompletionHandlerCallbacks.TryGetValue(thisPtr, out Action<NSError> callback))
            {
                Dispatcher.Instance.EnqueueOnMainThread(() => 
                    callback(
                        _operationError == IntPtr.Zero ? null : new NSError(_operationError)));
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
                CKFetchShareMetadataOperation_SetPropPerShareMetadataHandler(Handle, PerShareMetadataHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,Action<NSURL,CKShareMetadata,NSError>> PerShareMetadataHandlerCallbacks = new Dictionary<IntPtr,Action<NSURL,CKShareMetadata,NSError>>();

        [MonoPInvokeCallback(typeof(PerShareMetadataDelegate))]
        private static void PerShareMetadataHandlerCallback(IntPtr thisPtr, IntPtr _shareURL, IntPtr _shareMetadata, IntPtr _error)
        {
            if(PerShareMetadataHandlerCallbacks.TryGetValue(thisPtr, out Action<NSURL,CKShareMetadata,NSError> callback))
            {
                Dispatcher.Instance.EnqueueOnMainThread(() => 
                    callback(
                        _shareURL == IntPtr.Zero ? null : new NSURL(_shareURL),
                        _shareMetadata == IntPtr.Zero ? null : new CKShareMetadata(_shareMetadata),
                        _error == IntPtr.Zero ? null : new NSError(_error)));
            }
        }

        
        #endregion
        
        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchShareMetadataOperation_Dispose(HandleRef handle);
            
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
                
                //Debug.Log("CKFetchShareMetadataOperation Dispose");
                CKFetchShareMetadataOperation_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKFetchShareMetadataOperation()
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
