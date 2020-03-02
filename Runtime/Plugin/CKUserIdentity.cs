//
//  CKUserIdentity.cs
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
    public class CKUserIdentity : CKObject
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
        private static extern bool CKUserIdentity_GetPropHasiCloudAccount(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKUserIdentity_GetPropLookupInfo(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKUserIdentity_GetPropNameComponents(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKUserIdentity_GetPropUserRecordID(HandleRef ptr);
        // TODO: DLLPROPERTYSTRINGARRAY
        
        #endregion

        internal CKUserIdentity(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public bool HasiCloudAccount 
        {
            get 
            { 
                bool hasiCloudAccount = CKUserIdentity_GetPropHasiCloudAccount(Handle);
                return hasiCloudAccount;
            }
        }
        
        public CKUserIdentityLookupInfo LookupInfo 
        {
            get 
            { 
                IntPtr lookupInfo = CKUserIdentity_GetPropLookupInfo(Handle);
                return lookupInfo == IntPtr.Zero ? null : new CKUserIdentityLookupInfo(lookupInfo);
            }
        }
        
        public NSPersonNameComponents NameComponents 
        {
            get 
            { 
                IntPtr nameComponents = CKUserIdentity_GetPropNameComponents(Handle);
                return nameComponents == IntPtr.Zero ? null : new NSPersonNameComponents(nameComponents);
            }
        }
        
        public CKRecordID UserRecordID 
        {
            get 
            { 
                IntPtr userRecordID = CKUserIdentity_GetPropUserRecordID(Handle);
                return userRecordID == IntPtr.Zero ? null : new CKRecordID(userRecordID);
            }
        }
        
        // TODO: PROPERTYSTRINGARRAY
        
        #endregion
    }
}
