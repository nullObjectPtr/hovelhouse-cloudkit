//
//  NSQualityOfService.cs
//
//  Created by Jonathan on 02/25/2020
//  Copyright © 2020 HovelHouseApps. All rights reserved.
//

namespace HovelHouse.CloudKit
{
	// The backing type is long, because that's what they are in Objective-C
	// Although I doubt we'd ever run into a situation where a smaller backing
	// type would matter. But hey, futureproofing, amirite?

    public enum NSQualityOfService : long
    {
        UserInteractive = 33,
        UserInitiated = 25,
        Utility = 17,
        Background = 9,
        Default = -1
    }
}
