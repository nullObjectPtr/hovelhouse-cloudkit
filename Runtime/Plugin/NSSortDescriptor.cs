//
//  NSSortDescriptor.cs
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
    public class NSSortDescriptor : CKObject
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSSortDescriptor_initWithCoder(
            IntPtr coder);
        

        // Instance Methods
        

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void NSSortDescriptor_allowEvaluation(
            HandleRef ptr);
        
        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern bool NSSortDescriptor_GetPropAscending(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSSortDescriptor_GetPropKey(HandleRef ptr);
        
        #endregion

        internal NSSortDescriptor(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static NSSortDescriptor initWithCoder(
            NSCoder coder
        ){
            if(coder == null)
                throw new ArgumentNullException(nameof(coder));
            
            IntPtr ptr = NSSortDescriptor_initWithCoder(
                coder != null ? HandleRef.ToIntPtr(coder.Handle) : IntPtr.Zero);
            return new NSSortDescriptor(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        
        public void AllowEvaluation()
        {
                            
            ;
            NSSortDescriptor_allowEvaluation(
                Handle);
            
        }
        

        
        #endregion

        #region Properties
        
        public bool Ascending 
        {
            get 
            { 
                bool ascending = NSSortDescriptor_GetPropAscending(Handle);
                return ascending;
            }
        }
        
        public string Key 
        {
            get 
            { 
                IntPtr key = NSSortDescriptor_GetPropKey(Handle);
                return Marshal.PtrToStringAuto(key);
            }
        }
        
        #endregion
    }
}
