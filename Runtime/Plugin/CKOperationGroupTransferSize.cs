//
//  CKOperationGroupTransferSize.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 03/26/2020
//  Copyright © 2020 HovelHouseApps. All rights reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential
//

namespace HovelHouse.CloudKit
{
	// The backing type is long, because that's what they are in Objective-C
	// Although I doubt we'd ever run into a situation where a smaller backing
	// type would matter. But hey, futureproofing, amirite?

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
