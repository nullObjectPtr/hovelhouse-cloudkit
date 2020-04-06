//
//  ICloudNotifications.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 03/26/2020
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
    /// An OS-level notification that is triggered when the current iCloud identity changes
    /// </summary>
    public class ICloudNotifications : CKObject
    {
        #region dll

        // Class Methods
        

        

        

        

        // Properties
        

        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr AddNSUbiquityIdentityDidChangeNotificationObserver(NotificationDelegate handler, ref IntPtr exceptionPtr);

        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void RemoveNSUbiquityIdentityDidChangeNotificationObserver(HandleRef observerHandle, ref IntPtr exceptionPtr);
        

        #endregion

        internal ICloudNotifications(IntPtr ptr) : base(ptr) {}
        
        
        
        


        
        
        

        
        private static readonly Dictionary<IntPtr,Action<NSNotification>> IdentityDidChangeHandlers = new Dictionary<IntPtr,Action<NSNotification>>();

        [MonoPInvokeCallback(typeof(NotificationDelegate))]
        private static void NSUbiquityIdentityDidChangeNotificationStaticHandler(IntPtr ptr, IntPtr notification)
        {
            Action<NSNotification> handler = null;
            if(IdentityDidChangeHandlers.TryGetValue(ptr, out handler))
            {
                Dispatcher.Instance.EnqueueOnMainThread(() => {
                    handler.Invoke(ptr == IntPtr.Zero ? null : new NSNotification(notification));
                });
            }
        }

        public static Unsubscriber AddIdentityDidChangeObserver(Action<NSNotification> observer)
        {
            IntPtr exceptionPtr = IntPtr.Zero;

            IntPtr observerHandle = AddNSUbiquityIdentityDidChangeNotificationObserver(NSUbiquityIdentityDidChangeNotificationStaticHandler, ref exceptionPtr);
            
            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            IdentityDidChangeHandlers[observerHandle] = observer;

            return observerHandle == IntPtr.Zero ? null : new Unsubscriber(observerHandle);
        }
        
        public static void RemoveIdentityDidChangeObserver(Unsubscriber unsubscriber)
        {
            IntPtr exceptionPtr = IntPtr.Zero;
            RemoveNSUbiquityIdentityDidChangeNotificationObserver(unsubscriber.Handle, ref exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            IdentityDidChangeHandlers.Remove(HandleRef.ToIntPtr(unsubscriber.Handle));
        }
        

        
    }
}
