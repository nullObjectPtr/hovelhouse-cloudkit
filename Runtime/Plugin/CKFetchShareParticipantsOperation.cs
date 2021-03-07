//
//  CKFetchShareParticipantsOperation.cs
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
    /// A container operation that fetches the participants for shared records
    /// </summary>
    public class CKFetchShareParticipantsOperation : CKOperation, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        
        [DllImport(dll)]
        private static extern IntPtr CKFetchShareParticipantsOperation_init(
            out IntPtr exceptionPtr
            );
        
        [DllImport(dll)]
        private static extern IntPtr CKFetchShareParticipantsOperation_initWithUserIdentityLookupInfos(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 2)]
            IntPtr[] userIdentityLookupInfos,
			int userIdentityLookupInfosCount, 
            out IntPtr exceptionPtr
            );
        

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern void CKFetchShareParticipantsOperation_GetPropUserIdentityLookupInfos(HandleRef ptr, ref IntPtr buffer, ref long count);
        
        [DllImport(dll)]
        private static extern void CKFetchShareParticipantsOperation_SetPropUserIdentityLookupInfos(HandleRef ptr, IntPtr[] userIdentityLookupInfos,
			int userIdentityLookupInfosCount, out IntPtr exceptionPtr);

        [DllImport(dll)]
        private static extern void CKFetchShareParticipantsOperation_SetPropFetchShareParticipantsCompletionHandler(HandleRef ptr, FetchShareParticipantsCompletionDelegate fetchShareParticipantsCompletionHandler, out IntPtr exceptionPtr);

        [DllImport(dll)]
        private static extern void CKFetchShareParticipantsOperation_SetPropShareParticipantFetchedHandler(HandleRef ptr, ShareParticipantFetchedDelegate shareParticipantFetchedHandler, out IntPtr exceptionPtr);

        

        #endregion

        internal CKFetchShareParticipantsOperation(IntPtr ptr) : base(ptr) {}
        
        
        
        
        public CKFetchShareParticipantsOperation(
            )
        {
            
            IntPtr ptr = CKFetchShareParticipantsOperation_init(
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            Handle = new HandleRef(this,ptr);
        }
        
        
        public CKFetchShareParticipantsOperation(
            CKUserIdentityLookupInfo[] userIdentityLookupInfos
            )
        {
            if(userIdentityLookupInfos == null)
                throw new ArgumentNullException(nameof(userIdentityLookupInfos));
            
            IntPtr ptr = CKFetchShareParticipantsOperation_initWithUserIdentityLookupInfos(
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

                CKFetchShareParticipantsOperation_GetPropUserIdentityLookupInfos(Handle, ref bufferPtr, ref bufferLen);

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
                CKFetchShareParticipantsOperation_SetPropUserIdentityLookupInfos(Handle, value == null ? null : value.Select(x => HandleRef.ToIntPtr(x.Handle)).ToArray(),
				value == null ? 0 : value.Length, out IntPtr exceptionPtr);
                
                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        
        /// <value>FetchShareParticipantsCompletionHandler</value>
        public Action<NSError> FetchShareParticipantsCompletionHandler
        {
            get 
            {
                FetchShareParticipantsCompletionHandlerCallbacks.TryGetValue(
                    HandleRef.ToIntPtr(Handle), 
                    out ExecutionContext<NSError> value);
                return value.Callback;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    FetchShareParticipantsCompletionHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    FetchShareParticipantsCompletionHandlerCallbacks[myPtr] = new ExecutionContext<NSError>(value);
                }
                CKFetchShareParticipantsOperation_SetPropFetchShareParticipantsCompletionHandler(Handle, FetchShareParticipantsCompletionHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,ExecutionContext<NSError>> FetchShareParticipantsCompletionHandlerCallbacks = new Dictionary<IntPtr,ExecutionContext<NSError>>();

        [MonoPInvokeCallback(typeof(FetchShareParticipantsCompletionDelegate))]
        private static void FetchShareParticipantsCompletionHandlerCallback(IntPtr thisPtr, IntPtr _operationError)
        {
            if(FetchShareParticipantsCompletionHandlerCallbacks.TryGetValue(thisPtr, out ExecutionContext<NSError> callback))
            {
                callback.Invoke(
                        _operationError == IntPtr.Zero ? null : new NSError(_operationError));
            }
        }

        
        /// <value>ShareParticipantFetchedHandler</value>
        public Action<CKShareParticipant> ShareParticipantFetchedHandler
        {
            get 
            {
                ShareParticipantFetchedHandlerCallbacks.TryGetValue(
                    HandleRef.ToIntPtr(Handle), 
                    out ExecutionContext<CKShareParticipant> value);
                return value.Callback;
            }    
            set 
            {
                IntPtr myPtr = HandleRef.ToIntPtr(Handle);
                if(value == null)
                {
                    ShareParticipantFetchedHandlerCallbacks.Remove(myPtr);
                }
                else
                {
                    ShareParticipantFetchedHandlerCallbacks[myPtr] = new ExecutionContext<CKShareParticipant>(value);
                }
                CKFetchShareParticipantsOperation_SetPropShareParticipantFetchedHandler(Handle, ShareParticipantFetchedHandlerCallback, out IntPtr exceptionPtr);

                if(exceptionPtr != IntPtr.Zero)
                {
                    var nativeException = new NSException(exceptionPtr);
                    throw new CloudKitException(nativeException, nativeException.Reason);
                }
            }
        }

        private static readonly Dictionary<IntPtr,ExecutionContext<CKShareParticipant>> ShareParticipantFetchedHandlerCallbacks = new Dictionary<IntPtr,ExecutionContext<CKShareParticipant>>();

        [MonoPInvokeCallback(typeof(ShareParticipantFetchedDelegate))]
        private static void ShareParticipantFetchedHandlerCallback(IntPtr thisPtr, IntPtr _participant)
        {
            if(ShareParticipantFetchedHandlerCallbacks.TryGetValue(thisPtr, out ExecutionContext<CKShareParticipant> callback))
            {
                callback.Invoke(
                        _participant == IntPtr.Zero ? null : new CKShareParticipant(_participant));
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKFetchShareParticipantsOperation_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKFetchShareParticipantsOperation Dispose");
                CKFetchShareParticipantsOperation_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKFetchShareParticipantsOperation()
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
