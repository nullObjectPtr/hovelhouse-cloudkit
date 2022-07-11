//
//  CKOperation.cs
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
    /// The base class for all container and database operations. You do not instantiate this class directly. Use one of the concrete classes instead.
    /// </summary>
    public class CKOperation : CKObject, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern IntPtr CKOperation_GetPropConfiguration(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKOperation_SetPropConfiguration(HandleRef ptr, IntPtr configuration, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr CKOperation_GetPropOperationID(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr CKOperation_GetPropGroup(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKOperation_SetPropGroup(HandleRef ptr, IntPtr group, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern NSOperationQueuePriority CKOperation_GetPropQueuePriority(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKOperation_SetPropQueuePriority(HandleRef ptr, long queuePriority, out IntPtr exceptionPtr);

        

        #endregion

        internal CKOperation(IntPtr ptr) : base(ptr) {}
        internal CKOperation(){}
        
        
        
        


        
        
        
        /// <value>Configuration</value>
        public CKOperationConfiguration Configuration
        {
            get 
            { 
                IntPtr configuration = CKOperation_GetPropConfiguration(Handle);
                return configuration == IntPtr.Zero ? null : new CKOperationConfiguration(configuration);
            }
            set
            {
                CKOperation_SetPropConfiguration(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>OperationID</value>
        public string OperationID
        {
            get 
            { 
                IntPtr operationID = CKOperation_GetPropOperationID(Handle);
                return Marshal.PtrToStringAuto(operationID);
            }
        }

        
        /// <value>Group</value>
        public CKOperationGroup Group
        {
            get 
            { 
                IntPtr group = CKOperation_GetPropGroup(Handle);
                return group == IntPtr.Zero ? null : new CKOperationGroup(group);
            }
            set
            {
                CKOperation_SetPropGroup(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>QueuePriority</value>
        public NSOperationQueuePriority QueuePriority
        {
            get 
            { 
                NSOperationQueuePriority queuePriority = CKOperation_GetPropQueuePriority(Handle);
                return queuePriority;
            }
            set
            {
                CKOperation_SetPropQueuePriority(Handle, (long) value, out IntPtr exceptionPtr);
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKOperation_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKOperation Dispose");
                CKOperation_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKOperation()
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
