//
//  CKShareMetadata.cs
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
    public class CKShareMetadata : CKObject, IDisposable
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
        private static extern IntPtr CKShareMetadata_GetPropContainerIdentifier(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKShareMetadata_GetPropOwnerIdentity(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern CKShareParticipantPermission CKShareMetadata_GetPropParticipantPermission(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern CKShareParticipantAcceptanceStatus CKShareMetadata_GetPropParticipantStatus(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKShareMetadata_GetPropRootRecord(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKShareMetadata_GetPropRootRecordID(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKShareMetadata_GetPropShare(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern CKShareParticipantRole CKShareMetadata_GetPropParticipantRole(HandleRef ptr);
        
        #endregion

        internal CKShareMetadata(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public string ContainerIdentifier 
        {
            get 
            { 
                IntPtr containerIdentifier = CKShareMetadata_GetPropContainerIdentifier(Handle);
                return Marshal.PtrToStringAuto(containerIdentifier);
            }
        }
        
        public CKUserIdentity OwnerIdentity 
        {
            get 
            { 
                IntPtr ownerIdentity = CKShareMetadata_GetPropOwnerIdentity(Handle);
                return ownerIdentity == IntPtr.Zero ? null : new CKUserIdentity(ownerIdentity);
            }
        }
        
        public CKShareParticipantPermission ParticipantPermission 
        {
            get 
            { 
                CKShareParticipantPermission participantPermission = CKShareMetadata_GetPropParticipantPermission(Handle);
                return participantPermission;
            }
        }
        
        public CKShareParticipantAcceptanceStatus ParticipantStatus 
        {
            get 
            { 
                CKShareParticipantAcceptanceStatus participantStatus = CKShareMetadata_GetPropParticipantStatus(Handle);
                return participantStatus;
            }
        }
        
        public CKRecord RootRecord 
        {
            get 
            { 
                IntPtr rootRecord = CKShareMetadata_GetPropRootRecord(Handle);
                return rootRecord == IntPtr.Zero ? null : new CKRecord(rootRecord);
            }
        }
        
        public CKRecordID RootRecordID 
        {
            get 
            { 
                IntPtr rootRecordID = CKShareMetadata_GetPropRootRecordID(Handle);
                return rootRecordID == IntPtr.Zero ? null : new CKRecordID(rootRecordID);
            }
        }
        
        public CKShare Share 
        {
            get 
            { 
                IntPtr share = CKShareMetadata_GetPropShare(Handle);
                return share == IntPtr.Zero ? null : new CKShare(share);
            }
        }
        
        public CKShareParticipantRole ParticipantRole 
        {
            get 
            { 
                CKShareParticipantRole participantRole = CKShareMetadata_GetPropParticipantRole(Handle);
                return participantRole;
            }
        }
        
        #endregion
        
        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern void CKShareMetadata_Dispose(HandleRef handle);
            
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
                
                //Debug.Log("CKShareMetadata Dispose");
                CKShareMetadata_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKShareMetadata()
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