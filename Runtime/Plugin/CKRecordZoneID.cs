//
//  CKRecordZoneID.cs
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
    public class CKRecordZoneID : CKObject
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
    }
}
