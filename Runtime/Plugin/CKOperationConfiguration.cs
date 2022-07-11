//
//  CKOperationConfiguration.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on
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
    /// Specifies options for container and database operations, such as quality of service and timeout intervals
    /// </summary>
    /// <remarks>
    /// Database operations do not have sensible defaults of their QualityOfService. Be sure to set the QualityOfService to UserInitiated to ensure that your operations are executed in a timely manner.
    /// </remarks>
    public class CKOperationConfiguration : CKObject, IDisposable
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
        private static extern bool CKOperationConfiguration_GetPropLongLived(
            HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKOperationConfiguration_SetPropLongLived(HandleRef ptr, bool longLived, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern double CKOperationConfiguration_GetPropTimeoutIntervalForRequest(
            HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKOperationConfiguration_SetPropTimeoutIntervalForRequest(HandleRef ptr, double timeoutIntervalForRequest, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern double CKOperationConfiguration_GetPropTimeoutIntervalForResource(
            HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKOperationConfiguration_SetPropTimeoutIntervalForResource(HandleRef ptr, double timeoutIntervalForResource, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern bool CKOperationConfiguration_GetPropAllowsCellularAccess(
            HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKOperationConfiguration_SetPropAllowsCellularAccess(HandleRef ptr, bool allowsCellularAccess, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr CKOperationConfiguration_GetPropContainer(
            HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKOperationConfiguration_SetPropContainer(HandleRef ptr, IntPtr container, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern NSQualityOfService CKOperationConfiguration_GetPropQualityOfService(
            HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKOperationConfiguration_SetPropQualityOfService(HandleRef ptr, long qualityOfService, out IntPtr exceptionPtr);

        

        #endregion

        internal CKOperationConfiguration(IntPtr ptr) : base(ptr) {}
        
        
        


        
        
        
        /// <value>LongLived</value>
        public bool LongLived
        {
            get
            {
                bool longLived = CKOperationConfiguration_GetPropLongLived(Handle);
                
                return longLived;
            }
            set
            {
                CKOperationConfiguration_SetPropLongLived(Handle, value, out IntPtr exceptionPtr);
                if(exceptionPtr != IntPtr.Zero)
                {
                    var nsexception = new NSException(exceptionPtr);
                    throw new CloudKitException(nsexception, nsexception.Reason);
                }
            }
        }

        
        /// <value>TimeoutIntervalForRequest</value>
        public double TimeoutIntervalForRequest
        {
            get
            {
                double timeoutIntervalForRequest = CKOperationConfiguration_GetPropTimeoutIntervalForRequest(Handle);
                
                return timeoutIntervalForRequest;
            }
            set
            {
                CKOperationConfiguration_SetPropTimeoutIntervalForRequest(Handle, value, out IntPtr exceptionPtr);
                if(exceptionPtr != IntPtr.Zero)
                {
                    var nsexception = new NSException(exceptionPtr);
                    throw new CloudKitException(nsexception, nsexception.Reason);
                }
            }
        }

        
        /// <value>TimeoutIntervalForResource</value>
        public double TimeoutIntervalForResource
        {
            get
            {
                double timeoutIntervalForResource = CKOperationConfiguration_GetPropTimeoutIntervalForResource(Handle);
                
                return timeoutIntervalForResource;
            }
            set
            {
                CKOperationConfiguration_SetPropTimeoutIntervalForResource(Handle, value, out IntPtr exceptionPtr);
                if(exceptionPtr != IntPtr.Zero)
                {
                    var nsexception = new NSException(exceptionPtr);
                    throw new CloudKitException(nsexception, nsexception.Reason);
                }
            }
        }

        
        /// <value>AllowsCellularAccess</value>
        public bool AllowsCellularAccess
        {
            get
            {
                bool allowsCellularAccess = CKOperationConfiguration_GetPropAllowsCellularAccess(Handle);
                
                return allowsCellularAccess;
            }
            set
            {
                CKOperationConfiguration_SetPropAllowsCellularAccess(Handle, value, out IntPtr exceptionPtr);
                if(exceptionPtr != IntPtr.Zero)
                {
                    var nsexception = new NSException(exceptionPtr);
                    throw new CloudKitException(nsexception, nsexception.Reason);
                }
            }
        }

        
        /// <value>Container</value>
        private CKContainer _container;
        public CKContainer Container
        {
            get
            {
                var container = CKOperationConfiguration_GetPropContainer(Handle);
                if(_container == null || container != (IntPtr)_container.Handle)
                {
                    _container = container == IntPtr.Zero ? null : new CKContainer(container);
                }
                
                return _container;
            }
            set
            {
                CKOperationConfiguration_SetPropContainer(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero, out IntPtr exceptionPtr);
                if(exceptionPtr != IntPtr.Zero)
                {
                    var nsexception = new NSException(exceptionPtr);
                    throw new CloudKitException(nsexception, nsexception.Reason);
                }
            }
        }

        
        /// <value>QualityOfService</value>
        public NSQualityOfService QualityOfService
        {
            get
            {
                NSQualityOfService qualityOfService = CKOperationConfiguration_GetPropQualityOfService(Handle);
                
                return (NSQualityOfService) qualityOfService;
            }
            set
            {
                CKOperationConfiguration_SetPropQualityOfService(Handle, (long) value, out IntPtr exceptionPtr);
                if(exceptionPtr != IntPtr.Zero)
                {
                    var nsexception = new NSException(exceptionPtr);
                    throw new CloudKitException(nsexception, nsexception.Reason);
                }
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKOperationConfiguration_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                //Debug.Log("CKOperationConfiguration Dispose");
                CKOperationConfiguration_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKOperationConfiguration()
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
