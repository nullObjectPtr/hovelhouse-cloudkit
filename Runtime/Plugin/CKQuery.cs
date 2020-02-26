//
//  CKQuery.cs
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
    public class CKQuery : CKObject
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKQuery_initWithRecordType_predicate(
            string recordType, 
            IntPtr predicate);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKQuery_initWithCoder(
            IntPtr aDecoder);
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKQuery_GetPropSortDescriptors(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKQuery_SetPropSortDescriptors(HandleRef ptr, IntPtr[] sortDescriptors,
			int sortDescriptorsCount);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKQuery_GetPropRecordType(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKQuery_GetPropPredicate(HandleRef ptr);
        
        #endregion

        internal CKQuery(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKQuery initWithRecordType(
            string recordType, 
            NSPredicate predicate
        ){
            if(recordType == null)
                throw new ArgumentNullException(nameof(recordType));
            if(predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            
            IntPtr ptr = CKQuery_initWithRecordType_predicate(
                recordType,
                predicate != null ? HandleRef.ToIntPtr(predicate.Handle) : IntPtr.Zero);
            return new CKQuery(ptr);
        }
        
        
        public static CKQuery initWithCoder(
            NSCoder aDecoder
        ){
            if(aDecoder == null)
                throw new ArgumentNullException(nameof(aDecoder));
            
            IntPtr ptr = CKQuery_initWithCoder(
                aDecoder != null ? HandleRef.ToIntPtr(aDecoder.Handle) : IntPtr.Zero);
            return new CKQuery(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public NSSortDescriptor[] SortDescriptors 
        {
            get 
            { 
                IntPtr bufferPtr = IntPtr.Zero;
                long bufferLen = 0;

                CKQuery_GetPropSortDescriptors(Handle, ref bufferPtr, ref bufferLen);

                var sortDescriptors = new NSSortDescriptor[bufferLen];

                for (int i = 0; i < bufferLen; i++)
                {
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * 8));
                    sortDescriptors[i] = ptr2 == IntPtr.Zero ? null : new NSSortDescriptor(ptr2);
                }

                Marshal.FreeHGlobal(bufferPtr);

                return sortDescriptors;
            }
            set
            {
                CKQuery_SetPropSortDescriptors(Handle, value == null ? null : value.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				value == null ? 0 : value.Length);
            }
        }

        
        public string RecordType 
        {
            get 
            { 
                IntPtr recordType = CKQuery_GetPropRecordType(Handle);
                return Marshal.PtrToStringAuto(recordType);
            }
        }
        
        public NSPredicate Predicate 
        {
            get 
            { 
                IntPtr predicate = CKQuery_GetPropPredicate(Handle);
                return predicate == IntPtr.Zero ? null : new NSPredicate(predicate);
            }
        }
        
        #endregion
    }
}
