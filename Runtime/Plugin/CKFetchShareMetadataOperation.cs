//
//  CKFetchShareMetadataOperation.cs
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
    /// A container operation that fetches metadata for the provided share URLs
    /// </summary>
    public class CKFetchShareMetadataOperation : CKOperation, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        
        [DllImport(dll)]
        private static extern IntPtr CKFetchShareMetadataOperation_init(
            out IntPtr exceptionPtr
            );
        
        [DllImport(dll)]
        private static extern IntPtr CKFetchShareMetadataOperation_initWithShareURLs(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 2)]
            IntPtr[] shareURLs,
			int shareURLsCount, 
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern bool CKFetchShareMetadataOperation_GetPropShouldFetchRootRecord(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKFetchShareMetadataOperation_SetPropShouldFetchRootRecord(HandleRef ptr, bool shouldFetchRootRecord, out IntPtr exceptionPtr);

        // TODO: DLLPROPERTYSTRINGARRAY

        
        [DllImport(dll)]
        private static extern void CKFetchShareMetadataOperation_GetPropShareURLs(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        [DllImport(dll)]
        private static extern void CKFetchShareMetadataOperation_SetPropShareURLs(HandleRef ptr, IntPtr[] shareURLs,
			int shareURLsCount, out IntPtr exceptionPtr);

        [DllImport(dll)]
        private static extern void CKFetchShareMetadataOperation_SetPropFetchShareMetadataCompletionHandler(HandleRef ptr, FetchShareMetadataCompletionDelegate fetchShareMetadataCompletionHandler, out IntPtr exceptionPtr);

        [DllImport(dll)]
        private static extern void CKFetchShareMetadataOperation_SetPropPerShareMetadataHandler(HandleRef ptr, PerShareMetadataDelegate perShareMetadataHandler, out IntPtr exceptionPtr);

        

        #endregion

        internal CKFetchShareMetadataOperation(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKFetchShareMetadataOperation(
            )
        {
            
            IntPtr ptr = CKFetchShareMetadataOperation_init(
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKFetchShareMetadataOperation(
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

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        
        /// <value>ShouldFetchRootRecord</value>
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
        
        /// <value>ShareURLs</value>
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
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * IntPtr.Size));
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

        
        /// <value>FetchShareMetadataCompletionHandler</value>
        public Action<NSError> FetchShareMetadataCompletionHandler
        {
            get 
            {
                FetchShareMetadataCompletionHandlerCallbacks.TryGetValue(
                    HandleRef.ToIntPtr(Handle), 
                    out ExecutionContext<NSError> value);
                return value.Callback;
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
                    FetchShareMetadataCompletionHandlerCallbacks[myPtr] = new ExecutionContext<NSError>(value);
                }
                CKFetchShareMetadataOperation_SetPropFetchShareMetadataCompletionHandler(Handle, FetchShareMetadataCompletionHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,ExecutionContext<NSError>> FetchShareMetadataCompletionHandlerCallbacks = new Dictionary<IntPtr,ExecutionContext<NSError>>();

        [MonoPInvokeCallback(typeof(FetchShareMetadataCompletionDelegate))]
        private static void FetchShareMetadataCompletionHandlerCallback(IntPtr thisPtr, IntPtr _operationError)
        {
            if(FetchShareMetadataCompletionHandlerCallbacks.TryGetValue(thisPtr, out ExecutionContext<NSError> callback))
            {
                callback.Invoke(
                        _operationError == IntPtr.Zero ? null : new NSError(_operationError));
            }
        }

        
        /// <value>PerShareMetadataHandler</value>
        public Action<NSURL,CKShareMetadata,NSError> PerShareMetadataHandler
        {
            get 
            {
                PerShareMetadataHandlerCallbacks.TryGetValue(
                    HandleRef.ToIntPtr(Handle), 
                    out ExecutionContext<NSURL,CKShareMetadata,NSError> value);
                return value.Callback;
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
                    PerShareMetadataHandlerCallbacks[myPtr] = new ExecutionContext<NSURL,CKShareMetadata,NSError>(value);
                }
                CKFetchShareMetadataOperation_SetPropPerShareMetadataHandler(Handle, PerShareMetadataHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,ExecutionContext<NSURL,CKShareMetadata,NSError>> PerShareMetadataHandlerCallbacks = new Dictionary<IntPtr,ExecutionContext<NSURL,CKShareMetadata,NSError>>();

        [MonoPInvokeCallback(typeof(PerShareMetadataDelegate))]
        private static void PerShareMetadataHandlerCallback(IntPtr thisPtr, IntPtr _shareURL, IntPtr _shareMetadata, IntPtr _error)
        {
            if(PerShareMetadataHandlerCallbacks.TryGetValue(thisPtr, out ExecutionContext<NSURL,CKShareMetadata,NSError> callback))
            {
                callback.Invoke(
                        _shareURL == IntPtr.Zero ? null : new NSURL(_shareURL),
                        _shareMetadata == IntPtr.Zero ? null : new CKShareMetadata(_shareMetadata),
                        _error == IntPtr.Zero ? null : new NSError(_error));
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKFetchShareMetadataOperation_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected override void Dispose(bool disposing)
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
        public new void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
        
    }
}
