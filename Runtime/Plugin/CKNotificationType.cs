//
//  CKNotificationType.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 09/24/2020
//  Copyright © 2021 HovelHouseApps. All rights reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential
//

namespace HovelHouse.CloudKit
{
    public enum CKNotificationType : long
    {
        Query = 1,
        RecordZone = 2,
        ReadNotification = 3,
        Database = 4
    }
}
