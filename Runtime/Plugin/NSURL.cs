//
//  NSURL.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 03/26/2020
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
    /// A limited implementation of NSUrl
    /// </summary>
    /// <remarks>
    /// NSURL is a huge class. Only a small part of it is implemented since most of it&apos;s functionality is duplicated by unity&apos;s URL class.
    /// </remarks>
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
        

        

        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSURL_GetPropAbsoluteString(HandleRef ptr);
        

        #endregion

        internal NSURL(IntPtr ptr) : base(ptr) {}
        
        
        /// <summary>
        /// </summary>
        /// <param name="URLString"></param>
        /// <returns>val</returns>
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
        

        
        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <returns>val</returns>
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
        

        
        
        


        
        
        
        /// <value>AbsoluteString</value>
        public string AbsoluteString
        {
            get 
            { 
                IntPtr absoluteString = NSURL_GetPropAbsoluteString(Handle);
                return Marshal.PtrToStringAuto(absoluteString);
            }
        }

        

        

        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void NSURL_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
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
