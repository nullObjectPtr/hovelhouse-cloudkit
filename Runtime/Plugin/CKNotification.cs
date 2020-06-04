//
//  CKNotification.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 05/28/2020
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
    /// <summary>
    /// A push notification sent to the app
    /// </summary>
    public class CKNotification : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        

        

        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr CKNotification_GetPropNotificationID(HandleRef ptr);

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern CKNotificationType CKNotification_GetPropNotificationType(HandleRef ptr);

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr CKNotification_GetPropContainerIdentifier(HandleRef ptr);

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern bool CKNotification_GetPropIsPruned(HandleRef ptr);

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr CKNotification_GetPropSubscriptionID(HandleRef ptr);

        

        #endregion

        internal CKNotification(IntPtr ptr) : base(ptr) {}
        
        
        
        


        
        
        
        /// <value>NotificationID</value>
        public CKNotificationID NotificationID
        {
            get 
            { 
                IntPtr notificationID = CKNotification_GetPropNotificationID(Handle);
                return notificationID == IntPtr.Zero ? null : new CKNotificationID(notificationID);
            }
        }

        
        /// <value>NotificationType</value>
        public CKNotificationType NotificationType
        {
            get 
            { 
                CKNotificationType notificationType = CKNotification_GetPropNotificationType(Handle);
                return notificationType;
            }
        }

        
        /// <value>ContainerIdentifier</value>
        public string ContainerIdentifier
        {
            get 
            { 
                IntPtr containerIdentifier = CKNotification_GetPropContainerIdentifier(Handle);
                return Marshal.PtrToStringAuto(containerIdentifier);
            }
        }

        
        /// <value>IsPruned</value>
        public bool IsPruned
        {
            get 
            { 
                bool isPruned = CKNotification_GetPropIsPruned(Handle);
                return isPruned;
            }
        }

        
        /// <value>SubscriptionID</value>
        public string SubscriptionID
        {
            get 
            { 
                IntPtr subscriptionID = CKNotification_GetPropSubscriptionID(Handle);
                return Marshal.PtrToStringAuto(subscriptionID);
            }
        }

        

        

        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern void CKNotification_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKNotification Dispose");
                CKNotification_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKNotification()
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
