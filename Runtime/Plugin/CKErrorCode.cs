//
//  CKErrorCode.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 05/28/2020
//  Copyright © 2020 HovelHouseApps. All rights reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential
//

namespace HovelHouse.CloudKit
{
	// The backing type is long, because that's what they are in Objective-C
	// Although I doubt we'd ever run into a situation where a smaller backing
	// type would matter. But hey, futureproofing, amirite?

    public enum CKErrorCode : long
    {
        CKErrorAlreadyShared = 30,
        CKErrorAssetFileModified = 17,
        CKErrorAssetFileNotFound = 26,
        CKErrorBadContainer = 5,
        CKErrorBadDatabase = 24,
        CKErrorBatchRequestFailed = 22,
        CKErrorChangeTokenExpired = 21,
        CKErrorConstraintViolation = 19,
        CKErrorIncompatibleVersion = 18,
        CKErrorInternalError = 1,
        CKErrorInvalidArguments = 12,
        CKErrorLimitExceeded = 27,
        CKErrorManagedAccountRestricted = 32,
        CKErrorMissingEntitlement = 8,
        CKErrorNetworkFailure = 4,
        CKErrorNetworkUnavailable = 3,
        CKErrorNotAuthenticated = 9,
        CKErrorOperationCancelled = 20,
        CKErrorPartialFailure = 2,
        CKErrorParticipantMayNeedVerification = 33,
        CKErrorPermissionFailure = 10,
        CKErrorQuotaExceeded = 25,
        CKErrorReferenceViolation = 31,
        CKErrorRequestRateLimited = 7,
        CKErrorServerRecordChanged = 14,
        CKErrorServerRejectedRequest = 15,
        CKErrorServerResponseLost = 34,
        CKErrorServiceUnavailable = 6,
        CKErrorTooManyParticipants = 29,
        CKErrorUnknownItem = 11,
        CKErrorUserDeletedZone = 28,
        CKErrorZoneBusy = 23,
        CKErrorZoneNotFound = 26,
        CKErrorAssetNotAvailable = 35
    }
}
