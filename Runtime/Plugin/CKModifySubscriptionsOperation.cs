//
//  CKModifySubscriptionsOperation.cs
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
    /// An operation that creates a subscription on a database
    /// </summary>
    public class CKModifySubscriptionsOperation : CKDatabaseOperation, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        
        [DllImport(dll)]
        private static extern IntPtr CKModifySubscriptionsOperation_init(
            out IntPtr exceptionPtr
            );
        
        [DllImport(dll)]
        private static extern IntPtr CKModifySubscriptionsOperation_initWithSubscriptionsToSave_subscriptionIDsToDelete(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 2)]
            IntPtr[] subscriptionsToSave,
			int subscriptionsToSaveCount, 
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)]
            string[] subscriptionIDsToDelete,
			int subscriptionIDsToDeleteCount, 
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        [DllImport(dll)]
        private static extern void CKModifySubscriptionsOperation_SetPropModifySubscriptionsCompletionBlock(HandleRef ptr, ModifySubscriptionsCompletionDelegate modifySubscriptionsCompletionBlock, out IntPtr exceptionPtr);

        

        #endregion

        internal CKModifySubscriptionsOperation(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKModifySubscriptionsOperation(
            )
        {
            
            IntPtr ptr = CKModifySubscriptionsOperation_init(
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKModifySubscriptionsOperation(
            CKSubscription[] subscriptionsToSave, 
            string[] subscriptionIDsToDelete
            )
        {
            
            IntPtr ptr = CKModifySubscriptionsOperation_initWithSubscriptionsToSave_subscriptionIDsToDelete(
                subscriptionsToSave == null ? null : subscriptionsToSave.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				subscriptionsToSave == null ? 0 : subscriptionsToSave.Length, 
                subscriptionIDsToDelete == null ? null : subscriptionIDsToDelete,
				subscriptionIDsToDelete == null ? 0 : subscriptionIDsToDelete.Length, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        
        /// <value>ModifySubscriptionsCompletionBlock</value>
        public Action<CKSubscription[],string[],NSError> ModifySubscriptionsCompletionBlock
        {
            get 
            {
                ModifySubscriptionsCompletionBlockCallbacks.TryGetValue(
                    HandleRef.ToIntPtr(Handle), 
                    out ExecutionContext<CKSubscription[],string[],NSError> value);
                return value.Callback;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    ModifySubscriptionsCompletionBlockCallbacks.Remove(myPtr);
                }
                else
                {
                    ModifySubscriptionsCompletionBlockCallbacks[myPtr] = new ExecutionContext<CKSubscription[],string[],NSError>(value);
                }
                CKModifySubscriptionsOperation_SetPropModifySubscriptionsCompletionBlock(Handle, ModifySubscriptionsCompletionBlockCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,ExecutionContext<CKSubscription[],string[],NSError>> ModifySubscriptionsCompletionBlockCallbacks = new Dictionary<IntPtr,ExecutionContext<CKSubscription[],string[],NSError>>();

        [MonoPInvokeCallback(typeof(ModifySubscriptionsCompletionDelegate))]
        private static void ModifySubscriptionsCompletionBlockCallback(IntPtr thisPtr, IntPtr[] savedSubscriptions,
		long savedSubscriptionsCount, IntPtr[] deletedSubscriptionIDs,
		long deletedSubscriptionIDsCount, IntPtr operationError)
        {
            if(ModifySubscriptionsCompletionBlockCallbacks.TryGetValue(thisPtr, out ExecutionContext<CKSubscription[],string[],NSError> callback))
            {
                callback.Invoke(
                        savedSubscriptions == null ? null : savedSubscriptions.Select(x => new CKSubscription(x)).ToArray(),
                        deletedSubscriptionIDs == null ? null : deletedSubscriptionIDs.Select(x => Marshal.PtrToStringAuto(x)).ToArray(),
                        operationError == IntPtr.Zero ? null : new NSError(operationError));
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKModifySubscriptionsOperation_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKModifySubscriptionsOperation Dispose");
                CKModifySubscriptionsOperation_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKModifySubscriptionsOperation()
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
