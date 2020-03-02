//
//  CKAsset.cs
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
    public class CKAsset : CKObject
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKAsset_initWithFileURL(
            IntPtr fileURL);
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKAsset_GetPropFileURL(HandleRef ptr);
        
        #endregion

        internal CKAsset(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKAsset initWithFileURL(
            NSURL fileURL
        ){
            if(fileURL == null)
                throw new ArgumentNullException(nameof(fileURL));
            
            IntPtr ptr = CKAsset_initWithFileURL(
                fileURL != null ? HandleRef.ToIntPtr(fileURL.Handle) : IntPtr.Zero);
            return new CKAsset(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public NSURL FileURL 
        {
            get 
            { 
                IntPtr fileURL = CKAsset_GetPropFileURL(Handle);
                return fileURL == IntPtr.Zero ? null : new NSURL(fileURL);
            }
        }
        
        #endregion
    }
}
