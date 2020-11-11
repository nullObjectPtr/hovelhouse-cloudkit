//
//  NSError.cs
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
    /// An error object
    /// </summary>
    /// <remarks>
    /// Unlike C# apple makes a big distinction between Error&apos;s and Exceptions. All CKOperations will include an error as a parameter in it&apos;s handlers to indicate the failure of the operation. Use the code field provided and compare them against the various error enums to determine the type of error that happened.
    /// </remarks>
    public class NSError : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        

        

        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern long NSError_GetPropCode(HandleRef ptr);

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr NSError_GetPropLocalizedDescription(HandleRef ptr);

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr NSError_GetPropLocalizedRecoverySuggestion(HandleRef ptr);

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr NSError_GetPropLocalizedFailureReason(HandleRef ptr);

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr NSError_GetPropHelpAnchor(HandleRef ptr);

        // TODO: DLLPROPERTYSTRINGARRAY

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr NSError_GetPropDomain(HandleRef ptr);

        

        #endregion

        internal NSError(IntPtr ptr) : base(ptr) {}
        
        
        
        


        
        
        
        /// <value>Code</value>
        public long Code
        {
            get 
            { 
                long code = NSError_GetPropCode(Handle);
                return code;
            }
        }

        
        /// <value>LocalizedDescription</value>
        public string LocalizedDescription
        {
            get 
            { 
                IntPtr localizedDescription = NSError_GetPropLocalizedDescription(Handle);
                return Marshal.PtrToStringAuto(localizedDescription);
            }
        }

        
        /// <value>LocalizedRecoverySuggestion</value>
        public string LocalizedRecoverySuggestion
        {
            get 
            { 
                IntPtr localizedRecoverySuggestion = NSError_GetPropLocalizedRecoverySuggestion(Handle);
                return Marshal.PtrToStringAuto(localizedRecoverySuggestion);
            }
        }

        
        /// <value>LocalizedFailureReason</value>
        public string LocalizedFailureReason
        {
            get 
            { 
                IntPtr localizedFailureReason = NSError_GetPropLocalizedFailureReason(Handle);
                return Marshal.PtrToStringAuto(localizedFailureReason);
            }
        }

        
        /// <value>HelpAnchor</value>
        public string HelpAnchor
        {
            get 
            { 
                IntPtr helpAnchor = NSError_GetPropHelpAnchor(Handle);
                return Marshal.PtrToStringAuto(helpAnchor);
            }
        }

        
        // TODO: PROPERTYSTRINGARRAY
        
        /// <value>Domain</value>
        public string Domain
        {
            get 
            { 
                IntPtr domain = NSError_GetPropDomain(Handle);
                return Marshal.PtrToStringAuto(domain);
            }
        }

        

        

        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern void NSError_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
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
