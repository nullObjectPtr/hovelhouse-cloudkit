//
//  CKAsset.cs
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
    public class CKAsset : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKAsset_initWithFileURL(
            IntPtr fileURL, 
            out IntPtr exceptionPtr
            );
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKAsset_GetPropFileURL(HandleRef ptr);
        
        #endregion

        internal CKAsset(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKAsset initWithFileURL(
            NSURL fileURL
            )
        {
            if(fileURL == null)
                throw new ArgumentNullException(nameof(fileURL));
            
            IntPtr ptr = CKAsset_initWithFileURL(
                fileURL != null ? HandleRef.ToIntPtr(fileURL.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            return new CKAsset(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public NSURL FileURL 
        {
            get 
            { 
                IntPtr fileURL = CKAsset_GetPropFileURL(Handle);
                return fileURL == IntPtr.Zero ? null : new NSURL(fileURL);
            }
        }
        
        #endregion
        
        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKAsset_Dispose(HandleRef handle);
            
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
                
                //Debug.Log("CKAsset Dispose");
                CKAsset_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKAsset()
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
