//
//  CKFetchRecordZonesOperation.cs
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
    /// A database operation that fetches the available zones on the database
    /// </summary>
    public class CKFetchRecordZonesOperation : CKDatabaseOperation, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        
        [DllImport(dll)]
        private static extern IntPtr CKFetchRecordZonesOperation_fetchAllRecordZonesOperation(
            out IntPtr exceptionPtr);

        

        
        [DllImport(dll)]
        private static extern IntPtr CKFetchRecordZonesOperation_init(
            out IntPtr exceptionPtr
            );
        
        [DllImport(dll)]
        private static extern IntPtr CKFetchRecordZonesOperation_initWithRecordZoneIDs(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 2)]
            IntPtr[] zoneIDs,
			int zoneIDsCount, 
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern void CKFetchRecordZonesOperation_GetPropRecordZoneIDs(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        [DllImport(dll)]
        private static extern void CKFetchRecordZonesOperation_SetPropRecordZoneIDs(HandleRef ptr, IntPtr[] recordZoneIDs,
			int recordZoneIDsCount, out IntPtr exceptionPtr);

        [DllImport(dll)]
        private static extern void CKFetchRecordZonesOperation_SetPropFetchRecordZonesCompletionHandler(HandleRef ptr, FetchRecordZonesCompletionDelegate fetchRecordZonesCompletionHandler, out IntPtr exceptionPtr);

        

        #endregion

        internal CKFetchRecordZonesOperation(IntPtr ptr) : base(ptr) {}
        
        
        /// <summary>
        /// </summary>
        /// 
        /// <returns>val</returns>
        public static CKFetchRecordZonesOperation FetchAllRecordZonesOperation()
        { 
            var val = CKFetchRecordZonesOperation_fetchAllRecordZonesOperation(out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val == IntPtr.Zero ? null : new CKFetchRecordZonesOperation(val);
        }
        

        
        
        
        public CKFetchRecordZonesOperation(
            )
        {
            
            IntPtr ptr = CKFetchRecordZonesOperation_init(
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKFetchRecordZonesOperation(
            CKRecordZoneID[] zoneIDs
            )
        {
            if(zoneIDs == null)
                throw new ArgumentNullException(nameof(zoneIDs));
            
            IntPtr ptr = CKFetchRecordZonesOperation_initWithRecordZoneIDs(
                zoneIDs == null ? null : zoneIDs.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				zoneIDs == null ? 0 : zoneIDs.Length, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        
        /// <value>RecordZoneIDs</value>
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
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * IntPtr.Size));
                    recordZoneIDs[i] = ptr2 == IntPtr.Zero ? null : new CKRecordZoneID(ptr2);
                }

                Marshal.FreeHGlobal(bufferPtr);

                return recordZoneIDs;
            }
            set
            {
                CKFetchRecordZonesOperation_SetPropRecordZoneIDs(Handle, value == null ? null : value.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				value == null ? 0 : value.Length, out IntPtr exceptionPtr);
                
                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        
        /// <value>FetchRecordZonesCompletionHandler</value>
        public Action<Dictionary<CKRecordZoneID,CKRecordZone>,NSError> FetchRecordZonesCompletionHandler
        {
            get 
            {
                FetchRecordZonesCompletionHandlerCallbacks.TryGetValue(
                    HandleRef.ToIntPtr(Handle), 
                    out ExecutionContext<Dictionary<CKRecordZoneID,CKRecordZone>,NSError> value);
                return value.Callback;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    FetchRecordZonesCompletionHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    FetchRecordZonesCompletionHandlerCallbacks[myPtr] = new ExecutionContext<Dictionary<CKRecordZoneID,CKRecordZone>,NSError>(value);
                }
                CKFetchRecordZonesOperation_SetPropFetchRecordZonesCompletionHandler(Handle, FetchRecordZonesCompletionHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,ExecutionContext<Dictionary<CKRecordZoneID,CKRecordZone>,NSError>> FetchRecordZonesCompletionHandlerCallbacks = new Dictionary<IntPtr,ExecutionContext<Dictionary<CKRecordZoneID,CKRecordZone>,NSError>>();

        [MonoPInvokeCallback(typeof(FetchRecordZonesCompletionDelegate))]
        private static void FetchRecordZonesCompletionHandlerCallback(IntPtr thisPtr, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 3)]
		IntPtr[] _recordZonesByZoneIDKeys,
		[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 3)]
		IntPtr[] _recordZonesByZoneIDValues,
		long _recordZonesByZoneIDCount, IntPtr _operationError)
        {
            if(FetchRecordZonesCompletionHandlerCallbacks.TryGetValue(thisPtr, out ExecutionContext<Dictionary<CKRecordZoneID,CKRecordZone>,NSError> callback))
            {
                callback.Invoke(
                        _recordZonesByZoneIDKeys.Zip(_recordZonesByZoneIDValues, (k, v) => ( k == IntPtr.Zero ? null : new CKRecordZoneID(k), v == IntPtr.Zero ? null : new CKRecordZone(v) )).ToDictionary(item => item.Item1, item => item.Item2),
                        _operationError == IntPtr.Zero ? null : new NSError(_operationError));
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKFetchRecordZonesOperation_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKFetchRecordZonesOperation Dispose");
                CKFetchRecordZonesOperation_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKFetchRecordZonesOperation()
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
