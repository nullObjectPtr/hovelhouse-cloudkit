//
//  CKOperation.cs
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
    public class CKOperation : CKObject
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKOperation_init();
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKOperation_GetPropConfiguration(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKOperation_SetPropConfiguration(HandleRef ptr, IntPtr configuration);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKOperation_GetPropOperationID(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKOperation_GetPropGroup(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKOperation_SetPropGroup(HandleRef ptr, IntPtr group);
        
        #endregion

        internal CKOperation(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKOperation init(
        ){
            
            IntPtr ptr = CKOperation_init();
            return new CKOperation(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public CKOperationConfiguration Configuration 
        {
            get 
            { 
                IntPtr configuration = CKOperation_GetPropConfiguration(Handle);
                return configuration == IntPtr.Zero ? null : new CKOperationConfiguration(configuration);
            }
            set
            {
                CKOperation_SetPropConfiguration(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero);
            }
        }
        
        public string OperationID 
        {
            get 
            { 
                IntPtr operationID = CKOperation_GetPropOperationID(Handle);
                return Marshal.PtrToStringAuto(operationID);
            }
        }
        
        public CKOperationGroup Group 
        {
            get 
            { 
                IntPtr group = CKOperation_GetPropGroup(Handle);
                return group == IntPtr.Zero ? null : new CKOperationGroup(group);
            }
            set
            {
                CKOperation_SetPropGroup(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero);
            }
        }
        
        #endregion
    }
}
