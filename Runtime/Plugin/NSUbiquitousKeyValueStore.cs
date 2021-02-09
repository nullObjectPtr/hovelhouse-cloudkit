//
//  NSUbiquitousKeyValueStore.cs
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
    /// Simple key-value storage that is synced automatically to iCloud
    /// </summary>
    public class NSUbiquitousKeyValueStore : CKObject, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        

        
        [DllImport(dll)]
        private static extern bool NSUbiquitousKeyValueStore_boolForKey(
            HandleRef ptr, 
            string aKey,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern long NSUbiquitousKeyValueStore_longLongForKey(
            HandleRef ptr, 
            string aKey,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern double NSUbiquitousKeyValueStore_doubleForKey(
            HandleRef ptr, 
            string aKey,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr NSUbiquitousKeyValueStore_stringForKey(
            HandleRef ptr, 
            string aKey,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern void NSUbiquitousKeyValueStore_setBool_forKey(
            HandleRef ptr, 
            bool value,
            string aKey,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern void NSUbiquitousKeyValueStore_setDouble_forKey(
            HandleRef ptr, 
            double value,
            string aKey,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern void NSUbiquitousKeyValueStore_setLongLong_forKey(
            HandleRef ptr, 
            long value,
            string aKey,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern void NSUbiquitousKeyValueStore_setString_forKey(
            HandleRef ptr, 
            string aString,
            string aKey,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern void NSUbiquitousKeyValueStore_removeObjectForKey(
            HandleRef ptr, 
            string aKey,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern bool NSUbiquitousKeyValueStore_synchronize(
            HandleRef ptr, 
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
                private static extern IntPtr NSUbiquitousKeyValueStore_bufferForKey(
            HandleRef ptr,
            string key,
            ref IntPtr source,
            ref long length,
            ref IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr AddNSUbiquitousKeyValueStoreDidChangeExternallyNotificationObserver(NSUbiquitousKeyValueStoreDidChangeExternallyDelegate handler);

        
        [DllImport(dll)]
        private static extern void RemoveNSUbiquitousKeyValueStoreDidChangeExternallyNotificationObserver(HandleRef observerHandle, ref IntPtr exceptionPtr);

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern IntPtr NSUbiquitousKeyValueStore_GetPropDefaultStore();

        

        #endregion

        internal NSUbiquitousKeyValueStore(IntPtr ptr) : base(ptr) {}
        
        
        
        


        
        /// <summary>
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns>val</returns>
        public bool BoolForKey(
            string aKey)
        { 
            
            var val = NSUbiquitousKeyValueStore_boolForKey(
                Handle, 
                aKey, 
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
        /// <param name="aKey"></param>
        /// <returns>val</returns>
        public long LongLongForKey(
            string aKey)
        { 
            
            var val = NSUbiquitousKeyValueStore_longLongForKey(
                Handle, 
                aKey, 
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
        /// <param name="aKey"></param>
        /// <returns>val</returns>
        public double DoubleForKey(
            string aKey)
        { 
            
            var val = NSUbiquitousKeyValueStore_doubleForKey(
                Handle, 
                aKey, 
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
        /// <param name="aKey"></param>
        /// <returns>val</returns>
        public string StringForKey(
            string aKey)
        { 
            
            var val = NSUbiquitousKeyValueStore_stringForKey(
                Handle, 
                aKey, 
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
        /// <param name="value"></param><param name="aKey"></param>
        /// <returns>void</returns>
        public void SetBool(
            bool value, 
            string aKey)
        { 
            
            
            NSUbiquitousKeyValueStore_setBool_forKey(
                Handle, 
                value, 
                
                aKey, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="value"></param><param name="aKey"></param>
        /// <returns>void</returns>
        public void SetDouble(
            double value, 
            string aKey)
        { 
            
            
            NSUbiquitousKeyValueStore_setDouble_forKey(
                Handle, 
                value, 
                
                aKey, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="value"></param><param name="aKey"></param>
        /// <returns>void</returns>
        public void SetLongLong(
            long value, 
            string aKey)
        { 
            
            
            NSUbiquitousKeyValueStore_setLongLong_forKey(
                Handle, 
                value, 
                
                aKey, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="aString"></param><param name="aKey"></param>
        /// <returns>void</returns>
        public void SetString(
            string aString, 
            string aKey)
        { 
            
            
            NSUbiquitousKeyValueStore_setString_forKey(
                Handle, 
                aString, 
                
                aKey, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns>void</returns>
        public void RemoveObjectForKey(
            string aKey)
        { 
            
            NSUbiquitousKeyValueStore_removeObjectForKey(
                Handle, 
                aKey, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
        }
        

        
        /// <summary>
        /// </summary>
        /// 
        /// <returns>val</returns>
        public bool Synchronize()
        { 
            var val = NSUbiquitousKeyValueStore_synchronize(
                Handle, out IntPtr exceptionPtr);

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
public byte[] BufferForKey(
            string key)
        {
            IntPtr source = IntPtr.Zero;
            IntPtr exceptionPtr = IntPtr.Zero;
            long length = 0;

            NSUbiquitousKeyValueStore_bufferForKey(
                Handle,
                key,
                ref source,
                ref length,
                ref exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            byte[] bytes = new byte[length];
            Marshal.Copy(source, bytes, 0, (int) length);
            return bytes;
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns>val</returns>
        private static readonly Dictionary<IntPtr,Action<long,string[]>> NSUbiquitousKeyValueStoreDidChangeExternallyNotificationHandlers = new Dictionary<IntPtr,Action<long,string[]>>();

        [MonoPInvokeCallback(typeof(NSUbiquitousKeyValueStoreDidChangeExternallyDelegate))]
        private static void NSUbiquitousKeyValueStoreDidChangeExternallyNotificationStaticHandler(IntPtr ptr, long reason, IntPtr stringArrPtr, long stringsCount)
        {
            Action<long, string[]> handler;
            if (NSUbiquitousKeyValueStoreDidChangeExternallyNotificationHandlers.TryGetValue(ptr, out handler))
            {
                string[] strings = new string[stringsCount];

                for (int i = 0; i < stringsCount; i++)
                {
                    IntPtr ptr2 = Marshal.ReadIntPtr(stringArrPtr + (i * 8));
                    strings[i] = Marshal.PtrToStringAuto(ptr2);
                }

                handler.Invoke(reason, strings);
            }
        }

        public Unsubscriber AddDidChangeExternallyNotificationObserver(Action<long,string[]> observer)
        {
            IntPtr exceptionPtr = IntPtr.Zero;

            IntPtr observerHandle = AddNSUbiquitousKeyValueStoreDidChangeExternallyNotificationObserver(NSUbiquitousKeyValueStoreDidChangeExternallyNotificationStaticHandler);
            
            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            NSUbiquitousKeyValueStoreDidChangeExternallyNotificationHandlers[observerHandle] = observer;

            return observerHandle == IntPtr.Zero ? null : new Unsubscriber(observerHandle);
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns>val</returns>
public void RemoveDidChangeExternallyNotificationObserver(Unsubscriber unsubscriber)
        {
            IntPtr exceptionPtr = IntPtr.Zero;
            RemoveNSUbiquitousKeyValueStoreDidChangeExternallyNotificationObserver(unsubscriber.Handle, ref exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }

            NSUbiquitousKeyValueStoreDidChangeExternallyNotificationHandlers.Remove(HandleRef.ToIntPtr(unsubscriber.Handle));
        }
        

        
        
        
        /// <value>DefaultStore</value>
        public static NSUbiquitousKeyValueStore DefaultStore
        {
            get 
            { 
                IntPtr defaultStore = NSUbiquitousKeyValueStore_GetPropDefaultStore();
                return defaultStore == IntPtr.Zero ? null : new NSUbiquitousKeyValueStore(defaultStore);
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void NSUbiquitousKeyValueStore_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("NSUbiquitousKeyValueStore Dispose");
                NSUbiquitousKeyValueStore_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~NSUbiquitousKeyValueStore()
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
