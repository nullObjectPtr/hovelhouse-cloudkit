//
//  NSError.cs
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
    public class NSError : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        

        // Constructors
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern long NSError_GetPropCode(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSError_GetPropLocalizedDescription(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSError_GetPropLocalizedRecoverySuggestion(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSError_GetPropLocalizedFailureReason(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSError_GetPropHelpAnchor(HandleRef ptr);
        // TODO: DLLPROPERTYSTRINGARRAY
        
        #endregion

        internal NSError(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public long Code 
        {
            get 
            { 
                long code = NSError_GetPropCode(Handle);
                return code;
            }
        }
        
        public string LocalizedDescription 
        {
            get 
            { 
                IntPtr localizedDescription = NSError_GetPropLocalizedDescription(Handle);
                return Marshal.PtrToStringAuto(localizedDescription);
            }
        }
        
        public string LocalizedRecoverySuggestion 
        {
            get 
            { 
                IntPtr localizedRecoverySuggestion = NSError_GetPropLocalizedRecoverySuggestion(Handle);
                return Marshal.PtrToStringAuto(localizedRecoverySuggestion);
            }
        }
        
        public string LocalizedFailureReason 
        {
            get 
            { 
                IntPtr localizedFailureReason = NSError_GetPropLocalizedFailureReason(Handle);
                return Marshal.PtrToStringAuto(localizedFailureReason);
            }
        }
        
        public string HelpAnchor 
        {
            get 
            { 
                IntPtr helpAnchor = NSError_GetPropHelpAnchor(Handle);
                return Marshal.PtrToStringAuto(helpAnchor);
            }
        }
        
        // TODO: PROPERTYSTRINGARRAY
        
        #endregion
        
        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void NSError_Dispose(HandleRef handle);
            
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
                
                //Debug.Log("NSError Dispose");
                NSError_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~NSError()
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
