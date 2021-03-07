//
//  CKSubscription.cs
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
    /// Represents a subscription to a push notification
    /// </summary>
    public class CKSubscription : CKObject, IDisposable
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
        private static extern IntPtr CKSubscription_GetPropNotificationInfo(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKSubscription_SetPropNotificationInfo(HandleRef ptr, IntPtr notificationInfo, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern CKSubscriptionType CKSubscription_GetPropSubscriptionType(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr CKSubscription_GetPropSubscriptionID(HandleRef ptr);

        

        #endregion

        internal CKSubscription(IntPtr ptr) : base(ptr) {}
        internal CKSubscription(){}
        
        
        
        


        
        
        
        /// <value>NotificationInfo</value>
        public CKNotificationInfo NotificationInfo
        {
            get 
            { 
                IntPtr notificationInfo = CKSubscription_GetPropNotificationInfo(Handle);
                return notificationInfo == IntPtr.Zero ? null : new CKNotificationInfo(notificationInfo);
            }
            set
            {
                CKSubscription_SetPropNotificationInfo(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>SubscriptionType</value>
        public CKSubscriptionType SubscriptionType
        {
            get 
            { 
                CKSubscriptionType subscriptionType = CKSubscription_GetPropSubscriptionType(Handle);
                return subscriptionType;
            }
        }

        
        /// <value>SubscriptionID</value>
        public string SubscriptionID
        {
            get 
            { 
                IntPtr subscriptionID = CKSubscription_GetPropSubscriptionID(Handle);
                return Marshal.PtrToStringAuto(subscriptionID);
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKSubscription_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKSubscription Dispose");
                CKSubscription_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKSubscription()
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
