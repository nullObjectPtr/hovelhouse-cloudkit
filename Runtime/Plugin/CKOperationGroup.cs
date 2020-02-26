//
//  CKOperationGroup.cs
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
    public class CKOperationGroup : CKObject
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKOperationGroup_init();
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKOperationGroup_initWithCoder(
            IntPtr aDecoder);
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKOperationGroup_GetPropDefaultConfiguration(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKOperationGroup_SetPropDefaultConfiguration(HandleRef ptr, IntPtr defaultConfiguration);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern CKOperationGroupTransferSize CKOperationGroup_GetPropExpectedReceiveSize(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKOperationGroup_SetPropExpectedReceiveSize(HandleRef ptr, long expectedReceiveSize);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern CKOperationGroupTransferSize CKOperationGroup_GetPropExpectedSendSize(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKOperationGroup_SetPropExpectedSendSize(HandleRef ptr, long expectedSendSize);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKOperationGroup_GetPropName(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKOperationGroup_SetPropName(HandleRef ptr, string name);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKOperationGroup_GetPropOperationGroupID(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern ulong CKOperationGroup_GetPropQuantity(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKOperationGroup_SetPropQuantity(HandleRef ptr, ulong quantity);
        
        #endregion

        internal CKOperationGroup(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKOperationGroup init(
        ){
            
            IntPtr ptr = CKOperationGroup_init();
            return new CKOperationGroup(ptr);
        }
        
        
        public static CKOperationGroup initWithCoder(
            NSCoder aDecoder
        ){
            if(aDecoder == null)
                throw new ArgumentNullException(nameof(aDecoder));
            
            IntPtr ptr = CKOperationGroup_initWithCoder(
                aDecoder != null ? HandleRef.ToIntPtr(aDecoder.Handle) : IntPtr.Zero);
            return new CKOperationGroup(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public CKOperationConfiguration DefaultConfiguration 
        {
            get 
            { 
                IntPtr defaultConfiguration = CKOperationGroup_GetPropDefaultConfiguration(Handle);
                return defaultConfiguration == IntPtr.Zero ? null : new CKOperationConfiguration(defaultConfiguration);
            }
            set
            {
                CKOperationGroup_SetPropDefaultConfiguration(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero);
            }
        }
        
        public CKOperationGroupTransferSize ExpectedReceiveSize 
        {
            get 
            { 
                CKOperationGroupTransferSize expectedReceiveSize = CKOperationGroup_GetPropExpectedReceiveSize(Handle);
                return expectedReceiveSize;
            }
            set
            {
                CKOperationGroup_SetPropExpectedReceiveSize(Handle, (long) value);
            }
        }
        
        public CKOperationGroupTransferSize ExpectedSendSize 
        {
            get 
            { 
                CKOperationGroupTransferSize expectedSendSize = CKOperationGroup_GetPropExpectedSendSize(Handle);
                return expectedSendSize;
            }
            set
            {
                CKOperationGroup_SetPropExpectedSendSize(Handle, (long) value);
            }
        }
        
        public string Name 
        {
            get 
            { 
                IntPtr name = CKOperationGroup_GetPropName(Handle);
                return Marshal.PtrToStringAuto(name);
            }
            set
            {
                CKOperationGroup_SetPropName(Handle, value);
            }
        }
        
        public string OperationGroupID 
        {
            get 
            { 
                IntPtr operationGroupID = CKOperationGroup_GetPropOperationGroupID(Handle);
                return Marshal.PtrToStringAuto(operationGroupID);
            }
        }
        
        public ulong Quantity 
        {
            get 
            { 
                ulong quantity = CKOperationGroup_GetPropQuantity(Handle);
                return quantity;
            }
            set
            {
                CKOperationGroup_SetPropQuantity(Handle, value);
            }
        }
        
        #endregion
    }
}
