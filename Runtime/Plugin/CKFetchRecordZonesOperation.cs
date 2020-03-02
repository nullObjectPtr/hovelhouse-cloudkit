//
//  CKFetchRecordZonesOperation.cs
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
    public class CKFetchRecordZonesOperation : CKObject
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKFetchRecordZonesOperation_init();
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKFetchRecordZonesOperation_initWithRecordZoneIDs(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 2)]
            IntPtr[] zoneIDs,
			int zoneIDsCount);
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchRecordZonesOperation_GetPropRecordZoneIDs(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKFetchRecordZonesOperation_SetPropRecordZoneIDs(HandleRef ptr, IntPtr[] recordZoneIDs,
			int recordZoneIDsCount);
        
        #endregion

        internal CKFetchRecordZonesOperation(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKFetchRecordZonesOperation init(
        ){
            
            IntPtr ptr = CKFetchRecordZonesOperation_init();
            return new CKFetchRecordZonesOperation(ptr);
        }
        
        
        public static CKFetchRecordZonesOperation initWithRecordZoneIDs(
            CKRecordZoneID[] zoneIDs
        ){
            if(zoneIDs == null)
                throw new ArgumentNullException(nameof(zoneIDs));
            
            IntPtr ptr = CKFetchRecordZonesOperation_initWithRecordZoneIDs(
                zoneIDs == null ? null : zoneIDs.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				zoneIDs == null ? 0 : zoneIDs.Length);
            return new CKFetchRecordZonesOperation(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public CKRecordZoneID[] RecordZoneIDs 
        {
            get 
            { 
                IntPtr bufferPtr = IntPtr.Zero;
                long bufferLen = 0;

                CKFetchRecordZonesOperation_GetPropRecordZoneIDs(Handle, ref bufferPtr, ref bufferLen);

                var recordZoneIDs = new CKRecordZoneID[bufferLen];

                for (int i = 0; i < bufferLen; i++)
                {
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * 8));
                    recordZoneIDs[i] = ptr2 == IntPtr.Zero ? null : new CKRecordZoneID(ptr2);
                }

                Marshal.FreeHGlobal(bufferPtr);

                return recordZoneIDs;
            }
            set
            {
                CKFetchRecordZonesOperation_SetPropRecordZoneIDs(Handle, value == null ? null : value.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				value == null ? 0 : value.Length);
            }
        }

        
        #endregion
    }
}
