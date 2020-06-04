//
//  CKUserIdentityLookupInfo.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 05/28/2020
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
    /// <summary>
    /// Structured information used to lookup a User based on contact info
    /// </summary>
    public class CKUserIdentityLookupInfo : CKObject, IDisposable
    {
        #region dll

        // Class Methods
        

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr CKUserIdentityLookupInfo_initWithEmailAddress(
            string emailAddress, 
            out IntPtr exceptionPtr
            );
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr CKUserIdentityLookupInfo_initWithUserRecordID(
            IntPtr userRecordID, 
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr CKUserIdentityLookupInfo_GetPropEmailAddress(HandleRef ptr);

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr CKUserIdentityLookupInfo_GetPropPhoneNumber(HandleRef ptr);

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr CKUserIdentityLookupInfo_GetPropUserRecordID(HandleRef ptr);

        

        #endregion

        internal CKUserIdentityLookupInfo(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKUserIdentityLookupInfo(
            string emailAddress
            )
        {
            if(emailAddress == null)
                throw new ArgumentNullException(nameof(emailAddress));
            
            IntPtr ptr = CKUserIdentityLookupInfo_initWithEmailAddress(
                emailAddress, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKUserIdentityLookupInfo(
            CKRecordID userRecordID
            )
        {
            if(userRecordID == null)
                throw new ArgumentNullException(nameof(userRecordID));
            
            IntPtr ptr = CKUserIdentityLookupInfo_initWithUserRecordID(
                userRecordID != null ? HandleRef.ToIntPtr(userRecordID.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        
        /// <value>EmailAddress</value>
        public string EmailAddress
        {
            get 
            { 
                IntPtr emailAddress = CKUserIdentityLookupInfo_GetPropEmailAddress(Handle);
                return Marshal.PtrToStringAuto(emailAddress);
            }
        }

        
        /// <value>PhoneNumber</value>
        public string PhoneNumber
        {
            get 
            { 
                IntPtr phoneNumber = CKUserIdentityLookupInfo_GetPropPhoneNumber(Handle);
                return Marshal.PtrToStringAuto(phoneNumber);
            }
        }

        
        /// <value>UserRecordID</value>
        public CKRecordID UserRecordID
        {
            get 
            { 
                IntPtr userRecordID = CKUserIdentityLookupInfo_GetPropUserRecordID(Handle);
                return userRecordID == IntPtr.Zero ? null : new CKRecordID(userRecordID);
            }
        }

        

        

        
        #region IDisposable Support
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern void CKUserIdentityLookupInfo_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKUserIdentityLookupInfo Dispose");
                CKUserIdentityLookupInfo_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKUserIdentityLookupInfo()
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
