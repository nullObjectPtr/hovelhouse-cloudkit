//
//  NSURL.cs
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
    public class NSURL : CKObject
    {
        #region dll

        // Class Methods
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSURL_URLWithString(
            string URLString);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSURL_fileURLWithPath(
            string path);
        

        // Constructors
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSURL_GetPropAbsoluteString(HandleRef ptr);
        
        #endregion

        internal NSURL(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        public static NSURL URLWithString(
            string URLString)
        {
            if(URLString == null)
                throw new ArgumentNullException(nameof(URLString));
            
            var val = NSURL_URLWithString(URLString);
            return val == IntPtr.Zero ? null : new NSURL(val);
        }
        
        public static NSURL fileURLWithPath(
            string path)
        {
            if(path == null)
                throw new ArgumentNullException(nameof(path));
            
            var val = NSURL_fileURLWithPath(path);
            return val == IntPtr.Zero ? null : new NSURL(val);
        }
        
        #endregion

        #region Constructors
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public string AbsoluteString 
        {
            get 
            { 
                IntPtr absoluteString = NSURL_GetPropAbsoluteString(Handle);
                return Marshal.PtrToStringAuto(absoluteString);
            }
        }
        
        #endregion
    }
}
