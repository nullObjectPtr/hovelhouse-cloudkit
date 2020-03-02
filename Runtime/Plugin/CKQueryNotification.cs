//
//  CKQueryNotification.cs
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
    public class CKQueryNotification : CKSubscription
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
        private static extern CKDatabaseScope CKQueryNotification_GetPropDatabaseScope(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern CKQueryNotificationReason CKQueryNotification_GetPropQueryNotificationReason(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKQueryNotification_GetPropRecordID(HandleRef ptr);
        
        #endregion

        internal CKQueryNotification(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public CKDatabaseScope DatabaseScope 
        {
            get 
            { 
                CKDatabaseScope databaseScope = CKQueryNotification_GetPropDatabaseScope(Handle);
                return databaseScope;
            }
        }
        
        public CKQueryNotificationReason QueryNotificationReason 
        {
            get 
            { 
                CKQueryNotificationReason queryNotificationReason = CKQueryNotification_GetPropQueryNotificationReason(Handle);
                return queryNotificationReason;
            }
        }
        
        public CKRecordID RecordID 
        {
            get 
            { 
                IntPtr recordID = CKQueryNotification_GetPropRecordID(Handle);
                return recordID == IntPtr.Zero ? null : new CKRecordID(recordID);
            }
        }
        
        #endregion
    }
}
