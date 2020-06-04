//
//  NSException.cs
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
    /// An exception object
    /// </summary>
    /// <remarks>
    /// Apple makes a heavy distinction between errors and exceptions. Exceptions are typically generated when incorrect paramaters are sent to a method. Whenever a native exception is generated, the exception is caught and sent up to the plugin to be re-thrown as a managed CloudKitException. The CloudKitException will contain a reference to the NativeException which can be used to get it&apos;s type, and callstack if needed.
    /// </remarks>
    public class NSException : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        

        

        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr NSException_GetPropName(HandleRef ptr);

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr NSException_GetPropReason(HandleRef ptr);

        // TODO: DLLPROPERTYSTRINGARRAY

        

        #endregion

        internal NSException(IntPtr ptr) : base(ptr) {}
        
        
        
        


        
        
        
        /// <value>Name</value>
        public string Name
        {
            get 
            { 
                IntPtr name = NSException_GetPropName(Handle);
                return Marshal.PtrToStringAuto(name);
            }
        }

        
        /// <value>Reason</value>
        public string Reason
        {
            get 
            { 
                IntPtr reason = NSException_GetPropReason(Handle);
                return Marshal.PtrToStringAuto(reason);
            }
        }

        
        // TODO: PROPERTYSTRINGARRAY
        

        

        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern void NSException_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("NSException Dispose");
                NSException_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~NSException()
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
