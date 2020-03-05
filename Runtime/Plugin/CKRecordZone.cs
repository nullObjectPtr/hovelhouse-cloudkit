//
//  CKRecordZone.cs
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
    public class CKRecordZone : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKRecordZone_defaultRecordZone(
            out IntPtr exceptionPtr);
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKRecordZone_initWithZoneName(
            string zoneName, 
            out IntPtr exceptionPtr
            );
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKRecordZone_initWithZoneID(
            IntPtr zoneID, 
            out IntPtr exceptionPtr
            );
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKRecordZone_GetPropZoneID(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern CKRecordZoneCapabilities CKRecordZone_GetPropCapabilities(HandleRef ptr);
        
        #endregion

        internal CKRecordZone(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        
        public static CKRecordZone DefaultRecordZone()
        { 
            var val = CKRecordZone_defaultRecordZone(out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val == IntPtr.Zero ? null : new CKRecordZone(val);
        }
        

        
        #endregion

        #region Constructors
        
        public static CKRecordZone initWithZoneName(
            string zoneName
            )
        {
            if(zoneName == null)
                throw new ArgumentNullException(nameof(zoneName));
            
            IntPtr ptr = CKRecordZone_initWithZoneName(
                zoneName, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            return new CKRecordZone(ptr);
        }
        
        
        public static CKRecordZone initWithZoneID(
            CKRecordZoneID zoneID
            )
        {
            if(zoneID == null)
                throw new ArgumentNullException(nameof(zoneID));
            
            IntPtr ptr = CKRecordZone_initWithZoneID(
                zoneID != null ? HandleRef.ToIntPtr(zoneID.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            return new CKRecordZone(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public CKRecordZoneID ZoneID 
        {
            get 
            { 
                IntPtr zoneID = CKRecordZone_GetPropZoneID(Handle);
                return zoneID == IntPtr.Zero ? null : new CKRecordZoneID(zoneID);
            }
        }
        
        public CKRecordZoneCapabilities Capabilities 
        {
            get 
            { 
                CKRecordZoneCapabilities capabilities = CKRecordZone_GetPropCapabilities(Handle);
                return capabilities;
            }
        }
        
        #endregion
        
        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKRecordZone_Dispose(HandleRef handle);
            
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
                
                //Debug.Log("CKRecordZone Dispose");
                CKRecordZone_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKRecordZone()
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
