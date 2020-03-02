//
//  CKUserIdentityLookupInfo.cs
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
    public class CKUserIdentityLookupInfo : CKObject
    {
        #region dll

        // Class Methods
        

        // Constructors
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKUserIdentityLookupInfo_initWithEmailAddress(
            string emailAddress);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKUserIdentityLookupInfo_initWithPhoneNumber(
            string phoneNumber);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKUserIdentityLookupInfo_initWithUserRecordID(
            IntPtr userRecordID);
        

        // Instance Methods
        

        

        // Properties
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKUserIdentityLookupInfo_GetPropEmailAddress(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKUserIdentityLookupInfo_GetPropPhoneNumber(HandleRef ptr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKit")]
        #endif
        private static extern IntPtr CKUserIdentityLookupInfo_GetPropUserRecordID(HandleRef ptr);
        
        #endregion

        internal CKUserIdentityLookupInfo(IntPtr ptr) : base(ptr) {}
        
        #region Class Methods
        
        #endregion

        #region Constructors
        
        public static CKUserIdentityLookupInfo initWithEmailAddress(
            string emailAddress
        ){
            if(emailAddress == null)
                throw new ArgumentNullException(nameof(emailAddress));
            
            IntPtr ptr = CKUserIdentityLookupInfo_initWithEmailAddress(
                emailAddress);
            return new CKUserIdentityLookupInfo(ptr);
        }
        
        
        public static CKUserIdentityLookupInfo initWithPhoneNumber(
            string phoneNumber
        ){
            if(phoneNumber == null)
                throw new ArgumentNullException(nameof(phoneNumber));
            
            IntPtr ptr = CKUserIdentityLookupInfo_initWithPhoneNumber(
                phoneNumber);
            return new CKUserIdentityLookupInfo(ptr);
        }
        
        
        public static CKUserIdentityLookupInfo initWithUserRecordID(
            CKRecordID userRecordID
        ){
            if(userRecordID == null)
                throw new ArgumentNullException(nameof(userRecordID));
            
            IntPtr ptr = CKUserIdentityLookupInfo_initWithUserRecordID(
                userRecordID != null ? HandleRef.ToIntPtr(userRecordID.Handle) : IntPtr.Zero);
            return new CKUserIdentityLookupInfo(ptr);
        }
        
        
        #endregion


        #region Methods
        
        
        #endregion

        #region Properties
        
        public string EmailAddress 
        {
            get 
            { 
                IntPtr emailAddress = CKUserIdentityLookupInfo_GetPropEmailAddress(Handle);
                return Marshal.PtrToStringAuto(emailAddress);
            }
        }
        
        public string PhoneNumber 
        {
            get 
            { 
                IntPtr phoneNumber = CKUserIdentityLookupInfo_GetPropPhoneNumber(Handle);
                return Marshal.PtrToStringAuto(phoneNumber);
            }
        }
        
        public CKRecordID UserRecordID 
        {
            get 
            { 
                IntPtr userRecordID = CKUserIdentityLookupInfo_GetPropUserRecordID(Handle);
                return userRecordID == IntPtr.Zero ? null : new CKRecordID(userRecordID);
            }
        }
        
        #endregion
    }
}
