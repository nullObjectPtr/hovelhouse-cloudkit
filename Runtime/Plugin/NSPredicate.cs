//
//  NSPredicate.cs
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
    public class NSPredicate : CKObject
    {
        #region dll

        // Class Methods
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSPredicate_predicateWithValue(
            bool value);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSPredicate_predicateWithFormat(
            string predicateFormat);
        

        // Constructors
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSPredicate_GetPropPredicateFormat(HandleRef ptr);
        
        #endregion

        internal NSPredicate(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        public static NSPredicate predicateWithValue(
            bool value)
        {
            
            var val = NSPredicate_predicateWithValue(value);
            return val == IntPtr.Zero ? null : new NSPredicate(val);
        }
        
        public static NSPredicate predicateWithFormat(
            string predicateFormat)
        {
            
            var val = NSPredicate_predicateWithFormat(predicateFormat);
            return val == IntPtr.Zero ? null : new NSPredicate(val);
        }
        
        #endregion

        #region Constructors
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public string PredicateFormat 
        {
            get 
            { 
                IntPtr predicateFormat = NSPredicate_GetPropPredicateFormat(Handle);
                return Marshal.PtrToStringAuto(predicateFormat);
            }
        }
        
        #endregion
    }
}
