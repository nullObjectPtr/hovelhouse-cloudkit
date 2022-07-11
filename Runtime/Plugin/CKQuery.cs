//
//  CKQuery.cs
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
    /// A query to run against the database
    /// </summary>
    /// <remarks>
    /// A query is a tuple of a RecordType and a predicate. A predicate is a query that is written in an apple speficic syntax similar in purpose to SQL
    /// </remarks>
    public class CKQuery : CKObject, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        
        [DllImport(dll)]
        private static extern IntPtr CKQuery_initWithRecordType_predicate(
            string recordType, 
            IntPtr predicate, 
            out IntPtr exceptionPtr
            );
        
        [DllImport(dll)]
        private static extern IntPtr CKQuery_initWithCoder(
            IntPtr aDecoder, 
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern void CKQuery_GetPropSortDescriptors(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        [DllImport(dll)]
        private static extern void CKQuery_SetPropSortDescriptors(HandleRef ptr, IntPtr[] sortDescriptors,
			int sortDescriptorsCount, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr CKQuery_GetPropRecordType(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr CKQuery_GetPropPredicate(HandleRef ptr);

        

        #endregion

        internal CKQuery(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKQuery(
            string recordType, 
            NSPredicate predicate
            )
        {
            if(recordType == null)
                throw new ArgumentNullException(nameof(recordType));
            if(predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            
            IntPtr ptr = CKQuery_initWithRecordType_predicate(
                recordType, 
                predicate != null ? HandleRef.ToIntPtr(predicate.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKQuery(
            NSCoder aDecoder
            )
        {
            if(aDecoder == null)
                throw new ArgumentNullException(nameof(aDecoder));
            
            IntPtr ptr = CKQuery_initWithCoder(
                aDecoder != null ? HandleRef.ToIntPtr(aDecoder.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        
        /// <value>SortDescriptors</value>
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
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * IntPtr.Size));
                    sortDescriptors[i] = ptr2 == IntPtr.Zero ? null : new NSSortDescriptor(ptr2);
                }

                Marshal.FreeHGlobal(bufferPtr);

                return sortDescriptors;
            }
            set
            {
                CKQuery_SetPropSortDescriptors(Handle, value == null ? null : value.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				value == null ? 0 : value.Length, out IntPtr exceptionPtr);
                
                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        
        /// <value>RecordType</value>
        public string RecordType
        {
            get 
            { 
                IntPtr recordType = CKQuery_GetPropRecordType(Handle);
                return Marshal.PtrToStringAuto(recordType);
            }
        }

        
        /// <value>Predicate</value>
        public NSPredicate Predicate
        {
            get 
            { 
                IntPtr predicate = CKQuery_GetPropPredicate(Handle);
                return predicate == IntPtr.Zero ? null : new NSPredicate(predicate);
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKQuery_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKQuery Dispose");
                CKQuery_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKQuery()
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
