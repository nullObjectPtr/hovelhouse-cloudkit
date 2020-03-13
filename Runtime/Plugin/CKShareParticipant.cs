//
//  CKShareParticipant.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 03/13/2020
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
    public class CKShareParticipant : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        

        

        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern CKShareParticipantAcceptanceStatus CKShareParticipant_GetPropAcceptanceStatus(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern CKShareParticipantPermission CKShareParticipant_GetPropPermission(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKShareParticipant_SetPropPermission(HandleRef ptr, long permission, out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKShareParticipant_GetPropUserIdentity(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern CKShareParticipantRole CKShareParticipant_GetPropRole(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKShareParticipant_SetPropRole(HandleRef ptr, long role, out IntPtr exceptionPtr);
        

        #endregion

        internal CKShareParticipant(IntPtr ptr) : base(ptr) {}
        
        
        
        


        
        
        
        public CKShareParticipantAcceptanceStatus AcceptanceStatus 
        {
            get 
            { 
                CKShareParticipantAcceptanceStatus acceptanceStatus = CKShareParticipant_GetPropAcceptanceStatus(Handle);
                return acceptanceStatus;
            }
        }
        
        public CKShareParticipantPermission Permission 
        {
            get 
            { 
                CKShareParticipantPermission permission = CKShareParticipant_GetPropPermission(Handle);
                return permission;
            }
            set
            {
                CKShareParticipant_SetPropPermission(Handle, (long) value, out IntPtr exceptionPtr);
            }
        }
        
        public CKUserIdentity UserIdentity 
        {
            get 
            { 
                IntPtr userIdentity = CKShareParticipant_GetPropUserIdentity(Handle);
                return userIdentity == IntPtr.Zero ? null : new CKUserIdentity(userIdentity);
            }
        }
        
        public CKShareParticipantRole Role 
        {
            get 
            { 
                CKShareParticipantRole role = CKShareParticipant_GetPropRole(Handle);
                return role;
            }
            set
            {
                CKShareParticipant_SetPropRole(Handle, (long) value, out IntPtr exceptionPtr);
            }
        }
        

        

        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKShareParticipant_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKShareParticipant Dispose");
                CKShareParticipant_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKShareParticipant()
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
