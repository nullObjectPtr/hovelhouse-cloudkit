//
//  NSPersonNameComponents.cs
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
    public class NSPersonNameComponents : CKObject, IDisposable
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
        
        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void NSPersonNameComponents_Dispose(HandleRef handle);
            
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
                
                //Debug.Log("NSPersonNameComponents Dispose");
                NSPersonNameComponents_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~NSPersonNameComponents()
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
