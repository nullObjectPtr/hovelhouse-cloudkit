//
//  CKRecordZoneID.cs
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
    /// Uniquely identifies a Zone
    /// </summary>
    public class CKRecordZoneID : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKRecordZoneID_initWithZoneName_ownerName(
            string zoneName, 
            string ownerName, 
            out IntPtr exceptionPtr
            );
        

        

        

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
        
        
        
        
        public CKRecordZoneID(
            string zoneName, 
            string ownerName
            )
        {
            if(zoneName == null)
                throw new ArgumentNullException(nameof(zoneName));
            if(ownerName == null)
                throw new ArgumentNullException(nameof(ownerName));
            
            IntPtr ptr = CKRecordZoneID_initWithZoneName_ownerName(
                zoneName, 
                ownerName, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        
        /// <value>ZoneName</value>
        public string ZoneName
        {
            get 
            { 
                IntPtr zoneName = CKRecordZoneID_GetPropZoneName(Handle);
                return Marshal.PtrToStringAuto(zoneName);
            }
        }

        
        /// <value>OwnerName</value>
        public string OwnerName
        {
            get 
            { 
                IntPtr ownerName = CKRecordZoneID_GetPropOwnerName(Handle);
                return Marshal.PtrToStringAuto(ownerName);
            }
        }

        

        

        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKRecordZoneID_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKRecordZoneID Dispose");
                CKRecordZoneID_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKRecordZoneID()
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
