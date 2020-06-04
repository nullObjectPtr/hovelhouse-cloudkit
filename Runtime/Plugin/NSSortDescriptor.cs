//
//  NSSortDescriptor.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 05/28/2020
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
    /// <summary>
    /// Use this to specificy an ordering for database records
    /// </summary>
    public class NSSortDescriptor : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr NSSortDescriptor_initWithCoder(
            IntPtr coder, 
            out IntPtr exceptionPtr
            );
        

        

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern void NSSortDescriptor_allowEvaluation(
            HandleRef ptr, 
            out IntPtr exceptionPtr);

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern bool NSSortDescriptor_GetPropAscending(HandleRef ptr);

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr NSSortDescriptor_GetPropKey(HandleRef ptr);

        

        #endregion

        internal NSSortDescriptor(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public NSSortDescriptor(
            NSCoder coder
            )
        {
            if(coder == null)
                throw new ArgumentNullException(nameof(coder));
            
            IntPtr ptr = NSSortDescriptor_initWithCoder(
                coder != null ? HandleRef.ToIntPtr(coder.Handle) : IntPtr.Zero, 
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

        

        

        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
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
