//
//  CKQueryOperation.cs
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
    public class CKQueryOperation : CKObject
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKQueryOperation_init();
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKQueryOperation_initWithQuery(
            IntPtr query);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKQueryOperation_initWithCursor(
            IntPtr cursor);
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKQueryOperation_GetPropQuery(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKQueryOperation_SetPropQuery(HandleRef ptr, IntPtr query);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKQueryOperation_GetPropCursor(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKQueryOperation_SetPropCursor(HandleRef ptr, IntPtr cursor);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKQueryOperation_GetPropZoneID(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKQueryOperation_SetPropZoneID(HandleRef ptr, IntPtr zoneID);
        // TODO: DLLPROPERTYSTRINGARRAY
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKQueryOperation_SetPropRecordFetchedHandler(HandleRef ptr, RecordFetchedDelegate recordFetchedHandler);
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKQueryOperation_SetPropQueryCompletionHandler(HandleRef ptr, QueryCompletionDelegate queryCompletionHandler);
        
        #endregion

        internal CKQueryOperation(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKQueryOperation init(
        ){
            
            IntPtr ptr = CKQueryOperation_init();
            return new CKQueryOperation(ptr);
        }
        
        
        public static CKQueryOperation initWithQuery(
            CKQuery query
        ){
            if(query == null)
                throw new ArgumentNullException(nameof(query));
            
            IntPtr ptr = CKQueryOperation_initWithQuery(
                query != null ? HandleRef.ToIntPtr(query.Handle) : IntPtr.Zero);
            return new CKQueryOperation(ptr);
        }
        
        
        public static CKQueryOperation initWithCursor(
            CKQueryCursor cursor
        ){
            if(cursor == null)
                throw new ArgumentNullException(nameof(cursor));
            
            IntPtr ptr = CKQueryOperation_initWithCursor(
                cursor != null ? HandleRef.ToIntPtr(cursor.Handle) : IntPtr.Zero);
            return new CKQueryOperation(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public CKQuery Query 
        {
            get 
            { 
                IntPtr query = CKQueryOperation_GetPropQuery(Handle);
                return query == IntPtr.Zero ? null : new CKQuery(query);
            }
            set
            {
                CKQueryOperation_SetPropQuery(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero);
            }
        }
        
        public CKQueryCursor Cursor 
        {
            get 
            { 
                IntPtr cursor = CKQueryOperation_GetPropCursor(Handle);
                return cursor == IntPtr.Zero ? null : new CKQueryCursor(cursor);
            }
            set
            {
                CKQueryOperation_SetPropCursor(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero);
            }
        }
        
        public CKRecordZoneID ZoneID 
        {
            get 
            { 
                IntPtr zoneID = CKQueryOperation_GetPropZoneID(Handle);
                return zoneID == IntPtr.Zero ? null : new CKRecordZoneID(zoneID);
            }
            set
            {
                CKQueryOperation_SetPropZoneID(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero);
            }
        }
        
        // TODO: PROPERTYSTRINGARRAY
        
        public Action<CKRecord> RecordFetchedHandler 
        {
            get 
            {
                Action<CKRecord> value;
                RecordFetchedHandlerCallbacks.TryGetValue(HandleRef.ToIntPtr(Handle), out value);
                return value;
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
                    RecordFetchedHandlerCallbacks[myPtr] = value;
                }
                CKQueryOperation_SetPropRecordFetchedHandler(Handle, RecordFetchedHandlerCallback);
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKRecord>> RecordFetchedHandlerCallbacks = new Dictionary<IntPtr,Action<CKRecord>>();

        [MonoPInvokeCallback(typeof(RecordFetchedDelegate))]
        private static void RecordFetchedHandlerCallback(IntPtr thisPtr, IntPtr _record)
        {
            if(RecordFetchedHandlerCallbacks.TryGetValue(thisPtr, out Action<CKRecord> callback))
            {
                try
                {
                    callback(
                        _record == IntPtr.Zero ? null : new CKRecord(_record));
                }
                catch(Exception exc)
                {
                    Debug.LogError(exc);
                }
            }
        }

        
        public Action<CKQueryCursor,NSError> QueryCompletionHandler 
        {
            get 
            {
                Action<CKQueryCursor,NSError> value;
                QueryCompletionHandlerCallbacks.TryGetValue(HandleRef.ToIntPtr(Handle), out value);
                return value;
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
                    QueryCompletionHandlerCallbacks[myPtr] = value;
                }
                CKQueryOperation_SetPropQueryCompletionHandler(Handle, QueryCompletionHandlerCallback);
            }
        }

        private static readonly Dictionary<IntPtr,Action<CKQueryCursor,NSError>> QueryCompletionHandlerCallbacks = new Dictionary<IntPtr,Action<CKQueryCursor,NSError>>();

        [MonoPInvokeCallback(typeof(QueryCompletionDelegate))]
        private static void QueryCompletionHandlerCallback(IntPtr thisPtr, IntPtr _cursor, IntPtr _operationError)
        {
            if(QueryCompletionHandlerCallbacks.TryGetValue(thisPtr, out Action<CKQueryCursor,NSError> callback))
            {
                try
                {
                    callback(
                        _cursor == IntPtr.Zero ? null : new CKQueryCursor(_cursor),
                        _operationError == IntPtr.Zero ? null : new NSError(_operationError));
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
