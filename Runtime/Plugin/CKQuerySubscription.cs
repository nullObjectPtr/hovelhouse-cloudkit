//
//  CKQuerySubscription.cs
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
    public class CKQuerySubscription : CKSubscription, IDisposable
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
        
        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKQuerySubscription_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        // No base.Dispose() needed
        // All we ever do is decrement the reference count in managed code
        
        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKQuerySubscription Dispose");
                CKQuerySubscription_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKQuerySubscription()
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
