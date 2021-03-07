//
//  CKFetchSubscriptionsOperation.cs
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
    /// A container operation that fetches the participants for shared records
    /// </summary>
    public class CKFetchSubscriptionsOperation : CKDatabaseOperation, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        
        [DllImport(dll)]
        private static extern IntPtr CKFetchSubscriptionsOperation_init(
            out IntPtr exceptionPtr
            );
        
        [DllImport(dll)]
        private static extern IntPtr CKFetchSubscriptionsOperation_initWithSubscriptionIDs(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 2)]
            string[] subscriptionIDs,
			int subscriptionIDsCount, 
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        

        #endregion

        internal CKFetchSubscriptionsOperation(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKFetchSubscriptionsOperation(
            )
        {
            
            IntPtr ptr = CKFetchSubscriptionsOperation_init(
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKFetchSubscriptionsOperation(
            string[] subscriptionIDs
            )
        {
            
            IntPtr ptr = CKFetchSubscriptionsOperation_initWithSubscriptionIDs(
                subscriptionIDs == null ? null : subscriptionIDs,
				subscriptionIDs == null ? 0 : subscriptionIDs.Length, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKFetchSubscriptionsOperation_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKFetchSubscriptionsOperation Dispose");
                CKFetchSubscriptionsOperation_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKFetchSubscriptionsOperation()
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
