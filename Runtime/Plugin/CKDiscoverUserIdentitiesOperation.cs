//
//  CKDiscoverUserIdentitiesOperation.cs
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
    /// An operation that discovers users based on the provided contact info
    /// </summary>
    public class CKDiscoverUserIdentitiesOperation : CKOperation, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        
        [DllImport(dll)]
        private static extern IntPtr CKDiscoverUserIdentitiesOperation_init(
            out IntPtr exceptionPtr
            );
        
        [DllImport(dll)]
        private static extern IntPtr CKDiscoverUserIdentitiesOperation_initWithUserIdentityLookupInfos(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 2)]
            IntPtr[] userIdentityLookupInfos,
			int userIdentityLookupInfosCount, 
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern void CKDiscoverUserIdentitiesOperation_GetPropUserIdentityLookupInfos(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        [DllImport(dll)]
        private static extern void CKDiscoverUserIdentitiesOperation_SetPropUserIdentityLookupInfos(HandleRef ptr, IntPtr[] userIdentityLookupInfos,
			int userIdentityLookupInfosCount, out IntPtr exceptionPtr);

        [DllImport(dll)]
        private static extern void CKDiscoverUserIdentitiesOperation_SetPropUserIdentityDiscoveredHandler(HandleRef ptr, UserIdentityDiscoveredDelegate userIdentityDiscoveredHandler, out IntPtr exceptionPtr);

        [DllImport(dll)]
        private static extern void CKDiscoverUserIdentitiesOperation_SetPropDiscoverUserIdentitiesCompletionHandler(HandleRef ptr, DiscoverUserIdentitiesCompletionDelegate discoverUserIdentitiesCompletionHandler, out IntPtr exceptionPtr);

        

        #endregion

        internal CKDiscoverUserIdentitiesOperation(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKDiscoverUserIdentitiesOperation(
            )
        {
            
            IntPtr ptr = CKDiscoverUserIdentitiesOperation_init(
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKDiscoverUserIdentitiesOperation(
            CKUserIdentityLookupInfo[] userIdentityLookupInfos
            )
        {
            if(userIdentityLookupInfos == null)
                throw new ArgumentNullException(nameof(userIdentityLookupInfos));
            
            IntPtr ptr = CKDiscoverUserIdentitiesOperation_initWithUserIdentityLookupInfos(
                userIdentityLookupInfos == null ? null : userIdentityLookupInfos.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				userIdentityLookupInfos == null ? 0 : userIdentityLookupInfos.Length, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        


        
        
        
        /// <value>UserIdentityLookupInfos</value>
        public CKUserIdentityLookupInfo[] UserIdentityLookupInfos
        {
            get 
            { 
                IntPtr bufferPtr = IntPtr.Zero;
                long bufferLen = 0;

                CKDiscoverUserIdentitiesOperation_GetPropUserIdentityLookupInfos(Handle, ref bufferPtr, ref bufferLen);

                var userIdentityLookupInfos = new CKUserIdentityLookupInfo[bufferLen];

                for (int i = 0; i < bufferLen; i++)
                {
                    IntPtr ptr2 = Marshal.ReadIntPtr(bufferPtr + (i * IntPtr.Size));
                    userIdentityLookupInfos[i] = ptr2 == IntPtr.Zero ? null : new CKUserIdentityLookupInfo(ptr2);
                }

                Marshal.FreeHGlobal(bufferPtr);

                return userIdentityLookupInfos;
            }
            set
            {
                CKDiscoverUserIdentitiesOperation_SetPropUserIdentityLookupInfos(Handle, value == null ? null : value.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				value == null ? 0 : value.Length, out IntPtr exceptionPtr);
                
                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        
        /// <value>UserIdentityDiscoveredHandler</value>
        public Action<CKUserIdentity,CKUserIdentityLookupInfo> UserIdentityDiscoveredHandler
        {
            get 
            {
                UserIdentityDiscoveredHandlerCallbacks.TryGetValue(
                    HandleRef.ToIntPtr(Handle), 
                    out ExecutionContext<CKUserIdentity,CKUserIdentityLookupInfo> value);
                return value.Callback;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    UserIdentityDiscoveredHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    UserIdentityDiscoveredHandlerCallbacks[myPtr] = new ExecutionContext<CKUserIdentity,CKUserIdentityLookupInfo>(value);
                }
                CKDiscoverUserIdentitiesOperation_SetPropUserIdentityDiscoveredHandler(Handle, UserIdentityDiscoveredHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,ExecutionContext<CKUserIdentity,CKUserIdentityLookupInfo>> UserIdentityDiscoveredHandlerCallbacks = new Dictionary<IntPtr,ExecutionContext<CKUserIdentity,CKUserIdentityLookupInfo>>();

        [MonoPInvokeCallback(typeof(UserIdentityDiscoveredDelegate))]
        private static void UserIdentityDiscoveredHandlerCallback(IntPtr thisPtr, IntPtr _identity, IntPtr _lookupInfo)
        {
            if(UserIdentityDiscoveredHandlerCallbacks.TryGetValue(thisPtr, out ExecutionContext<CKUserIdentity,CKUserIdentityLookupInfo> callback))
            {
                callback.Invoke(
                        _identity == IntPtr.Zero ? null : new CKUserIdentity(_identity),
                        _lookupInfo == IntPtr.Zero ? null : new CKUserIdentityLookupInfo(_lookupInfo));
            }
        }

        
        /// <value>DiscoverUserIdentitiesCompletionHandler</value>
        public Action<NSError> DiscoverUserIdentitiesCompletionHandler
        {
            get 
            {
                DiscoverUserIdentitiesCompletionHandlerCallbacks.TryGetValue(
                    HandleRef.ToIntPtr(Handle), 
                    out ExecutionContext<NSError> value);
                return value.Callback;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    DiscoverUserIdentitiesCompletionHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    DiscoverUserIdentitiesCompletionHandlerCallbacks[myPtr] = new ExecutionContext<NSError>(value);
                }
                CKDiscoverUserIdentitiesOperation_SetPropDiscoverUserIdentitiesCompletionHandler(Handle, DiscoverUserIdentitiesCompletionHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,ExecutionContext<NSError>> DiscoverUserIdentitiesCompletionHandlerCallbacks = new Dictionary<IntPtr,ExecutionContext<NSError>>();

        [MonoPInvokeCallback(typeof(DiscoverUserIdentitiesCompletionDelegate))]
        private static void DiscoverUserIdentitiesCompletionHandlerCallback(IntPtr thisPtr, IntPtr _operationError)
        {
            if(DiscoverUserIdentitiesCompletionHandlerCallbacks.TryGetValue(thisPtr, out ExecutionContext<NSError> callback))
            {
                callback.Invoke(
                        _operationError == IntPtr.Zero ? null : new NSError(_operationError));
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKDiscoverUserIdentitiesOperation_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKDiscoverUserIdentitiesOperation Dispose");
                CKDiscoverUserIdentitiesOperation_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKDiscoverUserIdentitiesOperation()
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
