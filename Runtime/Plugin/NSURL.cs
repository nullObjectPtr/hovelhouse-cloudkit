//
//  NSURL.cs
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
    /// A limited implementation of NSUrl
    /// </summary>
    /// <remarks>
    /// NSURL is a huge class. Only a small part of it is implemented since most of it&apos;s functionality is duplicated by unity&apos;s URL class.
    /// </remarks>
    public class NSURL : CKObject, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        
        [DllImport(dll)]
        private static extern IntPtr NSURL_URLWithString(
            string URLString,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr NSURL_fileURLWithPath(
            string path,
            out IntPtr exceptionPtr);

        

        

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern IntPtr NSURL_GetPropAbsoluteString(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr NSURL_GetPropAbsoluteURL(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr NSURL_GetPropBaseURL(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr NSURL_GetPropLastPathComponent(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr NSURL_GetPropHost(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr NSURL_GetPropPassword(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr NSURL_GetPropPath(HandleRef ptr);

        // TODO: DLLPROPERTYSTRINGARRAY

        
        [DllImport(dll)]
        private static extern IntPtr NSURL_GetPropPathExtension(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr NSURL_GetPropQuery(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr NSURL_GetPropRelativePath(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr NSURL_GetPropResourceSpecifier(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr NSURL_GetPropScheme(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr NSURL_GetPropStandardizedURL(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr NSURL_GetPropUser(HandleRef ptr);

        

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

        
        /// <value>AbsoluteURL</value>
        public NSURL AbsoluteURL
        {
            get 
            { 
                IntPtr absoluteURL = NSURL_GetPropAbsoluteURL(Handle);
                return absoluteURL == IntPtr.Zero ? null : new NSURL(absoluteURL);
            }
        }

        
        /// <value>BaseURL</value>
        public NSURL BaseURL
        {
            get 
            { 
                IntPtr baseURL = NSURL_GetPropBaseURL(Handle);
                return baseURL == IntPtr.Zero ? null : new NSURL(baseURL);
            }
        }

        
        /// <value>LastPathComponent</value>
        public string LastPathComponent
        {
            get 
            { 
                IntPtr lastPathComponent = NSURL_GetPropLastPathComponent(Handle);
                return Marshal.PtrToStringAuto(lastPathComponent);
            }
        }

        
        /// <value>Host</value>
        public string Host
        {
            get 
            { 
                IntPtr host = NSURL_GetPropHost(Handle);
                return Marshal.PtrToStringAuto(host);
            }
        }

        
        /// <value>Password</value>
        public string Password
        {
            get 
            { 
                IntPtr password = NSURL_GetPropPassword(Handle);
                return Marshal.PtrToStringAuto(password);
            }
        }

        
        /// <value>Path</value>
        public string Path
        {
            get 
            { 
                IntPtr path = NSURL_GetPropPath(Handle);
                return Marshal.PtrToStringAuto(path);
            }
        }

        
        // TODO: PROPERTYSTRINGARRAY
        
        /// <value>PathExtension</value>
        public string PathExtension
        {
            get 
            { 
                IntPtr pathExtension = NSURL_GetPropPathExtension(Handle);
                return Marshal.PtrToStringAuto(pathExtension);
            }
        }

        
        /// <value>Query</value>
        public string Query
        {
            get 
            { 
                IntPtr query = NSURL_GetPropQuery(Handle);
                return Marshal.PtrToStringAuto(query);
            }
        }

        
        /// <value>RelativePath</value>
        public string RelativePath
        {
            get 
            { 
                IntPtr relativePath = NSURL_GetPropRelativePath(Handle);
                return Marshal.PtrToStringAuto(relativePath);
            }
        }

        
        /// <value>ResourceSpecifier</value>
        public string ResourceSpecifier
        {
            get 
            { 
                IntPtr resourceSpecifier = NSURL_GetPropResourceSpecifier(Handle);
                return Marshal.PtrToStringAuto(resourceSpecifier);
            }
        }

        
        /// <value>Scheme</value>
        public string Scheme
        {
            get 
            { 
                IntPtr scheme = NSURL_GetPropScheme(Handle);
                return Marshal.PtrToStringAuto(scheme);
            }
        }

        
        /// <value>StandardizedURL</value>
        public NSURL StandardizedURL
        {
            get 
            { 
                IntPtr standardizedURL = NSURL_GetPropStandardizedURL(Handle);
                return standardizedURL == IntPtr.Zero ? null : new NSURL(standardizedURL);
            }
        }

        
        /// <value>User</value>
        public string User
        {
            get 
            { 
                IntPtr user = NSURL_GetPropUser(Handle);
                return Marshal.PtrToStringAuto(user);
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
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
