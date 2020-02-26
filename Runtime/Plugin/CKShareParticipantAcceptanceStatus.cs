//
//  CKShareParticipantAcceptanceStatus.cs
//
//  Created by Jonathan on 02/25/2020
//  Copyright © 2020 HovelHouseApps. All rights reserved.
//

namespace HovelHouse.CloudKit
{
	// The backing type is long, because that's what they are in Objective-C
	// Although I doubt we'd ever run into a situation where a smaller backing
	// type would matter. But hey, futureproofing, amirite?

    public enum CKShareParticipantAcceptanceStatus : long
    {
        Unknown = 0,
        Pending = 1,
        Accepted = 2,
        Removed = 3
    }
}
