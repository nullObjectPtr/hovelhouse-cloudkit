//
//  CKReference.cs
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
    public class CKReference : CKObject
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKReference_initWithRecordID_action(
            IntPtr recordID, 
            long action);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKReference_initWithRecord_action(
            IntPtr record, 
            long action);
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern CKReferenceAction CKReference_GetPropReferenceAction(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKReference_GetPropRecordID(HandleRef ptr);
        
        #endregion

        internal CKReference(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKReference initWithRecordID(
            CKRecordID recordID, 
            CKReferenceAction action
        ){
            if(recordID == null)
                throw new ArgumentNullException(nameof(recordID));
            
            IntPtr ptr = CKReference_initWithRecordID_action(
                recordID != null ? HandleRef.ToIntPtr(recordID.Handle) : IntPtr.Zero,
                (long) action);
            return new CKReference(ptr);
        }
        
        
        public static CKReference initWithRecord(
            CKRecord record, 
            CKReferenceAction action
        ){
            if(record == null)
                throw new ArgumentNullException(nameof(record));
            
            IntPtr ptr = CKReference_initWithRecord_action(
                record != null ? HandleRef.ToIntPtr(record.Handle) : IntPtr.Zero,
                (long) action);
            return new CKReference(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public CKReferenceAction ReferenceAction 
        {
            get 
            { 
                CKReferenceAction referenceAction = CKReference_GetPropReferenceAction(Handle);
                return referenceAction;
            }
        }
        
        public CKRecordID RecordID 
        {
            get 
            { 
                IntPtr recordID = CKReference_GetPropRecordID(Handle);
                return recordID == IntPtr.Zero ? null : new CKRecordID(recordID);
            }
        }
        
        #endregion
    }
}
