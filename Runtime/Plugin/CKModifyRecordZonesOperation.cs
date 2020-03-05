//
//  CKModifyRecordZonesOperation.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 03/02/2020
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
    public class CKModifyRecordZonesOperation : CKDatabaseOperation, IDisposable
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKModifyRecordZonesOperation_init(
            out IntPtr exceptionPtr
            );
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKModifyRecordZonesOperation_initWithRecordZonesToSave_recordZoneIDsToDelete(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 2)]
            IntPtr[] recordZonesToSave,
			int recordZonesToSaveCount, 
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 4)]
            IntPtr[] recordZoneIDsToDelete,
			int recordZoneIDsToDeleteCount, 
            out IntPtr exceptionPtr
            );
        

        // Instance Methods
        

        

        // Properties
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKModifyRecordZonesOperation_SetPropModifyRecordZonesCompletionHandler(HandleRef ptr, ModifyRecordZonesCompletionDelegate modifyRecordZonesCompletionHandler, out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKModifyRecordZonesOperation_GetPropRecordZonesToSave(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKModifyRecordZonesOperation_SetPropRecordZonesToSave(HandleRef ptr, IntPtr[] recordZonesToSave,
			int recordZonesToSaveCount, out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKModifyRecordZonesOperation_GetPropRecordZoneIDsToDelete(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKModifyRecordZonesOperation_SetPropRecordZoneIDsToDelete(HandleRef ptr, IntPtr[] recordZoneIDsToDelete,
			int recordZoneIDsToDeleteCount, out IntPtr exceptionPtr);
        
        #endregion

        internal CKModifyRecordZonesOperation(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKModifyRecordZonesOperation init(
            )
        {
            
            IntPtr ptr = CKModifyRecordZonesOperation_init(
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            return new CKModifyRecordZonesOperation(ptr);
        }
        
        
        public static CKModifyRecordZonesOperation initWithRecordZonesToSave(
            CKRecordZone[] recordZonesToSave, 
            CKRecordZoneID[] recordZoneIDsToDelete
            )
        {
            
            IntPtr ptr = CKModifyRecordZonesOperation_initWithRecordZonesToSave_recordZoneIDsToDelete(
                recordZonesToSave == null ? null : recordZonesToSave.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				recordZonesToSave == null ? 0 : recordZonesToSave.Length, 
                recordZoneIDsToDelete == null ? null : recordZoneIDsToDelete.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				recordZoneIDsToDelete == null ? 0 : recordZoneIDsToDelete.Length, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            return new CKModifyRecordZonesOperation(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public Action<CKRecordZone[],CKRecordZoneID[],NSError> ModifyRecordZonesCompletionHandler 
        {
            get 
            {
                Action<CKRecordZone[],CKRecordZoneID[],NSError> value;
                ModifyRecordZonesCompletionHandlerCallbacks.TryGetValue(HandleRef.ToIntPtr(Handle), out value);
                return value;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    ModifyRecordZonesCompletionHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    ModifyRecordZonesCompletionHandlerCallbacks[myPtr] = value;
                }
                CKModifyRecordZonesOperation_SetPropModifyRecordZonesCompletionHandler(Handle, ModifyRecordZonesCompletionHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKRecordZone[],CKRecordZoneID[],NSError>> ModifyRecordZonesCompletionHandlerCallbacks = new Dictionary<IntPtr,Action<CKRecordZone[],CKRecordZoneID[],NSError>>();

        [MonoPInvokeCallback(typeof(ModifyRecordZonesCompletionDelegate))]
        private static void ModifyRecordZonesCompletionHandlerCallback(IntPtr thisPtr, IntPtr[] _savedRecordZones,
		long _savedRecordZonesCount, IntPtr[] _deletedRecordZoneIDs,
		long _deletedRecordZoneIDsCount, IntPtr _operationError)
        {
            if(ModifyRecordZonesCompletionHandlerCallbacks.TryGetValue(thisPtr, out Action<CKRecordZone[],CKRecordZoneID[],NSError> callback))
            {
                Dispatcher.Instance.EnqueueOnMainThread(() => 
                    callback(
                        _savedRecordZones == null ? null : _savedRecordZones.Select(x => new CKRecordZone(x)).ToArray(),
                        _deletedRecordZoneIDs == null ? null : _deletedRecordZoneIDs.Select(x => new CKRecordZoneID(x)).ToArray(),
                        _operationError == IntPtr.Zero ? null : new NSError(_operationError)));
            }
        }

        
        public CKRecordZone[] RecordZonesToSave 
        {
            get 
            { 
                IntPtr bufferPtr = IntPtr.Zero;
                long bufferLen = 0;

                CKModifyRecordZonesOperation_GetPropRecordZonesToSave(Handle, ref bufferPtr, ref bufferLen);

                var recordZonesToSave = new CKRecordZone[bufferLen];

                for (int i = 0; i < bufferLen; i++)
                {
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * 8));
                    recordZonesToSave[i] = ptr2 == IntPtr.Zero ? null : new CKRecordZone(ptr2);
                }

                Marshal.FreeHGlobal(bufferPtr);

                return recordZonesToSave;
            }
            set
            {
                CKModifyRecordZonesOperation_SetPropRecordZonesToSave(Handle, value == null ? null : value.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				value == null ? 0 : value.Length, out IntPtr exceptionPtr);
                
                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        
        public CKRecordZoneID[] RecordZoneIDsToDelete 
        {
            get 
            { 
                IntPtr bufferPtr = IntPtr.Zero;
                long bufferLen = 0;

                CKModifyRecordZonesOperation_GetPropRecordZoneIDsToDelete(Handle, ref bufferPtr, ref bufferLen);

                var recordZoneIDsToDelete = new CKRecordZoneID[bufferLen];

                for (int i = 0; i < bufferLen; i++)
                {
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * 8));
                    recordZoneIDsToDelete[i] = ptr2 == IntPtr.Zero ? null : new CKRecordZoneID(ptr2);
                }

                Marshal.FreeHGlobal(bufferPtr);

                return recordZoneIDsToDelete;
            }
            set
            {
                CKModifyRecordZonesOperation_SetPropRecordZoneIDsToDelete(Handle, value == null ? null : value.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				value == null ? 0 : value.Length, out IntPtr exceptionPtr);
                
                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        
        #endregion
        
        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKModifyRecordZonesOperation_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        // No base.Dispose() needed
        // All we ever do is decrement the reference count in managed code
        
        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKModifyRecordZonesOperation Dispose");
                CKModifyRecordZonesOperation_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKModifyRecordZonesOperation()
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
