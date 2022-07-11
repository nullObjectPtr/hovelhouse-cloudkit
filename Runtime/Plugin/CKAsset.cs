//
//  CKAsset.cs
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
    /// An object wrapper for a large file in the database
    /// </summary>
    /// <remarks>
    /// Apple reccomends that large files (think binary assets) be stored in Records using the CKAsset field type instead of a byte-array. The CKAsset will point to a URL where the asset is hosted. Until the asset is syncd with CloudKit this URL may be a file URL that points to the asset on the users local file system.
    /// </remarks>
    public class CKAsset : CKObject, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        
        [DllImport(dll)]
        private static extern IntPtr CKAsset_initWithFileURL(
            IntPtr fileURL, 
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern IntPtr CKAsset_GetPropFileURL(HandleRef ptr);

        

        #endregion

        internal CKAsset(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKAsset(
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

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        
        /// <value>FileURL</value>
        public NSURL FileURL
        {
            get 
            { 
                IntPtr fileURL = CKAsset_GetPropFileURL(Handle);
                return fileURL == IntPtr.Zero ? null : new NSURL(fileURL);
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKAsset_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
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
