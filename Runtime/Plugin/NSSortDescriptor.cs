//
//  NSSortDescriptor.cs
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
    /// Use this to specificy an ordering for database records
    /// </summary>
    public class NSSortDescriptor : CKObject, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        
        [DllImport(dll)]
        private static extern IntPtr NSSortDescriptor_sortDescriptorWithKey_ascending(
            string key,
            bool ascending,
            out IntPtr exceptionPtr);



        [DllImport(dll)]
        private static extern IntPtr NSSortDescriptor_initWithKey_ascending(
            string key, 
            bool ascending, 
            out IntPtr exceptionPtr
            );
        

        

        
        [DllImport(dll)]
        private static extern void NSSortDescriptor_allowEvaluation(
            HandleRef ptr, 
            out IntPtr exceptionPtr);

        

        // Properties
        
        [DllImport(dll)]
        private static extern bool NSSortDescriptor_GetPropAscending(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr NSSortDescriptor_GetPropKey(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr NSSortDescriptor_GetPropReversedSortDescriptor(HandleRef ptr);

        

        #endregion

        internal NSSortDescriptor(IntPtr ptr) : base(ptr) {}
        
        
        /// <summary>
        /// </summary>
        /// <param name="key"></param><param name="ascending"></param>
        /// <returns>val</returns>
        public static NSSortDescriptor SortDescriptorWithKey(
            string key, 
            bool ascending)
        { 
            
            
            var val = NSSortDescriptor_sortDescriptorWithKey_ascending(
                key, 
                
                ascending, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val == IntPtr.Zero ? null : new NSSortDescriptor(val);
        }
        

        
        
        
        public NSSortDescriptor(
            string key, 
            bool ascending
            )
        {
            IntPtr ptr = NSSortDescriptor_initWithKey_ascending(
                key, 
                ascending, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        /// <summary>
        /// </summary>
        /// 
        /// <returns>void</returns>
        public void AllowEvaluation()
        { 
            NSSortDescriptor_allowEvaluation(
                Handle, out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        

        
        
        /// <value>Ascending</value>
        public bool Ascending
        {
            get 
            { 
                bool ascending = NSSortDescriptor_GetPropAscending(Handle);
                return ascending;
            }
        }

        
        /// <value>Key</value>
        public string Key
        {
            get 
            { 
                IntPtr key = NSSortDescriptor_GetPropKey(Handle);
                return Marshal.PtrToStringAuto(key);
            }
        }

        
        /// <value>ReversedSortDescriptor</value>
        public NSSortDescriptor ReversedSortDescriptor
        {
            get 
            { 
                IntPtr reversedSortDescriptor = NSSortDescriptor_GetPropReversedSortDescriptor(Handle);
                return reversedSortDescriptor == IntPtr.Zero ? null : new NSSortDescriptor(reversedSortDescriptor);
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void NSSortDescriptor_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("NSSortDescriptor Dispose");
                NSSortDescriptor_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~NSSortDescriptor()
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
