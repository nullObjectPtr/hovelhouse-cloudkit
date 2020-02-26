//
//  CKDatabaseNotification.cs
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
    public class CKDatabaseNotification : CKNotification
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
        private static extern CKDatabaseScope CKDatabaseNotification_GetPropDatabaseScope(HandleRef ptr);
        
        #endregion

        internal CKDatabaseNotification(IntPtr ptr) : base(ptr) {}
        
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
                CKDatabaseScope databaseScope = CKDatabaseNotification_GetPropDatabaseScope(Handle);
                return databaseScope;
            }
        }
        
        #endregion
    }
}
