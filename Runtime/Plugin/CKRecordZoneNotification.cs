//
//  CKRecordZoneNotification.cs
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
    public class CKRecordZoneNotification : CKNotification
    {
        #region dll

        // Class Methods
        

        // Constructors
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKRecordZoneNotification_GetPropRecordZoneID(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern CKDatabaseScope CKRecordZoneNotification_GetPropDatabaseScope(HandleRef ptr);
        
        #endregion

        internal CKRecordZoneNotification(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public CKRecordZoneID RecordZoneID 
        {
            get 
            { 
                IntPtr recordZoneID = CKRecordZoneNotification_GetPropRecordZoneID(Handle);
                return recordZoneID == IntPtr.Zero ? null : new CKRecordZoneID(recordZoneID);
            }
        }
        
        public CKDatabaseScope DatabaseScope 
        {
            get 
            { 
                CKDatabaseScope databaseScope = CKRecordZoneNotification_GetPropDatabaseScope(Handle);
                return databaseScope;
            }
        }
        
        #endregion
    }
}
