//
//  CKDatabaseNotification.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 05/28/2020
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
    /// <summary>
    /// A notification triggered by a change to the database
    /// </summary>
    public class CKDatabaseNotification : CKNotification, IDisposable
    {
        #region dll

        // Class Methods
        

        

        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern CKDatabaseScope CKDatabaseNotification_GetPropDatabaseScope(HandleRef ptr);

        

        #endregion

        internal CKDatabaseNotification(IntPtr ptr) : base(ptr) {}
        
        
        
        


        
        
        
        /// <value>DatabaseScope</value>
        public CKDatabaseScope DatabaseScope
        {
            get 
            { 
                CKDatabaseScope databaseScope = CKDatabaseNotification_GetPropDatabaseScope(Handle);
                return databaseScope;
            }
        }

        

        

        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern void CKDatabaseNotification_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKDatabaseNotification Dispose");
                CKDatabaseNotification_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKDatabaseNotification()
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
