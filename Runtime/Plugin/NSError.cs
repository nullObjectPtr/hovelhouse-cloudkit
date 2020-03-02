//
//  NSError.cs
//
//  Created by Jonathan on 02/25/2020
//  Copyright © 2020 HovelHouseApps. All rights reserved.
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
    public class NSError : CKObject
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
    }
}
