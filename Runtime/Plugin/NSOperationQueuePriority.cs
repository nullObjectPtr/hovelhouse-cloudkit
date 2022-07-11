//
//  NSOperationQueuePriority.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 09/24/2020
//  Copyright © 2021 HovelHouseApps. All rights reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential
//

namespace HovelHouse.CloudKit
{
    public enum NSOperationQueuePriority : long
    {
        VeryLow = -8,
        Low = -4,
        Normal = 0,
        High = 4,
        VeryHigh = 8
    }
}
