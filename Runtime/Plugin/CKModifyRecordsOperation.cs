//
//  CKModifyRecordsOperation.cs
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
    public class CKModifyRecordsOperation : CKDatabaseOperation
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKModifyRecordsOperation_init();
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKModifyRecordsOperation_initWithRecordsToSave_recordIDsToDelete(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 2)]
            IntPtr[] records,
			int recordsCount, 
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 4)]
            IntPtr[] recordIDs,
			int recordIDsCount);
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKModifyRecordsOperation_GetPropRecordsToSave(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKModifyRecordsOperation_SetPropRecordsToSave(HandleRef ptr, IntPtr[] recordsToSave,
			int recordsToSaveCount);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKModifyRecordsOperation_GetPropRecordIDsToDelete(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKModifyRecordsOperation_SetPropRecordIDsToDelete(HandleRef ptr, IntPtr[] recordIDsToDelete,
			int recordIDsToDeleteCount);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern CKRecordSavePolicy CKModifyRecordsOperation_GetPropSavePolicy(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKModifyRecordsOperation_SetPropSavePolicy(HandleRef ptr, long savePolicy);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern bool CKModifyRecordsOperation_GetPropAtomic(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKModifyRecordsOperation_SetPropAtomic(HandleRef ptr, bool atomic);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKModifyRecordsOperation_SetPropModifyRecordsCompletionBlock(HandleRef ptr, ModifyRecordsCompletionDelegate modifyRecordsCompletionBlock);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKModifyRecordsOperation_SetPropPerRecordCompletionBlock(HandleRef ptr, PerRecordCompletionDelegate perRecordCompletionBlock);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKModifyRecordsOperation_SetPropPerRecordProgressBlock(HandleRef ptr, PerRecordProgressDelegate perRecordProgressBlock);
        
        #endregion

        internal CKModifyRecordsOperation(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKModifyRecordsOperation init(
        ){
            
            IntPtr ptr = CKModifyRecordsOperation_init();
            return new CKModifyRecordsOperation(ptr);
        }
        
        
        public static CKModifyRecordsOperation initWithRecordsToSave(
            CKRecord[] records, 
            CKRecordID[] recordIDs
        ){
            
            IntPtr ptr = CKModifyRecordsOperation_initWithRecordsToSave_recordIDsToDelete(
                records == null ? null : records.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				records == null ? 0 : records.Length,
                recordIDs == null ? null : recordIDs.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				recordIDs == null ? 0 : recordIDs.Length);
            return new CKModifyRecordsOperation(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public CKRecord[] RecordsToSave 
        {
            get 
            { 
                IntPtr bufferPtr = IntPtr.Zero;
                long bufferLen = 0;

                CKModifyRecordsOperation_GetPropRecordsToSave(Handle, ref bufferPtr, ref bufferLen);

                var recordsToSave = new CKRecord[bufferLen];

                for (int i = 0; i < bufferLen; i++)
                {
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * 8));
                    recordsToSave[i] = ptr2 == IntPtr.Zero ? null : new CKRecord(ptr2);
                }

                Marshal.FreeHGlobal(bufferPtr);

                return recordsToSave;
            }
            set
            {
                CKModifyRecordsOperation_SetPropRecordsToSave(Handle, value == null ? null : value.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				value == null ? 0 : value.Length);
            }
        }

        
        public CKRecordID[] RecordIDsToDelete 
        {
            get 
            { 
                IntPtr bufferPtr = IntPtr.Zero;
                long bufferLen = 0;

                CKModifyRecordsOperation_GetPropRecordIDsToDelete(Handle, ref bufferPtr, ref bufferLen);

                var recordIDsToDelete = new CKRecordID[bufferLen];

                for (int i = 0; i < bufferLen; i++)
                {
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * 8));
                    recordIDsToDelete[i] = ptr2 == IntPtr.Zero ? null : new CKRecordID(ptr2);
                }

                Marshal.FreeHGlobal(bufferPtr);

                return recordIDsToDelete;
            }
            set
            {
                CKModifyRecordsOperation_SetPropRecordIDsToDelete(Handle, value == null ? null : value.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				value == null ? 0 : value.Length);
            }
        }

        
        public CKRecordSavePolicy SavePolicy 
        {
            get 
            { 
                CKRecordSavePolicy savePolicy = CKModifyRecordsOperation_GetPropSavePolicy(Handle);
                return savePolicy;
            }
            set
            {
                CKModifyRecordsOperation_SetPropSavePolicy(Handle, (long) value);
            }
        }
        
        public bool Atomic 
        {
            get 
            { 
                bool atomic = CKModifyRecordsOperation_GetPropAtomic(Handle);
                return atomic;
            }
            set
            {
                CKModifyRecordsOperation_SetPropAtomic(Handle, value);
            }
        }
        
        public Action<CKRecord[],CKRecordID[],NSError> ModifyRecordsCompletionBlock 
        {
            get 
            {
                Action<CKRecord[],CKRecordID[],NSError> value;
                ModifyRecordsCompletionBlockCallbacks.TryGetValue(HandleRef.ToIntPtr(Handle), out value);
                return value;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    ModifyRecordsCompletionBlockCallbacks.Remove(myPtr);
                }
                else
                {
                    ModifyRecordsCompletionBlockCallbacks[myPtr] = value;
                }
                CKModifyRecordsOperation_SetPropModifyRecordsCompletionBlock(Handle, ModifyRecordsCompletionBlockCallback);
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKRecord[],CKRecordID[],NSError>> ModifyRecordsCompletionBlockCallbacks = new Dictionary<IntPtr,Action<CKRecord[],CKRecordID[],NSError>>();

        [MonoPInvokeCallback(typeof(ModifyRecordsCompletionDelegate))]
        private static void ModifyRecordsCompletionBlockCallback(IntPtr thisPtr, IntPtr[] _savedRecords,
		long _savedRecordsCount, IntPtr[] _deletedRecordIDs,
		long _deletedRecordIDsCount, IntPtr _operationError)
        {
            if(ModifyRecordsCompletionBlockCallbacks.TryGetValue(thisPtr, out Action<CKRecord[],CKRecordID[],NSError> callback))
            {
                try
                {
                    callback(
                        _savedRecords == null ? null : _savedRecords.Select(x => new CKRecord(x)).ToArray(),
                        _deletedRecordIDs == null ? null : _deletedRecordIDs.Select(x => new CKRecordID(x)).ToArray(),
                        _operationError == IntPtr.Zero ? null : new NSError(_operationError));
                }
                catch(Exception exc)
                {
                    Debug.LogError(exc);
                }
            }
        }

        
        public Action<CKRecord,NSError> PerRecordCompletionBlock 
        {
            get 
            {
                Action<CKRecord,NSError> value;
                PerRecordCompletionBlockCallbacks.TryGetValue(HandleRef.ToIntPtr(Handle), out value);
                return value;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    PerRecordCompletionBlockCallbacks.Remove(myPtr);
                }
                else
                {
                    PerRecordCompletionBlockCallbacks[myPtr] = value;
                }
                CKModifyRecordsOperation_SetPropPerRecordCompletionBlock(Handle, PerRecordCompletionBlockCallback);
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKRecord,NSError>> PerRecordCompletionBlockCallbacks = new Dictionary<IntPtr,Action<CKRecord,NSError>>();

        [MonoPInvokeCallback(typeof(PerRecordCompletionDelegate))]
        private static void PerRecordCompletionBlockCallback(IntPtr thisPtr, IntPtr _record, IntPtr _error)
        {
            if(PerRecordCompletionBlockCallbacks.TryGetValue(thisPtr, out Action<CKRecord,NSError> callback))
            {
                try
                {
                    callback(
                        _record == IntPtr.Zero ? null : new CKRecord(_record),
                        _error == IntPtr.Zero ? null : new NSError(_error));
                }
                catch(Exception exc)
                {
                    Debug.LogError(exc);
                }
            }
        }

        
        public Action<CKRecord,double> PerRecordProgressBlock 
        {
            get 
            {
                Action<CKRecord,double> value;
                PerRecordProgressBlockCallbacks.TryGetValue(HandleRef.ToIntPtr(Handle), out value);
                return value;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    PerRecordProgressBlockCallbacks.Remove(myPtr);
                }
                else
                {
                    PerRecordProgressBlockCallbacks[myPtr] = value;
                }
                CKModifyRecordsOperation_SetPropPerRecordProgressBlock(Handle, PerRecordProgressBlockCallback);
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKRecord,double>> PerRecordProgressBlockCallbacks = new Dictionary<IntPtr,Action<CKRecord,double>>();

        [MonoPInvokeCallback(typeof(PerRecordProgressDelegate))]
        private static void PerRecordProgressBlockCallback(IntPtr thisPtr, IntPtr _record, double _progress)
        {
            if(PerRecordProgressBlockCallbacks.TryGetValue(thisPtr, out Action<CKRecord,double> callback))
            {
                try
                {
                    callback(
                        _record == IntPtr.Zero ? null : new CKRecord(_record),
                        _progress);
                }
                catch(Exception exc)
                {
                    Debug.LogError(exc);
                }
            }
        }

        
        #endregion
    }
}
