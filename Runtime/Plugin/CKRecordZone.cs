//
//  CKRecordZone.cs
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
    public class CKRecordZone : CKObject
    {
        #region dll

        // Class Methods
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKRecordZone_defaultRecordZone();
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKRecordZone_initWithZoneName(
            string zoneName);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKRecordZone_initWithZoneID(
            IntPtr zoneID);
        

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
        
        public static CKRecordZone defaultRecordZone()
        {
            
            var val = CKRecordZone_defaultRecordZone();
            return val == IntPtr.Zero ? null : new CKRecordZone(val);
        }
        
        #endregion

        #region Constructors
        
        public static CKRecordZone initWithZoneName(
            string zoneName
        ){
            if(zoneName == null)
                throw new ArgumentNullException(nameof(zoneName));
            
            IntPtr ptr = CKRecordZone_initWithZoneName(
                zoneName);
            return new CKRecordZone(ptr);
        }
        
        
        public static CKRecordZone initWithZoneID(
            CKRecordZoneID zoneID
        ){
            if(zoneID == null)
                throw new ArgumentNullException(nameof(zoneID));
            
            IntPtr ptr = CKRecordZone_initWithZoneID(
                zoneID != null ? HandleRef.ToIntPtr(zoneID.Handle) : IntPtr.Zero);
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
    }
}
