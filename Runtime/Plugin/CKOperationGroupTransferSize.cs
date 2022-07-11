//
//  CKOperationGroupTransferSize.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 09/24/2020
//  Copyright © 2021 HovelHouseApps. All rights reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential
//

namespace HovelHouse.CloudKit
{
    public enum CKOperationGroupTransferSize : long
    {
        Unknown = 0,
        Kilobytes = 1,
        Megabytes = 2,
        TensOfMegabytes = 3,
        HundredsOfMegabytes = 4,
        Gigabytes = 5,
        TensOfGigabytes = 6
    }
}
