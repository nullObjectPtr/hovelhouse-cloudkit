//
//  NSPredicate.cs
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
    /// A predicate - A true/false evaluation that uses an apple-specific query syntax similar in purpose (but not syntax) to SQL
    /// </summary>
    /// <remarks>
    /// TBH the NSPredicate syntax is pretty bizzare. It&apos;s sorta half SQL and half Regex. I&apos;d recommend reading a tutorial on it. https://nshipster.com/nspredicate/ Poorly formatter predicate strings will cause an exception to be thrown.
    /// </remarks>
    public class NSPredicate : CKObject, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        
        [DllImport(dll)]
        private static extern IntPtr NSPredicate_predicateWithValue(
            bool value,
            out IntPtr exceptionPtr);

        
        [DllImport(dll)]
        private static extern IntPtr NSPredicate_predicateWithFormat(
            string predicateFormat,
            out IntPtr exceptionPtr);

        

        

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern IntPtr NSPredicate_GetPropPredicateFormat(HandleRef ptr);

        

        #endregion

        internal NSPredicate(IntPtr ptr) : base(ptr) {}
        
        
        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>val</returns>
        public static NSPredicate PredicateWithValue(
            bool value)
        { 
            
            var val = NSPredicate_predicateWithValue(
                value, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val == IntPtr.Zero ? null : new NSPredicate(val);
        }
        

        
        /// <summary>
        /// </summary>
        /// <param name="predicateFormat"></param>
        /// <returns>val</returns>
        public static NSPredicate PredicateWithFormat(
            string predicateFormat)
        { 
            
            var val = NSPredicate_predicateWithFormat(
                predicateFormat, 
                out IntPtr exceptionPtr);

            if(exceptionPtr != IntPtr.Zero)
            {
                var nativeException = new NSException(exceptionPtr);
                throw new CloudKitException(nativeException, nativeException.Reason);
            }
            
            return val == IntPtr.Zero ? null : new NSPredicate(val);
        }
        

        
        
        


        
        
        
        /// <value>PredicateFormat</value>
        public string PredicateFormat
        {
            get 
            { 
                IntPtr predicateFormat = NSPredicate_GetPropPredicateFormat(Handle);
                return Marshal.PtrToStringAuto(predicateFormat);
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void NSPredicate_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("NSPredicate Dispose");
                NSPredicate_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~NSPredicate()
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
