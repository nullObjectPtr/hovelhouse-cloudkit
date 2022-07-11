//
//  CKQuerySubscription.cs
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
    /// Use this to create push notifications based on a query
    /// </summary>
    public class CKQuerySubscription : CKSubscription, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        
        [DllImport(dll)]
        private static extern IntPtr CKQuerySubscription_initWithRecordType_predicate_options(
            string recordType, 
            IntPtr predicate, 
            long querySubscriptionOptions, 
            out IntPtr exceptionPtr
            );
        
        [DllImport(dll)]
        private static extern IntPtr CKQuerySubscription_initWithRecordType_predicate_subscriptionID_options(
            string recordType, 
            IntPtr predicate, 
            string subscriptionID, 
            long querySubscriptionOptions, 
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern IntPtr CKQuerySubscription_GetPropPredicate(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern CKQuerySubscriptionOptions CKQuerySubscription_GetPropQuerySubscriptionOptions(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr CKQuerySubscription_GetPropRecordType(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr CKQuerySubscription_GetPropZoneID(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKQuerySubscription_SetPropZoneID(HandleRef ptr, IntPtr zoneID, out IntPtr exceptionPtr);

        

        #endregion

        internal CKQuerySubscription(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKQuerySubscription(
            string recordType, 
            NSPredicate predicate, 
            CKQuerySubscriptionOptions querySubscriptionOptions
            )
        {
            if(recordType == null)
                throw new ArgumentNullException(nameof(recordType));
            if(predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            
            IntPtr ptr = CKQuerySubscription_initWithRecordType_predicate_options(
                recordType, 
                predicate != null ? HandleRef.ToIntPtr(predicate.Handle) : IntPtr.Zero, 
                (long) querySubscriptionOptions, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKQuerySubscription(
            string recordType, 
            NSPredicate predicate, 
            string subscriptionID, 
            CKQuerySubscriptionOptions querySubscriptionOptions
            )
        {
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
                (long) querySubscriptionOptions, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        
        /// <value>Predicate</value>
        public NSPredicate Predicate
        {
            get 
            { 
                IntPtr predicate = CKQuerySubscription_GetPropPredicate(Handle);
                return predicate == IntPtr.Zero ? null : new NSPredicate(predicate);
            }
        }

        
        /// <value>QuerySubscriptionOptions</value>
        public CKQuerySubscriptionOptions QuerySubscriptionOptions
        {
            get 
            { 
                CKQuerySubscriptionOptions querySubscriptionOptions = CKQuerySubscription_GetPropQuerySubscriptionOptions(Handle);
                return querySubscriptionOptions;
            }
        }

        
        /// <value>RecordType</value>
        public string RecordType
        {
            get 
            { 
                IntPtr recordType = CKQuerySubscription_GetPropRecordType(Handle);
                return Marshal.PtrToStringAuto(recordType);
            }
        }

        
        /// <value>ZoneID</value>
        public CKRecordZoneID ZoneID
        {
            get 
            { 
                IntPtr zoneID = CKQuerySubscription_GetPropZoneID(Handle);
                return zoneID == IntPtr.Zero ? null : new CKRecordZoneID(zoneID);
            }
            set
            {
                CKQuerySubscription_SetPropZoneID(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero, out IntPtr exceptionPtr);
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKQuerySubscription_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected override void Dispose(bool disposing)
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
        public new void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
        
    }
}
