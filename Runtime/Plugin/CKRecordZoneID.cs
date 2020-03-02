//
//  CKRecordZoneID.cs
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
    public class CKRecordZoneID : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKRecordZoneID_initWithZoneName_ownerName(
            string zoneName, 
            string ownerName);
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKRecordZoneID_GetPropZoneName(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKRecordZoneID_GetPropOwnerName(HandleRef ptr);
        
        #endregion

        internal CKRecordZoneID(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKRecordZoneID initWithZoneName(
            string zoneName, 
            string ownerName
        ){
            if(zoneName == null)
                throw new ArgumentNullException(nameof(zoneName));
            if(ownerName == null)
                throw new ArgumentNullException(nameof(ownerName));
            
            IntPtr ptr = CKRecordZoneID_initWithZoneName_ownerName(
                zoneName,
                ownerName);
            return new CKRecordZoneID(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public string ZoneName 
        {
            get 
            { 
                IntPtr zoneName = CKRecordZoneID_GetPropZoneName(Handle);
                return Marshal.PtrToStringAuto(zoneName);
            }
        }
        
        public string OwnerName 
        {
            get 
            { 
                IntPtr ownerName = CKRecordZoneID_GetPropOwnerName(Handle);
                return Marshal.PtrToStringAuto(ownerName);
            }
        }
        
        #endregion
        
        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKRecordZoneID_Dispose(HandleRef handle);
            
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
                
                //Debug.Log("CKRecordZoneID Dispose");
                CKRecordZoneID_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKRecordZoneID()
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
