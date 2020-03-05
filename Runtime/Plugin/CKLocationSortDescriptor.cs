//
//  CKLocationSortDescriptor.cs
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
    public class CKLocationSortDescriptor : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKLocationSortDescriptor_initWithCoder(
            IntPtr aDecoder, 
            out IntPtr exceptionPtr
            );
        

        // Instance Methods
        

        

        // Properties
        
        #endregion

        internal CKLocationSortDescriptor(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKLocationSortDescriptor initWithCoder(
            NSCoder aDecoder
            )
        {
            if(aDecoder == null)
                throw new ArgumentNullException(nameof(aDecoder));
            
            IntPtr ptr = CKLocationSortDescriptor_initWithCoder(
                aDecoder != null ? HandleRef.ToIntPtr(aDecoder.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            return new CKLocationSortDescriptor(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        #endregion
        
        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKLocationSortDescriptor_Dispose(HandleRef handle);
            
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
                
                //Debug.Log("CKLocationSortDescriptor Dispose");
                CKLocationSortDescriptor_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKLocationSortDescriptor()
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
