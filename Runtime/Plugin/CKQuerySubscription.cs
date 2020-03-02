//
//  CKQuerySubscription.cs
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
    public class CKQuerySubscription : CKSubscription
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKQuerySubscription_initWithRecordType_predicate_options(
            string recordType, 
            IntPtr predicate, 
            long querySubscriptionOptions);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKQuerySubscription_initWithRecordType_predicate_subscriptionID_options(
            string recordType, 
            IntPtr predicate, 
            string subscriptionID, 
            long querySubscriptionOptions);
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKQuerySubscription_GetPropPredicate(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern CKQuerySubscriptionOptions CKQuerySubscription_GetPropQuerySubscriptionOptions(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKQuerySubscription_GetPropRecordType(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKQuerySubscription_GetPropZoneID(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKQuerySubscription_SetPropZoneID(HandleRef ptr, IntPtr zoneID);
        
        #endregion

        internal CKQuerySubscription(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKQuerySubscription initWithRecordType(
            string recordType, 
            NSPredicate predicate, 
            CKQuerySubscriptionOptions querySubscriptionOptions
        ){
            if(recordType == null)
                throw new ArgumentNullException(nameof(recordType));
            if(predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            
            IntPtr ptr = CKQuerySubscription_initWithRecordType_predicate_options(
                recordType,
                predicate != null ? HandleRef.ToIntPtr(predicate.Handle) : IntPtr.Zero,
                (long) querySubscriptionOptions);
            return new CKQuerySubscription(ptr);
        }
        
        
        public static CKQuerySubscription initWithRecordType(
            string recordType, 
            NSPredicate predicate, 
            string subscriptionID, 
            CKQuerySubscriptionOptions querySubscriptionOptions
        ){
            if(recordType == null)
                throw new ArgumentNullException(nameof(recordType));
            if(predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            if(subscriptionID == null)
                throw new ArgumentNullException(nameof(subscriptionID));
            
            IntPtr ptr = CKQuerySubscription_initWithRecordType_predicate_subscriptionID_options(
                recordType,
                predicate != null ? HandleRef.ToIntPtr(predicate.Handle) : IntPtr.Zero,
                subscriptionID,
                (long) querySubscriptionOptions);
            return new CKQuerySubscription(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public NSPredicate Predicate 
        {
            get 
            { 
                IntPtr predicate = CKQuerySubscription_GetPropPredicate(Handle);
                return predicate == IntPtr.Zero ? null : new NSPredicate(predicate);
            }
        }
        
        public CKQuerySubscriptionOptions QuerySubscriptionOptions 
        {
            get 
            { 
                CKQuerySubscriptionOptions querySubscriptionOptions = CKQuerySubscription_GetPropQuerySubscriptionOptions(Handle);
                return querySubscriptionOptions;
            }
        }
        
        public string RecordType 
        {
            get 
            { 
                IntPtr recordType = CKQuerySubscription_GetPropRecordType(Handle);
                return Marshal.PtrToStringAuto(recordType);
            }
        }
        
        public CKRecordZoneID ZoneID 
        {
            get 
            { 
                IntPtr zoneID = CKQuerySubscription_GetPropZoneID(Handle);
                return zoneID == IntPtr.Zero ? null : new CKRecordZoneID(zoneID);
            }
            set
            {
                CKQuerySubscription_SetPropZoneID(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero);
            }
        }
        
        #endregion
    }
}
