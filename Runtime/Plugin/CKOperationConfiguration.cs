//
//  CKOperationConfiguration.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 03/13/2020
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
    public class CKOperationConfiguration : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        

        

        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern bool CKOperationConfiguration_GetPropAllowsCellularAccess(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKOperationConfiguration_SetPropAllowsCellularAccess(HandleRef ptr, bool allowsCellularAccess, out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKOperationConfiguration_GetPropContainer(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKOperationConfiguration_SetPropContainer(HandleRef ptr, IntPtr container, out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern NSQualityOfService CKOperationConfiguration_GetPropQualityOfService(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKOperationConfiguration_SetPropQualityOfService(HandleRef ptr, long qualityOfService, out IntPtr exceptionPtr);
        

        #endregion

        internal CKOperationConfiguration(IntPtr ptr) : base(ptr) {}
        
        
        
        


        
        
        
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
            }
        }
        
        public CKContainer Container 
        {
            get 
            { 
                IntPtr container = CKOperationConfiguration_GetPropContainer(Handle);
                return container == IntPtr.Zero ? null : new CKContainer(container);
            }
            set
            {
                CKOperationConfiguration_SetPropContainer(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero, out IntPtr exceptionPtr);
            }
        }
        
        public NSQualityOfService QualityOfService 
        {
            get 
            { 
                NSQualityOfService qualityOfService = CKOperationConfiguration_GetPropQualityOfService(Handle);
                return qualityOfService;
            }
            set
            {
                CKOperationConfiguration_SetPropQualityOfService(Handle, (long) value, out IntPtr exceptionPtr);
            }
        }
        

        

        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKOperationConfiguration_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
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
