//
//  CKServerChangeToken.cs
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
    /// A piece of record meta data that changes each time the record is updated. Can be used to tell if a record is out of sync with the database.
    /// </summary>
    /// <remarks>
    /// Compare your current records server change token against the database to see if your local copy of the data is out of sync with the cloud. If the changes you made to your record are made on the most up-to-date version of the information, then your records server change token will match whats on the cloud.
    /// </remarks>
    public class CKServerChangeToken : CKObject, IDisposable
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif

        // Class Methods
        

        

        

        

        // Properties
        

        #endregion

        internal CKServerChangeToken(IntPtr ptr) : base(ptr) {}
        
        
        
        


        
        
        

        

        
        #region IDisposable Support
        [DllImport(dll)]
        private static extern void CKServerChangeToken_Dispose(HandleRef handle);
            
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                
                //Debug.Log("CKServerChangeToken Dispose");
                CKServerChangeToken_Dispose(Handle);
                disposedValue = true;
            }
        }

        ~CKServerChangeToken()
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
