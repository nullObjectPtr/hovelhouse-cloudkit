//
//  CKDatabaseOperation.cs
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
    /// The parent class for all opretations that can be run on a database
    /// </summary>
    /// <remarks>
    /// Only operations that inherit from CKDatabaseOperation may be run on a database instance. The database property of the operation is set when the operation is added to the database using it&apos;s AddOperation method. One gotcha here is that the default QualityOfService for database operations is probably too slow for you. Set it to UserInitiated to ensure that the operation completes in a timely manner.
    /// </remarks>
    public class CKDatabaseOperation : CKOperation, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        

        

        

        // Properties
        
        [DllImport(dll)]
        private static extern IntPtr CKDatabaseOperation_GetPropDatabase(HandleRef ptr);
        
        [DllImport(dll)]
        private static extern void CKDatabaseOperation_SetPropDatabase(HandleRef ptr, IntPtr database, out IntPtr exceptionPtr);

        

        #endregion

        internal CKDatabaseOperation(IntPtr ptr) : base(ptr) {}
        internal CKDatabaseOperation(){}
        
        
        
        


        
        
        
        /// <value>Database</value>
        public CKDatabase Database
        {
            get 
            { 
                IntPtr database = CKDatabaseOperation_GetPropDatabase(Handle);
                return database == IntPtr.Zero ? null : new CKDatabase(database);
            }
            set
            {
                CKDatabaseOperation_SetPropDatabase(Handle, value != null ? HandleRef.ToIntPtr(value.Handle) : IntPtr.Zero, out IntPtr exceptionPtr);
            }
        }

        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKDatabaseOperation_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKDatabaseOperation Dispose");
                CKDatabaseOperation_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKDatabaseOperation()
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
