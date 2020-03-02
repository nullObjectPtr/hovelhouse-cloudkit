//
//  NSPersonNameComponents.cs
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
    public class NSPersonNameComponents : CKObject
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
        private static extern IntPtr NSPersonNameComponents_GetPropNamePrefix(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void NSPersonNameComponents_SetPropNamePrefix(HandleRef ptr, string namePrefix);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSPersonNameComponents_GetPropGivenName(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void NSPersonNameComponents_SetPropGivenName(HandleRef ptr, string givenName);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSPersonNameComponents_GetPropMiddleName(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void NSPersonNameComponents_SetPropMiddleName(HandleRef ptr, string middleName);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSPersonNameComponents_GetPropFamilyName(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void NSPersonNameComponents_SetPropFamilyName(HandleRef ptr, string familyName);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSPersonNameComponents_GetPropNameSuffix(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void NSPersonNameComponents_SetPropNameSuffix(HandleRef ptr, string nameSuffix);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSPersonNameComponents_GetPropNickname(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void NSPersonNameComponents_SetPropNickname(HandleRef ptr, string nickname);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr NSPersonNameComponents_GetPropPhoneticRepresentation(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void NSPersonNameComponents_SetPropPhoneticRepresentation(HandleRef ptr, IntPtr phoneticRepresentation);
        
        #endregion

        internal NSPersonNameComponents(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public string NamePrefix 
        {
            get 
            { 
                IntPtr namePrefix = NSPersonNameComponents_GetPropNamePrefix(Handle);
                return Marshal.PtrToStringAuto(namePrefix);
            }
            set
            {
                NSPersonNameComponents_SetPropNamePrefix(Handle, value);
            }
        }
        
        public string GivenName 
        {
            get 
            { 
                IntPtr givenName = NSPersonNameComponents_GetPropGivenName(Handle);
                return Marshal.PtrToStringAuto(givenName);
            }
            set
            {
                NSPersonNameComponents_SetPropGivenName(Handle, value);
            }
        }
        
        public string MiddleName 
        {
            get 
            { 
                IntPtr middleName = NSPersonNameComponents_GetPropMiddleName(Handle);
                return Marshal.PtrToStringAuto(middleName);
            }
            set
            {
                NSPersonNameComponents_SetPropMiddleName(Handle, value);
            }
        }
        
        public string FamilyName 
        {
            get 
            { 
                IntPtr familyName = NSPersonNameComponents_GetPropFamilyName(Handle);
                return Marshal.PtrToStringAuto(familyName);
            }
            set
            {
                NSPersonNameComponents_SetPropFamilyName(Handle, value);
            }
        }
        
        public string NameSuffix 
        {
            get 
            { 
                IntPtr nameSuffix = NSPersonNameComponents_GetPropNameSuffix(Handle);
                return Marshal.PtrToStringAuto(nameSuffix);
            }
            set
            {
                NSPersonNameComponents_SetPropNameSuffix(Handle, value);
            }
        }
        
        public string Nickname 
        {
            get 
            { 
                IntPtr nickname = NSPersonNameComponents_GetPropNickname(Handle);
                return Marshal.PtrToStringAuto(nickname);
            }
            set
            {
                NSPersonNameComponents_SetPropNickname(Handle, value);
            }
        }
        
        public NSPersonNameComponents PhoneticRepresentation 
        {
            get 
            { 
                IntPtr phoneticRepresentation = NSPersonNameComponents_GetPropPhoneticRepresentation(Handle);
                return phoneticRepresentation == IntPtr.Zero ? null : new NSPersonNameComponents(phoneticRepresentation);
            }
            set
            {
                NSPersonNameComponents_SetPropPhoneticRepresentation(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero);
            }
        }
        
        #endregion
    }
}
