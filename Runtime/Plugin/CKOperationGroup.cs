//
//  CKOperationGroup.cs
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
    /// Use this to batch multiple operations in a single request
    /// </summary>
    public class CKOperationGroup : CKObject, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        
        [DllImport(dll)]
        private static extern IntPtr CKOperationGroup_init(
            out IntPtr exceptionPtr
            );
        
        [DllImport(dll)]
        private static extern IntPtr CKOperationGroup_initWithCoder(
            IntPtr aDecoder, 
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern IntPtr CKOperationGroup_GetPropDefaultConfiguration(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKOperationGroup_SetPropDefaultConfiguration(HandleRef ptr, IntPtr defaultConfiguration, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern CKOperationGroupTransferSize CKOperationGroup_GetPropExpectedReceiveSize(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKOperationGroup_SetPropExpectedReceiveSize(HandleRef ptr, long expectedReceiveSize, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern CKOperationGroupTransferSize CKOperationGroup_GetPropExpectedSendSize(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKOperationGroup_SetPropExpectedSendSize(HandleRef ptr, long expectedSendSize, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr CKOperationGroup_GetPropName(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKOperationGroup_SetPropName(HandleRef ptr, string name, out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr CKOperationGroup_GetPropOperationGroupID(HandleRef ptr);

        
        [DllImport(dll)]
        private static extern ulong CKOperationGroup_GetPropQuantity(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKOperationGroup_SetPropQuantity(HandleRef ptr, ulong quantity, out IntPtr exceptionPtr);

        

        #endregion

        internal CKOperationGroup(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKOperationGroup(
            )
        {
            
            IntPtr ptr = CKOperationGroup_init(
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKOperationGroup(
            NSCoder aDecoder
            )
        {
            if(aDecoder == null)
                throw new ArgumentNullException(nameof(aDecoder));
            
            IntPtr ptr = CKOperationGroup_initWithCoder(
                aDecoder != null ? HandleRef.ToIntPtr(aDecoder.Handle) : IntPtr.Zero, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        
        /// <value>DefaultConfiguration</value>
        public CKOperationConfiguration DefaultConfiguration
        {
            get 
            { 
                IntPtr defaultConfiguration = CKOperationGroup_GetPropDefaultConfiguration(Handle);
                return defaultConfiguration == IntPtr.Zero ? null : new CKOperationConfiguration(defaultConfiguration);
            }
            set
            {
                CKOperationGroup_SetPropDefaultConfiguration(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>ExpectedReceiveSize</value>
        public CKOperationGroupTransferSize ExpectedReceiveSize
        {
            get 
            { 
                CKOperationGroupTransferSize expectedReceiveSize = CKOperationGroup_GetPropExpectedReceiveSize(Handle);
                return expectedReceiveSize;
            }
            set
            {
                CKOperationGroup_SetPropExpectedReceiveSize(Handle, (long) value, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>ExpectedSendSize</value>
        public CKOperationGroupTransferSize ExpectedSendSize
        {
            get 
            { 
                CKOperationGroupTransferSize expectedSendSize = CKOperationGroup_GetPropExpectedSendSize(Handle);
                return expectedSendSize;
            }
            set
            {
                CKOperationGroup_SetPropExpectedSendSize(Handle, (long) value, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>Name</value>
        public string Name
        {
            get 
            { 
                IntPtr name = CKOperationGroup_GetPropName(Handle);
                return Marshal.PtrToStringAuto(name);
            }
            set
            {
                CKOperationGroup_SetPropName(Handle, value, out IntPtr exceptionPtr);
            }
        }

        
        /// <value>OperationGroupID</value>
        public string OperationGroupID
        {
            get 
            { 
                IntPtr operationGroupID = CKOperationGroup_GetPropOperationGroupID(Handle);
                return Marshal.PtrToStringAuto(operationGroupID);
            }
        }

        
        /// <value>Quantity</value>
        public ulong Quantity
        {
            get 
            { 
                ulong quantity = CKOperationGroup_GetPropQuantity(Handle);
                return quantity;
            }
            set
            {
                CKOperationGroup_SetPropQuantity(Handle, value, out IntPtr exceptionPtr);
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKOperationGroup_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKOperationGroup Dispose");
                CKOperationGroup_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKOperationGroup()
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
