//
//  CKLocationSortDescriptor.cs
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
    /// Sorts records based on their distance from a given location.
    /// </summary>
    public class CKLocationSortDescriptor : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr CKLocationSortDescriptor_initWithCoder(
            IntPtr aDecoder, 
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        

        #endregion

        internal CKLocationSortDescriptor(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKLocationSortDescriptor(
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

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        

        

        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern void CKLocationSortDescriptor_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
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
