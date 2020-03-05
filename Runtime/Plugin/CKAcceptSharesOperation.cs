//
//  CKAcceptSharesOperation.cs
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
    public class CKAcceptSharesOperation : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKAcceptSharesOperation_init(
            out IntPtr exceptionPtr
            );
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKAcceptSharesOperation_initWithShareMetadatas(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 2)]
            IntPtr[] shareMetadatas,
			int shareMetadatasCount, 
            out IntPtr exceptionPtr
            );
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKAcceptSharesOperation_GetPropShareMetadatas(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKAcceptSharesOperation_SetPropShareMetadatas(HandleRef ptr, IntPtr[] shareMetadatas,
			int shareMetadatasCount, out IntPtr exceptionPtr);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKAcceptSharesOperation_SetPropAcceptSharesHandler(HandleRef ptr, AcceptSharesCompletionDelegate acceptSharesHandler, out IntPtr exceptionPtr);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKAcceptSharesOperation_SetPropPerShareCompletionHandler(HandleRef ptr, PerShareCompletionDelegate perShareCompletionHandler, out IntPtr exceptionPtr);
        
        #endregion

        internal CKAcceptSharesOperation(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKAcceptSharesOperation init(
            )
        {
            
            IntPtr ptr = CKAcceptSharesOperation_init(
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            return new CKAcceptSharesOperation(ptr);
        }
        
        
        public static CKAcceptSharesOperation initWithShareMetadatas(
            CKShareMetadata[] shareMetadatas
            )
        {
            if(shareMetadatas == null)
                throw new ArgumentNullException(nameof(shareMetadatas));
            
            IntPtr ptr = CKAcceptSharesOperation_initWithShareMetadatas(
                shareMetadatas == null ? null : shareMetadatas.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				shareMetadatas == null ? 0 : shareMetadatas.Length, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            return new CKAcceptSharesOperation(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public CKShareMetadata[] ShareMetadatas 
        {
            get 
            { 
                IntPtr bufferPtr = IntPtr.Zero;
                long bufferLen = 0;

                CKAcceptSharesOperation_GetPropShareMetadatas(Handle, ref bufferPtr, ref bufferLen);

                var shareMetadatas = new CKShareMetadata[bufferLen];

                for (int i = 0; i < bufferLen; i++)
                {
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * 8));
                    shareMetadatas[i] = ptr2 == IntPtr.Zero ? null : new CKShareMetadata(ptr2);
                }

                Marshal.FreeHGlobal(bufferPtr);

                return shareMetadatas;
            }
            set
            {
                CKAcceptSharesOperation_SetPropShareMetadatas(Handle, value == null ? null : value.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				value == null ? 0 : value.Length, out IntPtr exceptionPtr);
                
                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        
        public Action<NSError> AcceptSharesHandler 
        {
            get 
            {
                Action<NSError> value;
                AcceptSharesHandlerCallbacks.TryGetValue(HandleRef.ToIntPtr(Handle), out value);
                return value;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    AcceptSharesHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    AcceptSharesHandlerCallbacks[myPtr] = value;
                }
                CKAcceptSharesOperation_SetPropAcceptSharesHandler(Handle, AcceptSharesHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,Action<NSError>> AcceptSharesHandlerCallbacks = new Dictionary<IntPtr,Action<NSError>>();

        [MonoPInvokeCallback(typeof(AcceptSharesCompletionDelegate))]
        private static void AcceptSharesHandlerCallback(IntPtr thisPtr, IntPtr _operationError)
        {
            if(AcceptSharesHandlerCallbacks.TryGetValue(thisPtr, out Action<NSError> callback))
            {
                Dispatcher.Instance.EnqueueOnMainThread(() => 
                    callback(
                        _operationError == IntPtr.Zero ? null : new NSError(_operationError)));
            }
        }

        
        public Action<CKShareMetadata,CKShare,NSError> PerShareCompletionHandler 
        {
            get 
            {
                Action<CKShareMetadata,CKShare,NSError> value;
                PerShareCompletionHandlerCallbacks.TryGetValue(HandleRef.ToIntPtr(Handle), out value);
                return value;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    PerShareCompletionHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    PerShareCompletionHandlerCallbacks[myPtr] = value;
                }
                CKAcceptSharesOperation_SetPropPerShareCompletionHandler(Handle, PerShareCompletionHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKShareMetadata,CKShare,NSError>> PerShareCompletionHandlerCallbacks = new Dictionary<IntPtr,Action<CKShareMetadata,CKShare,NSError>>();

        [MonoPInvokeCallback(typeof(PerShareCompletionDelegate))]
        private static void PerShareCompletionHandlerCallback(IntPtr thisPtr, IntPtr _shareMetadata, IntPtr _acceptedShare, IntPtr _error)
        {
            if(PerShareCompletionHandlerCallbacks.TryGetValue(thisPtr, out Action<CKShareMetadata,CKShare,NSError> callback))
            {
                Dispatcher.Instance.EnqueueOnMainThread(() => 
                    callback(
                        _shareMetadata == IntPtr.Zero ? null : new CKShareMetadata(_shareMetadata),
                        _acceptedShare == IntPtr.Zero ? null : new CKShare(_acceptedShare),
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
        private static extern void CKAcceptSharesOperation_Dispose(HandleRef handle);
            
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
                
                //Debug.Log("CKAcceptSharesOperation Dispose");
                CKAcceptSharesOperation_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKAcceptSharesOperation()
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
