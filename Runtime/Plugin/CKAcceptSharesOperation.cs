//
//  CKAcceptSharesOperation.cs
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
    public class CKAcceptSharesOperation : CKObject
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKAcceptSharesOperation_init();
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKAcceptSharesOperation_initWithShareMetadatas(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 2)]
            IntPtr[] shareMetadatas,
			int shareMetadatasCount);
        

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
			int shareMetadatasCount);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKAcceptSharesOperation_SetPropAcceptSharesHandler(HandleRef ptr, AcceptSharesCompletionDelegate acceptSharesHandler);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKAcceptSharesOperation_SetPropPerShareCompletionHandler(HandleRef ptr, PerShareCompletionDelegate perShareCompletionHandler);
        
        #endregion

        internal CKAcceptSharesOperation(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKAcceptSharesOperation init(
        ){
            
            IntPtr ptr = CKAcceptSharesOperation_init();
            return new CKAcceptSharesOperation(ptr);
        }
        
        
        public static CKAcceptSharesOperation initWithShareMetadatas(
            CKShareMetadata[] shareMetadatas
        ){
            if(shareMetadatas == null)
                throw new ArgumentNullException(nameof(shareMetadatas));
            
            IntPtr ptr = CKAcceptSharesOperation_initWithShareMetadatas(
                shareMetadatas == null ? null : shareMetadatas.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				shareMetadatas == null ? 0 : shareMetadatas.Length);
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
				value == null ? 0 : value.Length);
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
                CKAcceptSharesOperation_SetPropAcceptSharesHandler(Handle, AcceptSharesHandlerCallback);
            }
        }

        private static readonly Dictionary<IntPtr,Action<NSError>> AcceptSharesHandlerCallbacks = new Dictionary<IntPtr,Action<NSError>>();

        [MonoPInvokeCallback(typeof(AcceptSharesCompletionDelegate))]
        private static void AcceptSharesHandlerCallback(IntPtr thisPtr, IntPtr _operationError)
        {
            if(AcceptSharesHandlerCallbacks.TryGetValue(thisPtr, out Action<NSError> callback))
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
                CKAcceptSharesOperation_SetPropPerShareCompletionHandler(Handle, PerShareCompletionHandlerCallback);
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKShareMetadata,CKShare,NSError>> PerShareCompletionHandlerCallbacks = new Dictionary<IntPtr,Action<CKShareMetadata,CKShare,NSError>>();

        [MonoPInvokeCallback(typeof(PerShareCompletionDelegate))]
        private static void PerShareCompletionHandlerCallback(IntPtr thisPtr, IntPtr _shareMetadata, IntPtr _acceptedShare, IntPtr _error)
        {
            if(PerShareCompletionHandlerCallbacks.TryGetValue(thisPtr, out Action<CKShareMetadata,CKShare,NSError> callback))
            {
                try
                {
                    callback(
                        _shareMetadata == IntPtr.Zero ? null : new CKShareMetadata(_shareMetadata),
                        _acceptedShare == IntPtr.Zero ? null : new CKShare(_acceptedShare),
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
