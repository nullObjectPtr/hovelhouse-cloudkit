//
//  CKOperationConfiguration.cs
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
    public class CKOperationConfiguration : CKObject
    {
        #region dll

        // Class Methods
        

        // Constructors
        

        // Instance Methods
        

        

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
        private static extern void CKOperationConfiguration_SetPropAllowsCellularAccess(HandleRef ptr, bool allowsCellularAccess);
        
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
        private static extern void CKOperationConfiguration_SetPropContainer(HandleRef ptr, IntPtr container);
        
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
        private static extern void CKOperationConfiguration_SetPropQualityOfService(HandleRef ptr, long qualityOfService);
        
        #endregion

        internal CKOperationConfiguration(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public bool AllowsCellularAccess 
        {
            get 
            { 
                bool allowsCellularAccess = CKOperationConfiguration_GetPropAllowsCellularAccess(Handle);
                return allowsCellularAccess;
            }
            set
            {
                CKOperationConfiguration_SetPropAllowsCellularAccess(Handle, value);
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
                CKOperationConfiguration_SetPropContainer(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero);
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
                CKOperationConfiguration_SetPropQualityOfService(Handle, (long) value);
            }
        }
        
        #endregion
    }
}
