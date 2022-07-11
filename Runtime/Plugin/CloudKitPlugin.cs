//
//  CKAcceptSharesOperation.cs
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
    public class CloudKitPlugin
    {
        #region dll
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif
        
        [DllImport(dll)]
        private static extern void CloudKitPlugin_SetLogLevel(
            int LogLevel
            );
        #endregion
        public static void SetNativeLogLevel(int LogLevel)
        {
            CloudKitPlugin_SetLogLevel(LogLevel);
        }
    }
}
