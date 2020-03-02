//
//  CKAccountStatus.cs
//
//  Created by Jonathan on 02/25/2020
//  Copyright © 2020 HovelHouseApps. All rights reserved.
//

namespace HovelHouse.CloudKit
{
	// The backing type is long, because that's what they are in Objective-C
	// Although I doubt we'd ever run into a situation where a smaller backing
	// type would matter. But hey, futureproofing, amirite?

    public enum CKAccountStatus : long
    {
        CouldNotDetermine = 0,
        Available = 1,
        Restricted = 2,
        NoAccount = 3
    }
}
