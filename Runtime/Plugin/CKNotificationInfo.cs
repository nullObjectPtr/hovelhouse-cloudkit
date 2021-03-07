//
//  CKNotificationInfo.cs
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
    /// Specifies the type of notifications to generate when a condition is met
    /// </summary>
    public class CKNotificationInfo : CKObject, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        
        [DllImport(dll)]
        private static extern IntPtr CKNotificationInfo_init(
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern IntPtr CKNotificationInfo_GetPropCollapseIDKey(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKNotificationInfo_SetPropCollapseIDKey(HandleRef ptr, string collapseIDKey, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern bool CKNotificationInfo_GetPropShouldBadge(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKNotificationInfo_SetPropShouldBadge(HandleRef ptr, bool shouldBadge, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern bool CKNotificationInfo_GetPropShouldSendContentAvailable(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKNotificationInfo_SetPropShouldSendContentAvailable(HandleRef ptr, bool shouldSendContentAvailable, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern bool CKNotificationInfo_GetPropShouldSendMutableContent(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKNotificationInfo_SetPropShouldSendMutableContent(HandleRef ptr, bool shouldSendMutableContent, out IntPtr exceptionPtr);

        // TODO: DLLPROPERTYSTRINGARRAY

        

        #endregion

        internal CKNotificationInfo(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKNotificationInfo(
            )
        {
            
            IntPtr ptr = CKNotificationInfo_init(
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        
        /// <value>CollapseIDKey</value>
        public string CollapseIDKey
        {
            get 
            { 
                IntPtr collapseIDKey = CKNotificationInfo_GetPropCollapseIDKey(Handle);
                return Marshal.PtrToStringAuto(collapseIDKey);
            }
            set
            {
                CKNotificationInfo_SetPropCollapseIDKey(Handle, value, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>ShouldBadge</value>
        public bool ShouldBadge
        {
            get 
            { 
                bool shouldBadge = CKNotificationInfo_GetPropShouldBadge(Handle);
                return shouldBadge;
            }
            set
            {
                CKNotificationInfo_SetPropShouldBadge(Handle, value, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>ShouldSendContentAvailable</value>
        public bool ShouldSendContentAvailable
        {
            get 
            { 
                bool shouldSendContentAvailable = CKNotificationInfo_GetPropShouldSendContentAvailable(Handle);
                return shouldSendContentAvailable;
            }
            set
            {
                CKNotificationInfo_SetPropShouldSendContentAvailable(Handle, value, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>ShouldSendMutableContent</value>
        public bool ShouldSendMutableContent
        {
            get 
            { 
                bool shouldSendMutableContent = CKNotificationInfo_GetPropShouldSendMutableContent(Handle);
                return shouldSendMutableContent;
            }
            set
            {
                CKNotificationInfo_SetPropShouldSendMutableContent(Handle, value, out IntPtr exceptionPtr);
            }
        }

        
        // TODO: PROPERTYSTRINGARRAY
        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKNotificationInfo_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKNotificationInfo Dispose");
                CKNotificationInfo_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKNotificationInfo()
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
