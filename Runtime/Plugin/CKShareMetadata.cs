//
//  CKShareMetadata.cs
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
    /// Metadata for a shared record
    /// </summary>
    public class CKShareMetadata : CKObject, IDisposable
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
        private static extern IntPtr CKShareMetadata_GetPropContainerIdentifier(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr CKShareMetadata_GetPropOwnerIdentity(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern CKShareParticipantPermission CKShareMetadata_GetPropParticipantPermission(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern CKShareParticipantAcceptanceStatus CKShareMetadata_GetPropParticipantStatus(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr CKShareMetadata_GetPropRootRecord(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr CKShareMetadata_GetPropRootRecordID(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr CKShareMetadata_GetPropShare(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern CKShareParticipantRole CKShareMetadata_GetPropParticipantRole(HandleRef ptr);

        

        #endregion

        internal CKShareMetadata(IntPtr ptr) : base(ptr) {}
        
        
        
        


        
        
        
        /// <value>ContainerIdentifier</value>
        public string ContainerIdentifier
        {
            get 
            { 
                IntPtr containerIdentifier = CKShareMetadata_GetPropContainerIdentifier(Handle);
                return Marshal.PtrToStringAuto(containerIdentifier);
            }
        }

        
        /// <value>OwnerIdentity</value>
        public CKUserIdentity OwnerIdentity
        {
            get 
            { 
                IntPtr ownerIdentity = CKShareMetadata_GetPropOwnerIdentity(Handle);
                return ownerIdentity == IntPtr.Zero ? null : new CKUserIdentity(ownerIdentity);
            }
        }

        
        /// <value>ParticipantPermission</value>
        public CKShareParticipantPermission ParticipantPermission
        {
            get 
            { 
                CKShareParticipantPermission participantPermission = CKShareMetadata_GetPropParticipantPermission(Handle);
                return participantPermission;
            }
        }

        
        /// <value>ParticipantStatus</value>
        public CKShareParticipantAcceptanceStatus ParticipantStatus
        {
            get 
            { 
                CKShareParticipantAcceptanceStatus participantStatus = CKShareMetadata_GetPropParticipantStatus(Handle);
                return participantStatus;
            }
        }

        
        /// <value>RootRecord</value>
        public CKRecord RootRecord
        {
            get 
            { 
                IntPtr rootRecord = CKShareMetadata_GetPropRootRecord(Handle);
                return rootRecord == IntPtr.Zero ? null : new CKRecord(rootRecord);
            }
        }

        
        /// <value>RootRecordID</value>
        public CKRecordID RootRecordID
        {
            get 
            { 
                IntPtr rootRecordID = CKShareMetadata_GetPropRootRecordID(Handle);
                return rootRecordID == IntPtr.Zero ? null : new CKRecordID(rootRecordID);
            }
        }

        
        /// <value>Share</value>
        public CKShare Share
        {
            get 
            { 
                IntPtr share = CKShareMetadata_GetPropShare(Handle);
                return share == IntPtr.Zero ? null : new CKShare(share);
            }
        }

        
        /// <value>ParticipantRole</value>
        public CKShareParticipantRole ParticipantRole
        {
            get 
            { 
                CKShareParticipantRole participantRole = CKShareMetadata_GetPropParticipantRole(Handle);
                return participantRole;
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKShareMetadata_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
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
