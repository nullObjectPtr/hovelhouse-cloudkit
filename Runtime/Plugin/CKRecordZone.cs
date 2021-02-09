//
//  CKRecordZone.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 05/28/2020
//  Copyright © 2021 HovelHouseApps. All rights reserved.
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
    /// A container of orgonizing data in a CloudKit database
    /// </summary>
    /// <remarks>
    /// Record zones can be used to create container or sandboxes of related information. Zones only work in the private database
    /// </remarks>
    public class CKRecordZone : CKObject, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        
        [DllImport(dll)]
        private static extern IntPtr CKRecordZone_defaultRecordZone(
            out IntPtr exceptionPtr);

        

        
        [DllImport(dll)]
        private static extern IntPtr CKRecordZone_initWithZoneName(
            string zoneName, 
            out IntPtr exceptionPtr
            );
        
        [DllImport(dll)]
        private static extern IntPtr CKRecordZone_initWithZoneID(
            IntPtr zoneID, 
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern IntPtr CKRecordZone_GetPropZoneID(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern CKRecordZoneCapabilities CKRecordZone_GetPropCapabilities(HandleRef ptr);

        

        #endregion

        internal CKRecordZone(IntPtr ptr) : base(ptr) {}
        
        
        /// <summary>
        /// </summary>
        /// 
        /// <returns>val</returns>
        public static CKRecordZone DefaultRecordZone()
        { 
            var val = CKRecordZone_defaultRecordZone(out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val == IntPtr.Zero ? null : new CKRecordZone(val);
        }
        

        
        
        
        public CKRecordZone(
            string zoneName
            )
        {
            if(zoneName == null)
                throw new ArgumentNullException(nameof(zoneName));
            
            IntPtr ptr = CKRecordZone_initWithZoneName(
                zoneName, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKRecordZone(
            CKRecordZoneID zoneID
            )
        {
            if(zoneID == null)
                throw new ArgumentNullException(nameof(zoneID));
            
            IntPtr ptr = CKRecordZone_initWithZoneID(
                zoneID != null ? HandleRef.ToIntPtr(zoneID.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        
        /// <value>ZoneID</value>
        public CKRecordZoneID ZoneID
        {
            get 
            { 
                IntPtr zoneID = CKRecordZone_GetPropZoneID(Handle);
                return zoneID == IntPtr.Zero ? null : new CKRecordZoneID(zoneID);
            }
        }

        
        /// <value>Capabilities</value>
        public CKRecordZoneCapabilities Capabilities
        {
            get 
            { 
                CKRecordZoneCapabilities capabilities = CKRecordZone_GetPropCapabilities(Handle);
                return capabilities;
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKRecordZone_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKRecordZone Dispose");
                CKRecordZone_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKRecordZone()
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
