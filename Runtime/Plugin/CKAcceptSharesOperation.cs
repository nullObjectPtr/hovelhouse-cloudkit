//
//  CKAcceptSharesOperation.cs
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
    /// An operation that accepts shared records
    /// </summary>
    public class CKAcceptSharesOperation : CKObject, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        
        [DllImport(dll)]
        private static extern IntPtr CKAcceptSharesOperation_init(
            out IntPtr exceptionPtr
            );
        
        [DllImport(dll)]
        private static extern IntPtr CKAcceptSharesOperation_initWithShareMetadatas(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 2)]
            IntPtr[] shareMetadatas,
			int shareMetadatasCount, 
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern void CKAcceptSharesOperation_GetPropShareMetadatas(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        [DllImport(dll)]
        private static extern void CKAcceptSharesOperation_SetPropShareMetadatas(HandleRef ptr, IntPtr[] shareMetadatas,
			int shareMetadatasCount, out IntPtr exceptionPtr);

        [DllImport(dll)]
        private static extern void CKAcceptSharesOperation_SetPropAcceptSharesHandler(HandleRef ptr, AcceptSharesCompletionDelegate acceptSharesHandler, out IntPtr exceptionPtr);

        [DllImport(dll)]
        private static extern void CKAcceptSharesOperation_SetPropPerShareCompletionHandler(HandleRef ptr, PerShareCompletionDelegate perShareCompletionHandler, out IntPtr exceptionPtr);

        

        #endregion

        internal CKAcceptSharesOperation(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKAcceptSharesOperation(
            )
        {
            
            IntPtr ptr = CKAcceptSharesOperation_init(
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKAcceptSharesOperation(
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

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        
        /// <value>ShareMetadatas</value>
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
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * IntPtr.Size));
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

        
        /// <value>AcceptSharesHandler</value>
        public Action<NSError> AcceptSharesHandler
        {
            get 
            {
                AcceptSharesHandlerCallbacks.TryGetValue(
                    HandleRef.ToIntPtr(Handle), 
                    out ExecutionContext<NSError> value);
                return value.Callback;
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
                    AcceptSharesHandlerCallbacks[myPtr] = new ExecutionContext<NSError>(value);
                }
                CKAcceptSharesOperation_SetPropAcceptSharesHandler(Handle, AcceptSharesHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,ExecutionContext<NSError>> AcceptSharesHandlerCallbacks = new Dictionary<IntPtr,ExecutionContext<NSError>>();

        [MonoPInvokeCallback(typeof(AcceptSharesCompletionDelegate))]
        private static void AcceptSharesHandlerCallback(IntPtr thisPtr, IntPtr _operationError)
        {
            if(AcceptSharesHandlerCallbacks.TryGetValue(thisPtr, out ExecutionContext<NSError> callback))
            {
                callback.Invoke(
                        _operationError == IntPtr.Zero ? null : new NSError(_operationError));
            }
        }

        
        /// <value>PerShareCompletionHandler</value>
        public Action<CKShareMetadata,CKShare,NSError> PerShareCompletionHandler
        {
            get 
            {
                PerShareCompletionHandlerCallbacks.TryGetValue(
                    HandleRef.ToIntPtr(Handle), 
                    out ExecutionContext<CKShareMetadata,CKShare,NSError> value);
                return value.Callback;
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
                    PerShareCompletionHandlerCallbacks[myPtr] = new ExecutionContext<CKShareMetadata,CKShare,NSError>(value);
                }
                CKAcceptSharesOperation_SetPropPerShareCompletionHandler(Handle, PerShareCompletionHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,ExecutionContext<CKShareMetadata,CKShare,NSError>> PerShareCompletionHandlerCallbacks = new Dictionary<IntPtr,ExecutionContext<CKShareMetadata,CKShare,NSError>>();

        [MonoPInvokeCallback(typeof(PerShareCompletionDelegate))]
        private static void PerShareCompletionHandlerCallback(IntPtr thisPtr, IntPtr _shareMetadata, IntPtr _acceptedShare, IntPtr _error)
        {
            if(PerShareCompletionHandlerCallbacks.TryGetValue(thisPtr, out ExecutionContext<CKShareMetadata,CKShare,NSError> callback))
            {
                callback.Invoke(
                        _shareMetadata == IntPtr.Zero ? null : new CKShareMetadata(_shareMetadata),
                        _acceptedShare == IntPtr.Zero ? null : new CKShare(_acceptedShare),
                        _error == IntPtr.Zero ? null : new NSError(_error));
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKAcceptSharesOperation_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
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
