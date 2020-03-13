//
//  CKModifyRecordsOperation.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 03/13/2020
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
    public class CKModifyRecordsOperation : CKDatabaseOperation, IDisposable
    {
        #region dll

        // Class Methods
        

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKModifyRecordsOperation_init(
            out IntPtr exceptionPtr
            );
        
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
			int recordIDsCount, 
            out IntPtr exceptionPtr
            );
        

        

        

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
			int recordsToSaveCount, out IntPtr exceptionPtr);
        
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
			int recordIDsToDeleteCount, out IntPtr exceptionPtr);
        
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
        private static extern void CKModifyRecordsOperation_SetPropSavePolicy(HandleRef ptr, long savePolicy, out IntPtr exceptionPtr);
        
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
        private static extern void CKModifyRecordsOperation_SetPropAtomic(HandleRef ptr, bool atomic, out IntPtr exceptionPtr);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKModifyRecordsOperation_SetPropModifyRecordsCompletionBlock(HandleRef ptr, ModifyRecordsCompletionDelegate modifyRecordsCompletionBlock, out IntPtr exceptionPtr);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKModifyRecordsOperation_SetPropPerRecordCompletionBlock(HandleRef ptr, PerRecordCompletionDelegate perRecordCompletionBlock, out IntPtr exceptionPtr);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKModifyRecordsOperation_SetPropPerRecordProgressBlock(HandleRef ptr, PerRecordProgressDelegate perRecordProgressBlock, out IntPtr exceptionPtr);
        

        #endregion

        internal CKModifyRecordsOperation(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKModifyRecordsOperation(
            )
        {
            
            IntPtr ptr = CKModifyRecordsOperation_init(
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKModifyRecordsOperation(
            CKRecord[] records, 
            CKRecordID[] recordIDs
            )
        {
            
            IntPtr ptr = CKModifyRecordsOperation_initWithRecordsToSave_recordIDsToDelete(
                records == null ? null : records.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				records == null ? 0 : records.Length, 
                recordIDs == null ? null : recordIDs.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				recordIDs == null ? 0 : recordIDs.Length, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        
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
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * IntPtr.Size));
                    recordsToSave[i] = ptr2 == IntPtr.Zero ? null : new CKRecord(ptr2);
                }

                Marshal.FreeHGlobal(bufferPtr);

                return recordsToSave;
            }
            set
            {
                CKModifyRecordsOperation_SetPropRecordsToSave(Handle, value == null ? null : value.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				value == null ? 0 : value.Length, out IntPtr exceptionPtr);
                
                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
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
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * IntPtr.Size));
                    recordIDsToDelete[i] = ptr2 == IntPtr.Zero ? null : new CKRecordID(ptr2);
                }

                Marshal.FreeHGlobal(bufferPtr);

                return recordIDsToDelete;
            }
            set
            {
                CKModifyRecordsOperation_SetPropRecordIDsToDelete(Handle, value == null ? null : value.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				value == null ? 0 : value.Length, out IntPtr exceptionPtr);
                
                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
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
                CKModifyRecordsOperation_SetPropSavePolicy(Handle, (long) value, out IntPtr exceptionPtr);
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
                CKModifyRecordsOperation_SetPropAtomic(Handle, value, out IntPtr exceptionPtr);
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
                CKModifyRecordsOperation_SetPropModifyRecordsCompletionBlock(Handle, ModifyRecordsCompletionBlockCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
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
                Dispatcher.Instance.EnqueueOnMainThread(() => 
                    callback(
                        _savedRecords == null ? null : _savedRecords.Select(x => new CKRecord(x)).ToArray(),
                        _deletedRecordIDs == null ? null : _deletedRecordIDs.Select(x => new CKRecordID(x)).ToArray(),
                        _operationError == IntPtr.Zero ? null : new NSError(_operationError)));
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
                CKModifyRecordsOperation_SetPropPerRecordCompletionBlock(Handle, PerRecordCompletionBlockCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKRecord,NSError>> PerRecordCompletionBlockCallbacks = new Dictionary<IntPtr,Action<CKRecord,NSError>>();

        [MonoPInvokeCallback(typeof(PerRecordCompletionDelegate))]
        private static void PerRecordCompletionBlockCallback(IntPtr thisPtr, IntPtr _record, IntPtr _error)
        {
            if(PerRecordCompletionBlockCallbacks.TryGetValue(thisPtr, out Action<CKRecord,NSError> callback))
            {
                Dispatcher.Instance.EnqueueOnMainThread(() => 
                    callback(
                        _record == IntPtr.Zero ? null : new CKRecord(_record),
                        _error == IntPtr.Zero ? null : new NSError(_error)));
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
                CKModifyRecordsOperation_SetPropPerRecordProgressBlock(Handle, PerRecordProgressBlockCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKRecord,double>> PerRecordProgressBlockCallbacks = new Dictionary<IntPtr,Action<CKRecord,double>>();

        [MonoPInvokeCallback(typeof(PerRecordProgressDelegate))]
        private static void PerRecordProgressBlockCallback(IntPtr thisPtr, IntPtr _record, double _progress)
        {
            if(PerRecordProgressBlockCallbacks.TryGetValue(thisPtr, out Action<CKRecord,double> callback))
            {
                Dispatcher.Instance.EnqueueOnMainThread(() => 
                    callback(
                        _record == IntPtr.Zero ? null : new CKRecord(_record),
                        _progress));
            }
        }

        

        

        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKModifyRecordsOperation_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKModifyRecordsOperation Dispose");
                CKModifyRecordsOperation_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKModifyRecordsOperation()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public new void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
        
    }
}
