//
//  NSPersonNameComponents.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 05/28/2020
//  Copyright © 2021 HovelHouseApps. All rights reserved.
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
    /// Structured information about a person's name
    /// </summary>
    public class NSPersonNameComponents : CKObject, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern IntPtr NSPersonNameComponents_GetPropNamePrefix(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void NSPersonNameComponents_SetPropNamePrefix(HandleRef ptr, string namePrefix, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr NSPersonNameComponents_GetPropGivenName(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void NSPersonNameComponents_SetPropGivenName(HandleRef ptr, string givenName, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr NSPersonNameComponents_GetPropMiddleName(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void NSPersonNameComponents_SetPropMiddleName(HandleRef ptr, string middleName, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr NSPersonNameComponents_GetPropFamilyName(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void NSPersonNameComponents_SetPropFamilyName(HandleRef ptr, string familyName, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr NSPersonNameComponents_GetPropNameSuffix(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void NSPersonNameComponents_SetPropNameSuffix(HandleRef ptr, string nameSuffix, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr NSPersonNameComponents_GetPropNickname(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void NSPersonNameComponents_SetPropNickname(HandleRef ptr, string nickname, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr NSPersonNameComponents_GetPropPhoneticRepresentation(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void NSPersonNameComponents_SetPropPhoneticRepresentation(HandleRef ptr, IntPtr phoneticRepresentation, out IntPtr exceptionPtr);

        

        #endregion

        internal NSPersonNameComponents(IntPtr ptr) : base(ptr) {}
        
        
        
        


        
        
        
        /// <value>NamePrefix</value>
        public string NamePrefix
        {
            get 
            { 
                IntPtr namePrefix = NSPersonNameComponents_GetPropNamePrefix(Handle);
                return Marshal.PtrToStringAuto(namePrefix);
            }
            set
            {
                NSPersonNameComponents_SetPropNamePrefix(Handle, value, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>GivenName</value>
        public string GivenName
        {
            get 
            { 
                IntPtr givenName = NSPersonNameComponents_GetPropGivenName(Handle);
                return Marshal.PtrToStringAuto(givenName);
            }
            set
            {
                NSPersonNameComponents_SetPropGivenName(Handle, value, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>MiddleName</value>
        public string MiddleName
        {
            get 
            { 
                IntPtr middleName = NSPersonNameComponents_GetPropMiddleName(Handle);
                return Marshal.PtrToStringAuto(middleName);
            }
            set
            {
                NSPersonNameComponents_SetPropMiddleName(Handle, value, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>FamilyName</value>
        public string FamilyName
        {
            get 
            { 
                IntPtr familyName = NSPersonNameComponents_GetPropFamilyName(Handle);
                return Marshal.PtrToStringAuto(familyName);
            }
            set
            {
                NSPersonNameComponents_SetPropFamilyName(Handle, value, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>NameSuffix</value>
        public string NameSuffix
        {
            get 
            { 
                IntPtr nameSuffix = NSPersonNameComponents_GetPropNameSuffix(Handle);
                return Marshal.PtrToStringAuto(nameSuffix);
            }
            set
            {
                NSPersonNameComponents_SetPropNameSuffix(Handle, value, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>Nickname</value>
        public string Nickname
        {
            get 
            { 
                IntPtr nickname = NSPersonNameComponents_GetPropNickname(Handle);
                return Marshal.PtrToStringAuto(nickname);
            }
            set
            {
                NSPersonNameComponents_SetPropNickname(Handle, value, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>PhoneticRepresentation</value>
        public NSPersonNameComponents PhoneticRepresentation
        {
            get 
            { 
                IntPtr phoneticRepresentation = NSPersonNameComponents_GetPropPhoneticRepresentation(Handle);
                return phoneticRepresentation == IntPtr.Zero ? null : new NSPersonNameComponents(phoneticRepresentation);
            }
            set
            {
                NSPersonNameComponents_SetPropPhoneticRepresentation(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero, out IntPtr exceptionPtr);
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void NSPersonNameComponents_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
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
