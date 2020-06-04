//
//  CKReference.cs
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
    /// A record field type that can be used to create relationships between records in a database
    /// </summary>
    /// <remarks>
    /// You can create relationships between two records in the database using CKReferences. There are useful in modeling parent-&gt;child relationships between records and you can even configure theres references to auto-delete children if their parents are deleted.
    /// </remarks>
    public class CKReference : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr CKReference_initWithRecordID_action(
            IntPtr recordID, 
            long action, 
            out IntPtr exceptionPtr
            );
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr CKReference_initWithRecord_action(
            IntPtr record, 
            long action, 
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern CKReferenceAction CKReference_GetPropReferenceAction(HandleRef ptr);

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr CKReference_GetPropRecordID(HandleRef ptr);

        

        #endregion

        internal CKReference(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKReference(
            CKRecordID recordID, 
            CKReferenceAction action
            )
        {
            if(recordID == null)
                throw new ArgumentNullException(nameof(recordID));
            
            IntPtr ptr = CKReference_initWithRecordID_action(
                recordID != null ? HandleRef.ToIntPtr(recordID.Handle) : IntPtr.Zero, 
                (long) action, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKReference(
            CKRecord record, 
            CKReferenceAction action
            )
        {
            if(record == null)
                throw new ArgumentNullException(nameof(record));
            
            IntPtr ptr = CKReference_initWithRecord_action(
                record != null ? HandleRef.ToIntPtr(record.Handle) : IntPtr.Zero, 
                (long) action, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        
        /// <value>ReferenceAction</value>
        public CKReferenceAction ReferenceAction
        {
            get 
            { 
                CKReferenceAction referenceAction = CKReference_GetPropReferenceAction(Handle);
                return referenceAction;
            }
        }

        
        /// <value>RecordID</value>
        public CKRecordID RecordID
        {
            get 
            { 
                IntPtr recordID = CKReference_GetPropRecordID(Handle);
                return recordID == IntPtr.Zero ? null : new CKRecordID(recordID);
            }
        }

        

        

        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern void CKReference_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKReference Dispose");
                CKReference_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKReference()
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
