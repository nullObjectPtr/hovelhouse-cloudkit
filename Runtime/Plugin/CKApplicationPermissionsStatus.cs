//
//  CKApplicationPermissionsStatus.cs
//
//  Created by Jonathan on 12/31/19.
//  Copyright © 2019 HovelHouseApps. All rights reserved.
//

namespace HovelHouse 
{
	// The backing type is long, because that's what they are in Objective-C
	// Although I doubt we'd ever run into a situation where a smaller backing
	// type would matter. But hey, futureproofing, amirite?

    public enum CKApplicationPermissionsStatus : long
    {
        InitialState = 0,
        CouldNotComplete = 1,
        StatusDenied = 2,
        StatusGranted = 3
    }
}
