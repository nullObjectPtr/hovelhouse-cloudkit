//
//  CKModifyRecordZonesOperation.cs
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
    /// A database operation that can simultaneously save and delete record zones
    /// </summary>
    public class CKModifyRecordZonesOperation : CKDatabaseOperation, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        
        [DllImport(dll)]
        private static extern IntPtr CKModifyRecordZonesOperation_init(
            out IntPtr exceptionPtr
            );
        
        [DllImport(dll)]
        private static extern IntPtr CKModifyRecordZonesOperation_initWithRecordZonesToSave_recordZoneIDsToDelete(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 2)]
            IntPtr[] recordZonesToSave,
			int recordZonesToSaveCount, 
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 4)]
            IntPtr[] recordZoneIDsToDelete,
			int recordZoneIDsToDeleteCount, 
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        [DllImport(dll)]
        private static extern void CKModifyRecordZonesOperation_SetPropModifyRecordZonesCompletionHandler(HandleRef ptr, ModifyRecordZonesCompletionDelegate modifyRecordZonesCompletionHandler, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern void CKModifyRecordZonesOperation_GetPropRecordZonesToSave(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        [DllImport(dll)]
        private static extern void CKModifyRecordZonesOperation_SetPropRecordZonesToSave(HandleRef ptr, IntPtr[] recordZonesToSave,
			int recordZonesToSaveCount, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern void CKModifyRecordZonesOperation_GetPropRecordZoneIDsToDelete(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        [DllImport(dll)]
        private static extern void CKModifyRecordZonesOperation_SetPropRecordZoneIDsToDelete(HandleRef ptr, IntPtr[] recordZoneIDsToDelete,
			int recordZoneIDsToDeleteCount, out IntPtr exceptionPtr);

        

        #endregion

        internal CKModifyRecordZonesOperation(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKModifyRecordZonesOperation(
            )
        {
            
            IntPtr ptr = CKModifyRecordZonesOperation_init(
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKModifyRecordZonesOperation(
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

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        
        /// <value>ModifyRecordZonesCompletionHandler</value>
        public Action<CKRecordZone[],CKRecordZoneID[],NSError> ModifyRecordZonesCompletionHandler
        {
            get 
            {
                ModifyRecordZonesCompletionHandlerCallbacks.TryGetValue(
                    HandleRef.ToIntPtr(Handle), 
                    out ExecutionContext<CKRecordZone[],CKRecordZoneID[],NSError> value);
                return value.Callback;
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
                    ModifyRecordZonesCompletionHandlerCallbacks[myPtr] = new ExecutionContext<CKRecordZone[],CKRecordZoneID[],NSError>(value);
                }
                CKModifyRecordZonesOperation_SetPropModifyRecordZonesCompletionHandler(Handle, ModifyRecordZonesCompletionHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,ExecutionContext<CKRecordZone[],CKRecordZoneID[],NSError>> ModifyRecordZonesCompletionHandlerCallbacks = new Dictionary<IntPtr,ExecutionContext<CKRecordZone[],CKRecordZoneID[],NSError>>();

        [MonoPInvokeCallback(typeof(ModifyRecordZonesCompletionDelegate))]
        private static void ModifyRecordZonesCompletionHandlerCallback(IntPtr thisPtr, IntPtr[] _savedRecordZones,
		long _savedRecordZonesCount, IntPtr[] _deletedRecordZoneIDs,
		long _deletedRecordZoneIDsCount, IntPtr _operationError)
        {
            if(ModifyRecordZonesCompletionHandlerCallbacks.TryGetValue(thisPtr, out ExecutionContext<CKRecordZone[],CKRecordZoneID[],NSError> callback))
            {
                callback.Invoke(
                        _savedRecordZones == null ? null : _savedRecordZones.Select(x => new CKRecordZone(x)).ToArray(),
                        _deletedRecordZoneIDs == null ? null : _deletedRecordZoneIDs.Select(x => new CKRecordZoneID(x)).ToArray(),
                        _operationError == IntPtr.Zero ? null : new NSError(_operationError));
            }
        }

        
        /// <value>RecordZonesToSave</value>
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
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * IntPtr.Size));
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

        
        /// <value>RecordZoneIDsToDelete</value>
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
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * IntPtr.Size));
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

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKModifyRecordZonesOperation_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected override void Dispose(bool disposing)
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
        public new void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
        
    }
}
