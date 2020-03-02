//
//  CKSubscription.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 03/02/2020
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
    public class CKSubscription : CKObject
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
        private static extern IntPtr CKSubscription_GetPropNotificationInfo(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKSubscription_SetPropNotificationInfo(HandleRef ptr, IntPtr notificationInfo);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern CKSubscriptionType CKSubscription_GetPropSubscriptionType(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKSubscription_GetPropSubscriptionID(HandleRef ptr);
        
        #endregion

        internal CKSubscription(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public CKNotificationInfo NotificationInfo 
        {
            get 
            { 
                IntPtr notificationInfo = CKSubscription_GetPropNotificationInfo(Handle);
                return notificationInfo == IntPtr.Zero ? null : new CKNotificationInfo(notificationInfo);
            }
            set
            {
                CKSubscription_SetPropNotificationInfo(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero);
            }
        }
        
        public CKSubscriptionType SubscriptionType 
        {
            get 
            { 
                CKSubscriptionType subscriptionType = CKSubscription_GetPropSubscriptionType(Handle);
                return subscriptionType;
            }
        }
        
        public string SubscriptionID 
        {
            get 
            { 
                IntPtr subscriptionID = CKSubscription_GetPropSubscriptionID(Handle);
                return Marshal.PtrToStringAuto(subscriptionID);
            }
        }
        
        #endregion
        
        
    }
}
