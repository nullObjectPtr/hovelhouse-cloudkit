//
//  CKSubscriptionType.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 04/16/2020
//  Copyright © 2020 HovelHouseApps. All rights reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential
//

namespace HovelHouse.CloudKit
{
	// The backing type is long, because that's what they are in Objective-C
	// Although I doubt we'd ever run into a situation where a smaller backing
	// type would matter. But hey, futureproofing, amirite?

    public enum CKSubscriptionType : long
    {
        Query = 1,
        RecordZone = 2,
        Database = 3
    }
}
