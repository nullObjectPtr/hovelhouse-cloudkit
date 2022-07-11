//
//  CKQueryOperation.cs
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
    /// A database operation used to lookup record ids using the provided query
    /// </summary>
    public class CKQueryOperation : CKDatabaseOperation, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        
        [DllImport(dll)]
        private static extern IntPtr CKQueryOperation_init(
            out IntPtr exceptionPtr
            );
        
        [DllImport(dll)]
        private static extern IntPtr CKQueryOperation_initWithQuery(
            IntPtr query, 
            out IntPtr exceptionPtr
            );
        
        [DllImport(dll)]
        private static extern IntPtr CKQueryOperation_initWithCursor(
            IntPtr cursor, 
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern IntPtr CKQueryOperation_GetPropQuery(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKQueryOperation_SetPropQuery(HandleRef ptr, IntPtr query, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr CKQueryOperation_GetPropCursor(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKQueryOperation_SetPropCursor(HandleRef ptr, IntPtr cursor, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr CKQueryOperation_GetPropZoneID(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKQueryOperation_SetPropZoneID(HandleRef ptr, IntPtr zoneID, out IntPtr exceptionPtr);

        // TODO: DLLPROPERTYSTRINGARRAY

        [DllImport(dll)]
        private static extern void CKQueryOperation_SetPropRecordFetchedHandler(HandleRef ptr, RecordFetchedDelegate recordFetchedHandler, out IntPtr exceptionPtr);

        [DllImport(dll)]
        private static extern void CKQueryOperation_SetPropQueryCompletionHandler(HandleRef ptr, QueryCompletionDelegate queryCompletionHandler, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern ulong CKQueryOperation_GetPropResultsLimit(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKQueryOperation_SetPropResultsLimit(HandleRef ptr, ulong resultsLimit, out IntPtr exceptionPtr);

        

        #endregion

        internal CKQueryOperation(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKQueryOperation(
            )
        {
            
            IntPtr ptr = CKQueryOperation_init(
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKQueryOperation(
            CKQuery query
            )
        {
            if(query == null)
                throw new ArgumentNullException(nameof(query));
            
            IntPtr ptr = CKQueryOperation_initWithQuery(
                query != null ? HandleRef.ToIntPtr(query.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKQueryOperation(
            CKQueryCursor cursor
            )
        {
            if(cursor == null)
                throw new ArgumentNullException(nameof(cursor));
            
            IntPtr ptr = CKQueryOperation_initWithCursor(
                cursor != null ? HandleRef.ToIntPtr(cursor.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        
        /// <value>Query</value>
        public CKQuery Query
        {
            get 
            { 
                IntPtr query = CKQueryOperation_GetPropQuery(Handle);
                return query == IntPtr.Zero ? null : new CKQuery(query);
            }
            set
            {
                CKQueryOperation_SetPropQuery(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>Cursor</value>
        public CKQueryCursor Cursor
        {
            get 
            { 
                IntPtr cursor = CKQueryOperation_GetPropCursor(Handle);
                return cursor == IntPtr.Zero ? null : new CKQueryCursor(cursor);
            }
            set
            {
                CKQueryOperation_SetPropCursor(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>ZoneID</value>
        public CKRecordZoneID ZoneID
        {
            get 
            { 
                IntPtr zoneID = CKQueryOperation_GetPropZoneID(Handle);
                return zoneID == IntPtr.Zero ? null : new CKRecordZoneID(zoneID);
            }
            set
            {
                CKQueryOperation_SetPropZoneID(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero, out IntPtr exceptionPtr);
            }
        }

        
        // TODO: PROPERTYSTRINGARRAY
        
        /// <value>RecordFetchedHandler</value>
        public Action<CKRecord> RecordFetchedHandler
        {
            get 
            {
                RecordFetchedHandlerCallbacks.TryGetValue(
                    HandleRef.ToIntPtr(Handle), 
                    out ExecutionContext<CKRecord> value);
                return value.Callback;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    RecordFetchedHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    RecordFetchedHandlerCallbacks[myPtr] = new ExecutionContext<CKRecord>(value);
                }
                CKQueryOperation_SetPropRecordFetchedHandler(Handle, RecordFetchedHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,ExecutionContext<CKRecord>> RecordFetchedHandlerCallbacks = new Dictionary<IntPtr,ExecutionContext<CKRecord>>();

        [MonoPInvokeCallback(typeof(RecordFetchedDelegate))]
        private static void RecordFetchedHandlerCallback(IntPtr thisPtr, IntPtr _record)
        {
            if(RecordFetchedHandlerCallbacks.TryGetValue(thisPtr, out ExecutionContext<CKRecord> callback))
            {
                callback.Invoke(
                        _record == IntPtr.Zero ? null : new CKRecord(_record));
            }
        }

        
        /// <value>QueryCompletionHandler</value>
        public Action<CKQueryCursor,NSError> QueryCompletionHandler
        {
            get 
            {
                QueryCompletionHandlerCallbacks.TryGetValue(
                    HandleRef.ToIntPtr(Handle), 
                    out ExecutionContext<CKQueryCursor,NSError> value);
                return value.Callback;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    QueryCompletionHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    QueryCompletionHandlerCallbacks[myPtr] = new ExecutionContext<CKQueryCursor,NSError>(value);
                }
                CKQueryOperation_SetPropQueryCompletionHandler(Handle, QueryCompletionHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,ExecutionContext<CKQueryCursor,NSError>> QueryCompletionHandlerCallbacks = new Dictionary<IntPtr,ExecutionContext<CKQueryCursor,NSError>>();

        [MonoPInvokeCallback(typeof(QueryCompletionDelegate))]
        private static void QueryCompletionHandlerCallback(IntPtr thisPtr, IntPtr _cursor, IntPtr _operationError)
        {
            if(QueryCompletionHandlerCallbacks.TryGetValue(thisPtr, out ExecutionContext<CKQueryCursor,NSError> callback))
            {
                callback.Invoke(
                        _cursor == IntPtr.Zero ? null : new CKQueryCursor(_cursor),
                        _operationError == IntPtr.Zero ? null : new NSError(_operationError));
            }
        }

        
        /// <value>ResultsLimit</value>
        public ulong ResultsLimit
        {
            get 
            { 
                ulong resultsLimit = CKQueryOperation_GetPropResultsLimit(Handle);
                return resultsLimit;
            }
            set
            {
                CKQueryOperation_SetPropResultsLimit(Handle, value, out IntPtr exceptionPtr);
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKQueryOperation_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKQueryOperation Dispose");
                CKQueryOperation_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKQueryOperation()
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
