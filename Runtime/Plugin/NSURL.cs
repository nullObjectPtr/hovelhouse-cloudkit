//
//  NSURL.cs
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
    public class NSURL : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSURL_URLWithString(
            string URLString,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSURL_fileURLWithPath(
            string path,
            out IntPtr exceptionPtr);
        

        // Constructors
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSURL_GetPropAbsoluteString(HandleRef ptr);
        
        #endregion

        internal NSURL(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        
        public static NSURL URLWithString(
            string URLString)
        { 
            if(URLString == null)
                throw new ArgumentNullException(nameof(URLString));
            
            var val = NSURL_URLWithString(
                URLString, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val == IntPtr.Zero ? null : new NSURL(val);
        }
        

        
        
        public static NSURL FileURLWithPath(
            string path)
        { 
            if(path == null)
                throw new ArgumentNullException(nameof(path));
            
            var val = NSURL_fileURLWithPath(
                path, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val == IntPtr.Zero ? null : new NSURL(val);
        }
        

        
        #endregion

        #region Constructors
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public string AbsoluteString 
        {
            get 
            { 
                IntPtr absoluteString = NSURL_GetPropAbsoluteString(Handle);
                return Marshal.PtrToStringAuto(absoluteString);
            }
        }
        
        #endregion
        
        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void NSURL_Dispose(HandleRef handle);
            
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
                
                //Debug.Log("NSURL Dispose");
                NSURL_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~NSURL()
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
