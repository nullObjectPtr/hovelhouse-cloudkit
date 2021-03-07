//
//  NSError.cs
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
    /// An error object
    /// </summary>
    /// <remarks>
    /// Unlike C# apple makes a big distinction between Error&apos;s and Exceptions. All CKOperations will include an error as a parameter in it&apos;s handlers to indicate the failure of the operation. Use the code field provided and compare them against the various error enums to determine the type of error that happened.
    /// </remarks>
    public class NSError : CKObject, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        

        
        [DllImport(dll)]
        private static extern IntPtr NSError_stringForUserInfoKey(
            HandleRef ptr, 
            string key,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern long NSError_intForUserInfoKey(
            HandleRef ptr, 
            string key,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern float NSError_floatForUserInfoKey(
            HandleRef ptr, 
            string key,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr NSError_recordForUserInfoKey(
            HandleRef ptr, 
            string key,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr NSError_errorForUserInfoKey(
            HandleRef ptr, 
            string key,
            out IntPtr exceptionPtr);
        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr NSError_partialErrorForItemId(
            HandleRef ptr, 
            HandleRef itemIdPtr,
            out IntPtr exceptionPtr);

        
        #if UNITY_IPHONE || UNITY_TVOS
        [DllImport("__Internal")]
        #else
        [DllImport("HHCloudKitMacOS")]
        #endif
        private static extern IntPtr NSError_userInfoAsString(
            HandleRef ptr, 
            out IntPtr exceptionPtr);

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern long NSError_GetPropCode(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr NSError_GetPropLocalizedDescription(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr NSError_GetPropLocalizedRecoverySuggestion(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr NSError_GetPropLocalizedFailureReason(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern IntPtr NSError_GetPropHelpAnchor(HandleRef ptr);

        // TODO: DLLPROPERTYSTRINGARRAY

        
        [DllImport(dll)]
        private static extern IntPtr NSError_GetPropDomain(HandleRef ptr);

        

        #endregion

        internal NSError(IntPtr ptr) : base(ptr) {}
        
        
        
        


        
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns>val</returns>
        public string StringForUserInfoKey(
            string key)
        { 
            
            var val = NSError_stringForUserInfoKey(
                Handle, 
                key, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return Marshal.PtrToStringAuto(val);
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns>val</returns>
        public long IntForUserInfoKey(
            string key)
        { 
            
            var val = NSError_intForUserInfoKey(
                Handle, 
                key, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val;
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns>val</returns>
        public float FloatForUserInfoKey(
            string key)
        { 
            
            var val = NSError_floatForUserInfoKey(
                Handle, 
                key, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val;
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns>val</returns>
        public CKRecord RecordForUserInfoKey(
            string key)
        { 
            
            var val = NSError_recordForUserInfoKey(
                Handle, 
                key, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val == IntPtr.Zero ? null : new CKRecord(val);
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns>val</returns>
        public NSError ErrorForUserInfoKey(
            string key)
        { 
            
            var val = NSError_errorForUserInfoKey(
                Handle, 
                key, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val == IntPtr.Zero ? null : new NSError(val);
        }
        
        // See:
        // https://developer.apple.com/documentation/cloudkit/ckpartialerrorsbyitemidkey?language=objc
        // for how this works
        public NSError PartialErrorForItemId(
            CKObject itemId)
        {
            var val = NSError_partialErrorForItemId(
                Handle, 
                itemId.Handle,
                out var exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val == IntPtr.Zero ? null : new NSError(val);
        }
        
        

        
        /// <summary>
        /// </summary>
        /// 
        /// <returns>val</returns>
        public string UserInfoAsString()
        { 
            var val = NSError_userInfoAsString(
                Handle, out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return Marshal.PtrToStringAuto(val);
        }
        

        
        
        
        /// <value>Code</value>
        public long Code
        {
            get 
            { 
                long code = NSError_GetPropCode(Handle);
                return code;
            }
        }

        
        /// <value>LocalizedDescription</value>
        public string LocalizedDescription
        {
            get 
            { 
                IntPtr localizedDescription = NSError_GetPropLocalizedDescription(Handle);
                return Marshal.PtrToStringAuto(localizedDescription);
            }
        }

        
        /// <value>LocalizedRecoverySuggestion</value>
        public string LocalizedRecoverySuggestion
        {
            get 
            { 
                IntPtr localizedRecoverySuggestion = NSError_GetPropLocalizedRecoverySuggestion(Handle);
                return Marshal.PtrToStringAuto(localizedRecoverySuggestion);
            }
        }

        
        /// <value>LocalizedFailureReason</value>
        public string LocalizedFailureReason
        {
            get 
            { 
                IntPtr localizedFailureReason = NSError_GetPropLocalizedFailureReason(Handle);
                return Marshal.PtrToStringAuto(localizedFailureReason);
            }
        }

        
        /// <value>HelpAnchor</value>
        public string HelpAnchor
        {
            get 
            { 
                IntPtr helpAnchor = NSError_GetPropHelpAnchor(Handle);
                return Marshal.PtrToStringAuto(helpAnchor);
            }
        }

        
        // TODO: PROPERTYSTRINGARRAY
        
        /// <value>Domain</value>
        public string Domain
        {
            get 
            { 
                IntPtr domain = NSError_GetPropDomain(Handle);
                return Marshal.PtrToStringAuto(domain);
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void NSError_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("NSError Dispose");
                NSError_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~NSError()
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
