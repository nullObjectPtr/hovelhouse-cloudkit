//
//  CKDatabaseSubscription.cs
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
    /// TODO
    /// </summary>
    /// <remarks>
    /// TODO
    /// </remarks>
    public class CKDatabaseSubscription : CKSubscription, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        
        [DllImport(dll)]
        private static extern IntPtr CKDatabaseSubscription_init(
            out IntPtr exceptionPtr
            );
        
        [DllImport(dll)]
        private static extern IntPtr CKDatabaseSubscription_initWithSubscriptionID(
            string subscriptionID, 
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern IntPtr CKDatabaseSubscription_GetPropRecordType(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKDatabaseSubscription_SetPropRecordType(HandleRef ptr, string recordType, out IntPtr exceptionPtr);

        

        #endregion

        internal CKDatabaseSubscription(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKDatabaseSubscription(
            )
        {
            
            IntPtr ptr = CKDatabaseSubscription_init(
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKDatabaseSubscription(
            string subscriptionID
            )
        {
            
            IntPtr ptr = CKDatabaseSubscription_initWithSubscriptionID(
                subscriptionID, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        
        /// <value>RecordType</value>
        public string RecordType
        {
            get 
            { 
                IntPtr recordType = CKDatabaseSubscription_GetPropRecordType(Handle);
                return Marshal.PtrToStringAuto(recordType);
            }
            set
            {
                CKDatabaseSubscription_SetPropRecordType(Handle, value, out IntPtr exceptionPtr);
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKDatabaseSubscription_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKDatabaseSubscription Dispose");
                CKDatabaseSubscription_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKDatabaseSubscription()
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
