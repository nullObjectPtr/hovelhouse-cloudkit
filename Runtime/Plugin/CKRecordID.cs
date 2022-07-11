//
//  CKRecordID.cs
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
    /// Uniquely identifies a record in a CloudKit database
    /// </summary>
    public class CKRecordID : CKObject, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        
        [DllImport(dll)]
        private static extern IntPtr CKRecordID_initWithRecordName(
            string recordName, 
            out IntPtr exceptionPtr
            );
        
        [DllImport(dll)]
        private static extern IntPtr CKRecordID_initWithRecordName_zoneID(
            string recordName, 
            IntPtr zoneID, 
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern IntPtr CKRecordID_GetPropRecordName(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr CKRecordID_GetPropZoneID(HandleRef ptr);

        

        #endregion

        internal CKRecordID(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKRecordID(
            string recordName
            )
        {
            if(recordName == null)
                throw new ArgumentNullException(nameof(recordName));
            
            IntPtr ptr = CKRecordID_initWithRecordName(
                recordName, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKRecordID(
            string recordName, 
            CKRecordZoneID zoneID
            )
        {
            if(recordName == null)
                throw new ArgumentNullException(nameof(recordName));
            if(zoneID == null)
                throw new ArgumentNullException(nameof(zoneID));
            
            IntPtr ptr = CKRecordID_initWithRecordName_zoneID(
                recordName, 
                zoneID != null ? HandleRef.ToIntPtr(zoneID.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        
        /// <value>RecordName</value>
        public string RecordName
        {
            get 
            { 
                IntPtr recordName = CKRecordID_GetPropRecordName(Handle);
                return Marshal.PtrToStringAuto(recordName);
            }
        }

        
        /// <value>ZoneID</value>
        public CKRecordZoneID ZoneID
        {
            get 
            { 
                IntPtr zoneID = CKRecordID_GetPropZoneID(Handle);
                return zoneID == IntPtr.Zero ? null : new CKRecordZoneID(zoneID);
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKRecordID_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKRecordID Dispose");
                CKRecordID_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKRecordID()
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
