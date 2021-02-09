//
//  CKFetchRecordsOperation.cs
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
    /// An operation that fetches records from the database using the record id's you provide
    /// </summary>
    /// <remarks>
    /// Use this to retrieve records by their record id&apos;s. You can also opt to recieve only a subset of the record data by specifying the record key&apos;s youd by filling in the &apos;DesiredKeys&apos; property with the key values you wish to retrieve. Assign a delegate to the FetchRecordsCompletionHandler property to be invoked when this operation completes.
    /// </remarks>
    public class CKFetchRecordsOperation : CKDatabaseOperation, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        
        [DllImport(dll)]
        private static extern IntPtr CKFetchRecordsOperation_fetchCurrentUserRecordOperation(
            out IntPtr exceptionPtr);

        

        
        [DllImport(dll)]
        private static extern IntPtr CKFetchRecordsOperation_init(
            out IntPtr exceptionPtr
            );
        
        [DllImport(dll)]
        private static extern IntPtr CKFetchRecordsOperation_initWithRecordIDs(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 2)]
            IntPtr[] recordIDs,
			int recordIDsCount, 
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern void CKFetchRecordsOperation_GetPropRecordIDs(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        [DllImport(dll)]
        private static extern void CKFetchRecordsOperation_SetPropRecordIDs(HandleRef ptr, IntPtr[] recordIDs,
			int recordIDsCount, out IntPtr exceptionPtr);

        // TODO: DLLPROPERTYSTRINGARRAY

        [DllImport(dll)]
        private static extern void CKFetchRecordsOperation_SetPropFetchRecordsCompletionHandler(HandleRef ptr, FetchRecordsCompletionDelegate fetchRecordsCompletionHandler, out IntPtr exceptionPtr);

        [DllImport(dll)]
        private static extern void CKFetchRecordsOperation_SetPropPerRecordCompletionHandler(HandleRef ptr, PerRecordCompletionDelegate2 perRecordCompletionHandler, out IntPtr exceptionPtr);

        [DllImport(dll)]
        private static extern void CKFetchRecordsOperation_SetPropProgressHandler(HandleRef ptr, PerRecordProgressDelegate2 progressHandler, out IntPtr exceptionPtr);

        

        #endregion

        internal CKFetchRecordsOperation(IntPtr ptr) : base(ptr) {}
        
        
        /// <summary>
        /// </summary>
        /// 
        /// <returns>val</returns>
        public static CKFetchRecordsOperation FetchCurrentUserRecordOperation()
        { 
            var val = CKFetchRecordsOperation_fetchCurrentUserRecordOperation(out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val == IntPtr.Zero ? null : new CKFetchRecordsOperation(val);
        }
        

        
        
        
        public CKFetchRecordsOperation(
            )
        {
            
            IntPtr ptr = CKFetchRecordsOperation_init(
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKFetchRecordsOperation(
            CKRecordID[] recordIDs
            )
        {
            if(recordIDs == null)
                throw new ArgumentNullException(nameof(recordIDs));
            
            IntPtr ptr = CKFetchRecordsOperation_initWithRecordIDs(
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
        
        


        
        
        
        /// <value>RecordIDs</value>
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
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * IntPtr.Size));
                    recordIDs[i] = ptr2 == IntPtr.Zero ? null : new CKRecordID(ptr2);
                }

                Marshal.FreeHGlobal(bufferPtr);

                return recordIDs;
            }
            set
            {
                CKFetchRecordsOperation_SetPropRecordIDs(Handle, value == null ? null : value.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				value == null ? 0 : value.Length, out IntPtr exceptionPtr);
                
                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        
        // TODO: PROPERTYSTRINGARRAY
        
        /// <value>FetchRecordsCompletionHandler</value>
        public Action<Dictionary<CKRecordID,CKRecord>,NSError> FetchRecordsCompletionHandler
        {
            get 
            {
                FetchRecordsCompletionHandlerCallbacks.TryGetValue(
                    HandleRef.ToIntPtr(Handle), 
                    out ExecutionContext<Dictionary<CKRecordID,CKRecord>,NSError> value);
                return value.Callback;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    FetchRecordsCompletionHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    FetchRecordsCompletionHandlerCallbacks[myPtr] = new ExecutionContext<Dictionary<CKRecordID,CKRecord>,NSError>(value);
                }
                CKFetchRecordsOperation_SetPropFetchRecordsCompletionHandler(Handle, FetchRecordsCompletionHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,ExecutionContext<Dictionary<CKRecordID,CKRecord>,NSError>> FetchRecordsCompletionHandlerCallbacks = new Dictionary<IntPtr,ExecutionContext<Dictionary<CKRecordID,CKRecord>,NSError>>();

        [MonoPInvokeCallback(typeof(FetchRecordsCompletionDelegate))]
        private static void FetchRecordsCompletionHandlerCallback(IntPtr thisPtr, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 3)]
		IntPtr[] _recordsByRecordIDKeys,
		[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 3)]
		IntPtr[] _recordsByRecordIDValues,
		long _recordsByRecordIDCount, IntPtr _operationError)
        {
            if(FetchRecordsCompletionHandlerCallbacks.TryGetValue(thisPtr, out ExecutionContext<Dictionary<CKRecordID,CKRecord>,NSError> callback))
            {
                callback.Invoke(
                        _recordsByRecordIDKeys.Zip(_recordsByRecordIDValues, (k, v) => ( k == IntPtr.Zero ? null : new CKRecordID(k), v == IntPtr.Zero ? null : new CKRecord(v) )).ToDictionary(item => item.Item1, item => item.Item2),
                        _operationError == IntPtr.Zero ? null : new NSError(_operationError));
            }
        }

        
        /// <value>PerRecordCompletionHandler</value>
        public Action<CKRecord,CKRecordID,NSError> PerRecordCompletionHandler
        {
            get 
            {
                PerRecordCompletionHandlerCallbacks.TryGetValue(
                    HandleRef.ToIntPtr(Handle), 
                    out ExecutionContext<CKRecord,CKRecordID,NSError> value);
                return value.Callback;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    PerRecordCompletionHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    PerRecordCompletionHandlerCallbacks[myPtr] = new ExecutionContext<CKRecord,CKRecordID,NSError>(value);
                }
                CKFetchRecordsOperation_SetPropPerRecordCompletionHandler(Handle, PerRecordCompletionHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,ExecutionContext<CKRecord,CKRecordID,NSError>> PerRecordCompletionHandlerCallbacks = new Dictionary<IntPtr,ExecutionContext<CKRecord,CKRecordID,NSError>>();

        [MonoPInvokeCallback(typeof(PerRecordCompletionDelegate2))]
        private static void PerRecordCompletionHandlerCallback(IntPtr thisPtr, IntPtr _record, IntPtr _recordID, IntPtr _error)
        {
            if(PerRecordCompletionHandlerCallbacks.TryGetValue(thisPtr, out ExecutionContext<CKRecord,CKRecordID,NSError> callback))
            {
                callback.Invoke(
                        _record == IntPtr.Zero ? null : new CKRecord(_record),
                        _recordID == IntPtr.Zero ? null : new CKRecordID(_recordID),
                        _error == IntPtr.Zero ? null : new NSError(_error));
            }
        }

        
        /// <value>ProgressHandler</value>
        public Action<CKRecordID,double> ProgressHandler
        {
            get 
            {
                ProgressHandlerCallbacks.TryGetValue(
                    HandleRef.ToIntPtr(Handle), 
                    out ExecutionContext<CKRecordID,double> value);
                return value.Callback;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    ProgressHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    ProgressHandlerCallbacks[myPtr] = new ExecutionContext<CKRecordID,double>(value);
                }
                CKFetchRecordsOperation_SetPropProgressHandler(Handle, ProgressHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,ExecutionContext<CKRecordID,double>> ProgressHandlerCallbacks = new Dictionary<IntPtr,ExecutionContext<CKRecordID,double>>();

        [MonoPInvokeCallback(typeof(PerRecordProgressDelegate2))]
        private static void ProgressHandlerCallback(IntPtr thisPtr, IntPtr _record, double _progress)
        {
            if(ProgressHandlerCallbacks.TryGetValue(thisPtr, out ExecutionContext<CKRecordID,double> callback))
            {
                callback.Invoke(
                        _record == IntPtr.Zero ? null : new CKRecordID(_record),
                        _progress);
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKFetchRecordsOperation_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKFetchRecordsOperation Dispose");
                CKFetchRecordsOperation_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKFetchRecordsOperation()
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
