//
//  ICloudNotifications.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 05/28/2020
//  Copyright © 2020 HovelHouseApps. All rights reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential
//

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;
#if UNITY_IOS
using NotificationServices = UnityEngine.iOS.NotificationServices;
using NotificationType = UnityEngine.iOS.NotificationType;
#endif

namespace HovelHouse.CloudKit
{
    /// <summary>
    /// An OS-level notification that is triggered when the current iCloud identity changes
    /// </summary>
    public class ICloudNotifications : CKObject
    {
        #region dll

        // Class Methods
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern void ICloudNotifications_nada(
            HandleRef ptr, 
            out IntPtr exceptionPtr);

#if UNITY_STANDALONE_OSX
        [DllImport("HHCloudKitMacOS")]
        private static extern void RequestNotificationTokenMacOs(RegisteredForNotificationsCallback callback);
#endif

#if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
#else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern void RegisterForRemoteNotifications(CKNotificationDelegate del);

        

        

        

        

        // Properties
        

        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr AddNSUbiquityIdentityDidChangeNotificationObserver(NotificationDelegate handler, ref IntPtr exceptionPtr);

        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern void RemoveNSUbiquityIdentityDidChangeNotificationObserver(HandleRef observerHandle, ref IntPtr exceptionPtr);
        

        #endregion

        internal ICloudNotifications(IntPtr ptr) : base(ptr) {}

        public static void RequestNotificationToken()
        {
#if UNITY_STANDALONE_OSX
            RequestNotificationTokenMacOs(OnRegisteredForNotifications);
#elif UNITY_IOS
            NotificationServices.RegisterForNotifications(
            NotificationType.Alert |
            NotificationType.Badge |
            NotificationType.Sound,
            true);
#else
            Debug.LogWarning("RequestNotificationToken is not implemented on this platform");
#endif
        }

        [MonoPInvokeCallback(typeof(RegisteredForNotificationsCallback))]
        private static void OnRegisteredForNotifications(byte[] tokenBytes, long length, IntPtr errorPtr)
        {
            NSError error = errorPtr == IntPtr.Zero ? null : new NSError(errorPtr);
            if(error != null)
            {
                Debug.LogError(error.LocalizedDescription);
            }
            else
            {
                string hexToken = "%" + System.BitConverter.ToString(tokenBytes).Replace('-', '%');
                Debug.Log("Successfully registered for notification token: " + hexToken);
            }
        }

        /// <summary>
        /// </summary>
        /// 
        /// <returns>void</returns>
        [MonoPInvokeCallback(typeof(CKNotificationDelegate))]
        private static void _onRemoteNotification(IntPtr ptr, string className)
        {
            CKNotification notification = null;
            Debug.Log("Class name: " + className);

            if (_myRemoteNotificationHandler != null)
            {
                if (className == "CKQueryNotification")
                {
                    notification = new CKQueryNotification(ptr);
                }
                else if (className == "CKRecordZoneNotification")
                {
                    notification = new CKRecordZoneNotification(ptr);
                }
                else if (className == "CKDatabaseNotification")
                {
                    notification = new CKDatabaseNotification(ptr);
                }
                else
                {
                    Debug.LogError("unhandled CKNotification type: " + className);
                }

                if (notification != null)
                    _myRemoteNotificationHandler(notification);
            }
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns>val</returns>
private static Action<CKNotification> _myRemoteNotificationHandler;

        public static void SetRemoteNotificationHandler(Action<CKNotification> p)
        {
            _myRemoteNotificationHandler = p;
            RegisterForRemoteNotifications(_onRemoteNotification);
        }
        

        
        
        


        
        
        

        
        private static readonly Dictionary<IntPtr,ExecutionContext<NSNotification>> IdentityDidChangeHandlers = new Dictionary<IntPtr,ExecutionContext<NSNotification>>();

        [MonoPInvokeCallback(typeof(NotificationDelegate))]
        private static void NSUbiquityIdentityDidChangeNotificationStaticHandler(IntPtr ptr, IntPtr notification)
        {
            if(IdentityDidChangeHandlers.TryGetValue(ptr, out ExecutionContext<NSNotification> handler))
            {
                handler.Invoke(ptr == IntPtr.Zero ? null : new NSNotification(notification));
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

            IdentityDidChangeHandlers[observerHandle] = new ExecutionContext<NSNotification>(observer);

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
