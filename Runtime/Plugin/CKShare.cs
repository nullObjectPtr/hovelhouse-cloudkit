//
//  CKShare.cs
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
    /// A shared record
    /// </summary>
    public class CKShare : CKRecord, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        
        [DllImport(dll)]
        private static extern IntPtr CKShare_initWithCoder(
            IntPtr aDecoder, 
            out IntPtr exceptionPtr
            );
        
        [DllImport(dll)]
        private static extern IntPtr CKShare_initWithRootRecord(
            IntPtr rootRecord, 
            out IntPtr exceptionPtr
            );
        
        [DllImport(dll)]
        private static extern IntPtr CKShare_initWithRootRecord_shareID(
            IntPtr rootRecord, 
            IntPtr shareID, 
            out IntPtr exceptionPtr
            );
        

        
        [DllImport(dll)]
        private static extern void CKShare_addParticipant(
            HandleRef ptr, 
            IntPtr participant,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern void CKShare_removeParticipant(
            HandleRef ptr, 
            IntPtr participant,
            out IntPtr exceptionPtr);

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern CKShareParticipantPermission CKShare_GetPropPublicPermission(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKShare_SetPropPublicPermission(HandleRef ptr, long publicPermission, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr CKShare_GetPropURL(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr CKShare_GetPropCurrentUserParticipant(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr CKShare_GetPropOwner(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern void CKShare_GetPropParticipants(HandleRef ptr, ref IntPtr buffer, ref long count);

        

        #endregion

        internal CKShare(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKShare(
            NSCoder aDecoder
            )
        {
            
            IntPtr ptr = CKShare_initWithCoder(
                aDecoder != null ? HandleRef.ToIntPtr(aDecoder.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKShare(
            CKRecord rootRecord
            )
        {
            
            IntPtr ptr = CKShare_initWithRootRecord(
                rootRecord != null ? HandleRef.ToIntPtr(rootRecord.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKShare(
            CKRecord rootRecord, 
            CKRecordID shareID
            )
        {
            
            IntPtr ptr = CKShare_initWithRootRecord_shareID(
                rootRecord != null ? HandleRef.ToIntPtr(rootRecord.Handle) : IntPtr.Zero, 
                shareID != null ? HandleRef.ToIntPtr(shareID.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        /// <summary>
        /// </summary>
        /// <param name="participant"></param>
        /// <returns>void</returns>
        public void AddParticipant(
            CKShareParticipant participant)
        { 
            
            CKShare_addParticipant(
                Handle, 
                participant != null ? HandleRef.ToIntPtr(participant.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="participant"></param>
        /// <returns>void</returns>
        public void RemoveParticipant(
            CKShareParticipant participant)
        { 
            
            CKShare_removeParticipant(
                Handle, 
                participant != null ? HandleRef.ToIntPtr(participant.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        

        
        
        
        /// <value>PublicPermission</value>
        public CKShareParticipantPermission PublicPermission
        {
            get 
            { 
                CKShareParticipantPermission publicPermission = CKShare_GetPropPublicPermission(Handle);
                return publicPermission;
            }
            set
            {
                CKShare_SetPropPublicPermission(Handle, (long) value, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>URL</value>
        public NSURL URL
        {
            get 
            { 
                IntPtr URL = CKShare_GetPropURL(Handle);
                return URL == IntPtr.Zero ? null : new NSURL(URL);
            }
        }

        
        /// <value>CurrentUserParticipant</value>
        public CKShareParticipant CurrentUserParticipant
        {
            get 
            { 
                IntPtr currentUserParticipant = CKShare_GetPropCurrentUserParticipant(Handle);
                return currentUserParticipant == IntPtr.Zero ? null : new CKShareParticipant(currentUserParticipant);
            }
        }

        
        /// <value>Owner</value>
        public CKShareParticipant Owner
        {
            get 
            { 
                IntPtr owner = CKShare_GetPropOwner(Handle);
                return owner == IntPtr.Zero ? null : new CKShareParticipant(owner);
            }
        }

        
        /// <value>Participants</value>
        public CKShareParticipant[] Participants
        {
            get 
            { 
                IntPtr bufferPtr = IntPtr.Zero;
                long bufferLen = 0;

                CKShare_GetPropParticipants(Handle, ref bufferPtr, ref bufferLen);

                var participants = new CKShareParticipant[bufferLen];

                for (int i = 0; i < bufferLen; i++)
                {
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * IntPtr.Size));
                    participants[i] = ptr2 == IntPtr.Zero ? null : new CKShareParticipant(ptr2);
                }

                Marshal.FreeHGlobal(bufferPtr);

                return participants;
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKShare_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKShare Dispose");
                CKShare_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKShare()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public new void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
        
    }
}
