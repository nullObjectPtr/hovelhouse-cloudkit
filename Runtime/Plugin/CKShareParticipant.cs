//
//  CKShareParticipant.cs
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
    /// A user that is participating in a shared record
    /// </summary>
    public class CKShareParticipant : CKObject, IDisposable
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
        private static extern CKShareParticipantAcceptanceStatus CKShareParticipant_GetPropAcceptanceStatus(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern CKShareParticipantPermission CKShareParticipant_GetPropPermission(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKShareParticipant_SetPropPermission(HandleRef ptr, long permission, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr CKShareParticipant_GetPropUserIdentity(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern CKShareParticipantRole CKShareParticipant_GetPropRole(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKShareParticipant_SetPropRole(HandleRef ptr, long role, out IntPtr exceptionPtr);

        

        #endregion

        internal CKShareParticipant(IntPtr ptr) : base(ptr) {}
        
        
        
        


        
        
        
        /// <value>AcceptanceStatus</value>
        public CKShareParticipantAcceptanceStatus AcceptanceStatus
        {
            get 
            { 
                CKShareParticipantAcceptanceStatus acceptanceStatus = CKShareParticipant_GetPropAcceptanceStatus(Handle);
                return acceptanceStatus;
            }
        }

        
        /// <value>Permission</value>
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

        
        /// <value>UserIdentity</value>
        public CKUserIdentity UserIdentity
        {
            get 
            { 
                IntPtr userIdentity = CKShareParticipant_GetPropUserIdentity(Handle);
                return userIdentity == IntPtr.Zero ? null : new CKUserIdentity(userIdentity);
            }
        }

        
        /// <value>Role</value>
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
        [DllImport(dll)]
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
