//
//  NSFileManager.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 03/13/2020
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
    public class NSFileManager : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSFileManager_defaultManager(
            out IntPtr exceptionPtr);
        

        

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSFileManager_URLForUbiquityContainerIdentifier(
            HandleRef ptr, 
            string containerIdentifier,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern bool NSFileManager_isUbiquitousItemAtURL(
            HandleRef ptr, 
            IntPtr url,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern bool NSFileManager_startDownloadingUbiquitousItemAtURL_error(
            HandleRef ptr, 
            IntPtr url,
            IntPtr error,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern bool NSFileManager_evictUbiquitousItemAtURL_error(
            HandleRef ptr, 
            IntPtr url,
            IntPtr error,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
                private static extern bool NSFileManager_setUbiquitous_itemAtURL_destinationURL_error(
            HandleRef ptr, 
            bool flag,
            IntPtr url,
            IntPtr destinationURL,
            out IntPtr error,
            out IntPtr exceptionPtr);
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSFileManager_GetPropUbiquityIdentityToken(HandleRef ptr);
        

        #endregion

        internal NSFileManager(IntPtr ptr) : base(ptr) {}
        
        
        
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
        

        
        
        
        public UbiquityIdentityToken UbiquityIdentityToken 
        {
            get 
            { 
                IntPtr ubiquityIdentityToken = NSFileManager_GetPropUbiquityIdentityToken(Handle);
                return new UbiquityIdentityToken(ubiquityIdentityToken);
            }
        }
        

        

        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
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
