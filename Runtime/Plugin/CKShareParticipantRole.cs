//
//  CKShareParticipantRole.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 09/24/2020
//  Copyright © 2021 HovelHouseApps. All rights reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential
//

namespace HovelHouse.CloudKit
{
    public enum CKShareParticipantRole : long
    {
        Owner = 1,
        PrivateUser = 3,
        PublicUser = 4,
        RoleUnknown = 0
    }
}
