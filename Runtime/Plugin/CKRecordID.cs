//
//  CKRecordID.cs
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
    public class CKRecordID : CKObject
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKRecordID_initWithRecordName(
            string recordName);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKRecordID_initWithRecordName_zoneID(
            string recordName, 
            IntPtr zoneID);
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKRecordID_GetPropRecordName(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKRecordID_GetPropZoneID(HandleRef ptr);
        
        #endregion

        internal CKRecordID(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKRecordID initWithRecordName(
            string recordName
        ){
            if(recordName == null)
                throw new ArgumentNullException(nameof(recordName));
            
            IntPtr ptr = CKRecordID_initWithRecordName(
                recordName);
            return new CKRecordID(ptr);
        }
        
        
        public static CKRecordID initWithRecordName(
            string recordName, 
            CKRecordZoneID zoneID
        ){
            if(recordName == null)
                throw new ArgumentNullException(nameof(recordName));
            if(zoneID == null)
                throw new ArgumentNullException(nameof(zoneID));
            
            IntPtr ptr = CKRecordID_initWithRecordName_zoneID(
                recordName,
                zoneID != null ? HandleRef.ToIntPtr(zoneID.Handle) : IntPtr.Zero);
            return new CKRecordID(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public string RecordName 
        {
            get 
            { 
                IntPtr recordName = CKRecordID_GetPropRecordName(Handle);
                return Marshal.PtrToStringAuto(recordName);
            }
        }
        
        public CKRecordZoneID ZoneID 
        {
            get 
            { 
                IntPtr zoneID = CKRecordID_GetPropZoneID(Handle);
                return zoneID == IntPtr.Zero ? null : new CKRecordZoneID(zoneID);
            }
        }
        
        #endregion
    }
}
