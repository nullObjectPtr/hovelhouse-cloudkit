//
//  NSPredicate.cs
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
    public class NSPredicate : CKObject, IDisposable
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
        
        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void NSPredicate_Dispose(HandleRef handle);
            
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
                
                //Debug.Log("NSPredicate Dispose");
                NSPredicate_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~NSPredicate()
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
