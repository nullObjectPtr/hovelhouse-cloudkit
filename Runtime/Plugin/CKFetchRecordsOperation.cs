//
//  CKFetchRecordsOperation.cs
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
    public class CKFetchRecordsOperation : CKDatabaseOperation
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKFetchRecordsOperation_init();
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKFetchRecordsOperation_initWithRecordIDs(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 2)]
            IntPtr[] recordIDs,
			int recordIDsCount);
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchRecordsOperation_GetPropRecordIDs(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchRecordsOperation_SetPropRecordIDs(HandleRef ptr, IntPtr[] recordIDs,
			int recordIDsCount);
        // TODO: DLLPROPERTYSTRINGARRAY
        
        #endregion

        internal CKFetchRecordsOperation(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKFetchRecordsOperation init(
        ){
            
            IntPtr ptr = CKFetchRecordsOperation_init();
            return new CKFetchRecordsOperation(ptr);
        }
        
        
        public static CKFetchRecordsOperation initWithRecordIDs(
            CKRecordID[] recordIDs
        ){
            if(recordIDs == null)
                throw new ArgumentNullException(nameof(recordIDs));
            
            IntPtr ptr = CKFetchRecordsOperation_initWithRecordIDs(
                recordIDs == null ? null : recordIDs.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				recordIDs == null ? 0 : recordIDs.Length);
            return new CKFetchRecordsOperation(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public CKRecordID[] RecordIDs 
        {
            get 
            { 
                IntPtr bufferPtr = IntPtr.Zero;
                long bufferLen = 0;

                CKFetchRecordsOperation_GetPropRecordIDs(Handle, ref bufferPtr, ref bufferLen);

                var recordIDs = new CKRecordID[bufferLen];

                for (int i = 0; i < bufferLen; i++)
                {
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * 8));
                    recordIDs[i] = ptr2 == IntPtr.Zero ? null : new CKRecordID(ptr2);
                }

                Marshal.FreeHGlobal(bufferPtr);

                return recordIDs;
            }
            set
            {
                CKFetchRecordsOperation_SetPropRecordIDs(Handle, value == null ? null : value.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				value == null ? 0 : value.Length);
            }
        }

        
        // TODO: PROPERTYSTRINGARRAY
        
        #endregion
    }
}
