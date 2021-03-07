//
//  CKShareParticipantPermission.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 09/24/2020
//  Copyright © 2021 HovelHouseApps. All rights reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential
//

namespace HovelHouse.CloudKit
{
    public enum CKShareParticipantPermission : long
    {
        Unknown = 0,
        None = 1,
        ReadOnly = 2,
        ReadWrite = 3
    }
}
