//
//  NSFileManager.cs
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
    /// A limited implementation of NSFileManager. Use this to write to iCloud Documents or get the current Ubiquity information.
    /// </summary>
    /// <remarks>
    /// This class is only partially implemented and contains only the methods which may be of use to CloudKit. The un-implemented methods are not likely needed since NSFileManager largely duplicates the functionality already provided by C#&apos;s File and Directory classes.
    /// </remarks>
    public class NSFileManager : CKObject, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        
        [DllImport(dll)]
        private static extern IntPtr NSFileManager_defaultManager(
            out IntPtr exceptionPtr);

        

        

        
        [DllImport(dll)]
        private static extern IntPtr NSFileManager_URLForUbiquityContainerIdentifier(
            HandleRef ptr, 
            string containerIdentifier,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern bool NSFileManager_isUbiquitousItemAtURL(
            HandleRef ptr, 
            IntPtr url,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern bool NSFileManager_startDownloadingUbiquitousItemAtURL_error(
            HandleRef ptr, 
            IntPtr url,
            IntPtr error,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern bool NSFileManager_evictUbiquitousItemAtURL_error(
            HandleRef ptr, 
            IntPtr url,
            IntPtr error,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern bool NSFileManager_setUbiquitous_itemAtURL_destinationURL_error(
            HandleRef ptr, 
            bool flag,
            IntPtr url,
            IntPtr destinationURL,
            out IntPtr error,
            out IntPtr exceptionPtr);

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern IntPtr NSFileManager_GetPropUbiquityIdentityToken(HandleRef ptr);

        

        #endregion

        internal NSFileManager(IntPtr ptr) : base(ptr) {}
        
        
        /// <summary>
        /// </summary>
        /// 
        /// <returns>val</returns>
        public static NSFileManager DefaultManager()
        { 
            var val = NSFileManager_defaultManager(out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val == IntPtr.Zero ? null : new NSFileManager(val);
        }
        

        
        
        


        
        /// <summary>
        /// </summary>
        /// <param name="containerIdentifier"></param>
        /// <returns>val</returns>
        public NSURL URLForUbiquityContainerIdentifier(
            string containerIdentifier)
        { 
            
            var val = NSFileManager_URLForUbiquityContainerIdentifier(
                Handle, 
                containerIdentifier, 
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
        /// <param name="url"></param>
        /// <returns>val</returns>
        public bool IsUbiquitousItemAtURL(
            NSURL url)
        { 
            
            var val = NSFileManager_isUbiquitousItemAtURL(
                Handle, 
                url != null ? HandleRef.ToIntPtr(url.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val;
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="url"></param><param name="error"></param>
        /// <returns>val</returns>
        public bool StartDownloadingUbiquitousItemAtURL(
            NSURL url, 
            NSError error)
        { 
            
            
            var val = NSFileManager_startDownloadingUbiquitousItemAtURL_error(
                Handle, 
                url != null ? HandleRef.ToIntPtr(url.Handle) : IntPtr.Zero, 
                
                error != null ? HandleRef.ToIntPtr(error.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val;
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="url"></param><param name="error"></param>
        /// <returns>val</returns>
        public bool EvictUbiquitousItemAtURL(
            NSURL url, 
            NSError error)
        { 
            
            
            var val = NSFileManager_evictUbiquitousItemAtURL_error(
                Handle, 
                url != null ? HandleRef.ToIntPtr(url.Handle) : IntPtr.Zero, 
                
                error != null ? HandleRef.ToIntPtr(error.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val;
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="flag"></param><param name="url"></param><param name="destinationURL"></param><param name="error"></param>
        /// <returns>val</returns>
        public bool SetUbiquitousItemAtURL(
            bool flag, 
            NSURL url, 
            NSURL destinationURL, 
            out NSError error)
        {
            IntPtr errorPtr;
            
            var val = NSFileManager_setUbiquitous_itemAtURL_destinationURL_error(
                Handle, 
                flag,
                url != null ? HandleRef.ToIntPtr(url.Handle) : IntPtr.Zero,
                destinationURL != null ? HandleRef.ToIntPtr(destinationURL.Handle) : IntPtr.Zero, 
                out errorPtr, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            error = errorPtr == IntPtr.Zero ? null : new NSError(errorPtr);
            
            return val;
        }
        

        
        
        
        /// <value>UbiquityIdentityToken</value>
        public UbiquityIdentityToken UbiquityIdentityToken
        {
            get 
            { 
                IntPtr ubiquityIdentityToken = NSFileManager_GetPropUbiquityIdentityToken(Handle);
                return new UbiquityIdentityToken(ubiquityIdentityToken);
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void NSFileManager_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("NSFileManager Dispose");
                NSFileManager_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~NSFileManager()
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
