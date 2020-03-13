//
//  CKQueryNotification.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 03/13/2020
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
    public class CKQueryNotification : CKSubscription, IDisposable
    {
        #region dll

        // Class Methods
        

        

        

        

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
        

        

        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKQueryNotification_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKQueryNotification Dispose");
                CKQueryNotification_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKQueryNotification()
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
