//
//  CKUserIdentity.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 03/26/2020
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
    /// A CloudKit User basically
    /// </summary>
    public class CKUserIdentity : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        

        

        

        

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
        
        
        
        


        
        
        
        /// <value>HasiCloudAccount</value>
        public bool HasiCloudAccount
        {
            get 
            { 
                bool hasiCloudAccount = CKUserIdentity_GetPropHasiCloudAccount(Handle);
                return hasiCloudAccount;
            }
        }

        
        /// <value>LookupInfo</value>
        public CKUserIdentityLookupInfo LookupInfo
        {
            get 
            { 
                IntPtr lookupInfo = CKUserIdentity_GetPropLookupInfo(Handle);
                return lookupInfo == IntPtr.Zero ? null : new CKUserIdentityLookupInfo(lookupInfo);
            }
        }

        
        /// <value>NameComponents</value>
        public NSPersonNameComponents NameComponents
        {
            get 
            { 
                IntPtr nameComponents = CKUserIdentity_GetPropNameComponents(Handle);
                return nameComponents == IntPtr.Zero ? null : new NSPersonNameComponents(nameComponents);
            }
        }

        
        /// <value>UserRecordID</value>
        public CKRecordID UserRecordID
        {
            get 
            { 
                IntPtr userRecordID = CKUserIdentity_GetPropUserRecordID(Handle);
                return userRecordID == IntPtr.Zero ? null : new CKRecordID(userRecordID);
            }
        }

        
        // TODO: PROPERTYSTRINGARRAY
        

        

        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKUserIdentity_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKUserIdentity Dispose");
                CKUserIdentity_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKUserIdentity()
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
