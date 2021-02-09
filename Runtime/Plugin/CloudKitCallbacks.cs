//
//  CloudKitCallbacks.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 05/28/2020
//  Copyright © 2021 HovelHouseApps. All rights reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential
//

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace HovelHouse.CloudKit
{

	public delegate void CKApplicationPermissionsDelegate(
        IntPtr _this, 
        ulong invocationId,
    	CKApplicationPermissionStatus applicationPermissionsStatus,
    	IntPtr error);
    
	public delegate void CKAccountStatusDelegate(
        IntPtr _this, 
        ulong invocationId,
    	CKAccountStatus accountStatus,
    	IntPtr error);
    
	public delegate void UserIdentitiesDelegate(
        IntPtr _this,
    	[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 2)]
		IntPtr[] userIdentities,
		long userIdentitiesCount,
    	IntPtr error);
    
	public delegate void UserIdentityDelegate(
        IntPtr _this, 
        ulong invocationId,
    	IntPtr userIdentity,
    	IntPtr error);
    
	public delegate void CKShareParticipantDelegate(
        IntPtr _this, 
        ulong invocationId,
    	IntPtr shareParticipant,
    	IntPtr error);
    
	public delegate void CKRecordIDDelegate(
        IntPtr _this, 
        ulong invocationId,
    	IntPtr _recordID,
    	IntPtr _error);
    
	public delegate void CKRecordDelegate(
        IntPtr _this, 
        ulong invocationId,
    	IntPtr _record,
    	IntPtr _error);
    
	public delegate void CKRecordListDelegate(
        IntPtr _this, 
        ulong invocationId,
    	[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 3)]
		IntPtr[] _recordID,
		long _recordIDCount,
    	IntPtr _error);
    
	public delegate void CKRecordZoneDelegate(
        IntPtr _this, 
        ulong invocationId,
    	IntPtr recordZone,
    	IntPtr error);
    
	public delegate void CKRecordZoneListDelegate(
        IntPtr _this, 
        ulong invocationId,
    	[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 3)]
		IntPtr[] _recordZones,
		long _recordZonesCount,
    	IntPtr _error);
    
	public delegate void CKRecordZoneIDDelegate(
        IntPtr _this, 
        ulong invocationId,
    	IntPtr recordZoneID,
    	IntPtr error);
    
	public delegate void CKSubscriptionDelegate(
        IntPtr _this, 
        ulong invocationId,
    	IntPtr _subscription,
    	IntPtr _error);
    
	public delegate void CKSubscriptionArrayDelegate(
        IntPtr _this, 
        ulong invocationId,
    	[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 3)]
		IntPtr[] _subscriptionArr,
		long _subscriptionArrCount,
    	IntPtr _error);
    
	public delegate void CKLongLivedOperationIdsDelegate(
        IntPtr _this, 
        ulong invocationId,
    	[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 3)]
		IntPtr[] outstandingOperationIDs,
		long outstandingOperationIDsCount,
    	IntPtr error);
    
	public delegate void CKLongLivedOperationDelegate(
        IntPtr _this, 
        ulong invocationId,
    	IntPtr operationID,
    	IntPtr error);
    
	public delegate void CKShareDelegate(
        IntPtr _this, 
        ulong invocationId,
    	IntPtr acceptedShare,
    	IntPtr error);
    
	public delegate void CKShareMetadataDelegate(
        IntPtr _this, 
        ulong invocationId,
    	IntPtr metadata,
    	IntPtr error);
    
	public delegate void NSStringDelegate(
        IntPtr _this, 
        ulong invocationId,
    	IntPtr str,
    	IntPtr error);
    
	public delegate void PerRecordProgressDelegate(
        IntPtr _this,
    	IntPtr _record,
    	double _progress);
    
	public delegate void PerRecordProgressDelegate2(
        IntPtr _this,
    	IntPtr _record,
    	double _progress);
    
	public delegate void PerRecordCompletionDelegate(
        IntPtr _this,
    	IntPtr _record,
    	IntPtr _error);
    
	public delegate void PerRecordCompletionDelegate2(
        IntPtr _this,
    	IntPtr _record,
    	IntPtr _recordID,
    	IntPtr _error);
    
	public delegate void ModifyRecordsCompletionDelegate(
        IntPtr _this,
    	[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 2)]
		IntPtr[] _savedRecords,
		long _savedRecordsCount,
    	[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 4)]
		IntPtr[] _deletedRecordIDs,
		long _deletedRecordIDsCount,
    	IntPtr _operationError);
    
	public delegate void DiscoverAllUserIdentitiesCompletionDelegate(
        IntPtr _this,
    	IntPtr _operationError);
    
	public delegate void DiscoverUserIdentitiesCompletionDelegate(
        IntPtr _this,
    	IntPtr _operationError);
    
	public delegate void ChangeTokenUpdatedDelegate(
        IntPtr _this,
    	IntPtr _serverChangeToken);
    
	public delegate void FetchDatabaseChangesCompletionDelegate(
        IntPtr _this,
    	IntPtr _serverChangeToken,
    	bool _moreComing,
    	IntPtr _operationError);
    
	public delegate void RecordZoneWithIDChangedDelegate(
        IntPtr _this,
    	IntPtr _zoneID);
    
	public delegate void RecordZoneWithIDWasDeletedDelegate(
        IntPtr _this,
    	IntPtr _zoneID);
    
	public delegate void RecordZoneWithIDWasPurgedDelegate(
        IntPtr _this,
    	IntPtr _zoneID);
    
//	public delegate void FetchRecordZoneChangesCompletionDelegate(
//        IntPtr _this,
//    	IntPtr _operationError);
    
	public delegate void RecordChangedDelegate(
        IntPtr _this,
    	IntPtr _record);
    
	public delegate void RecordWithIDWasDeletedDelegate(
        IntPtr _this,
    	IntPtr _recordID,
    	IntPtr _recordType);
    
	public delegate void RecordZoneFetchCompletionDelegate(
        IntPtr _this,
    	IntPtr _recordZoneID,
    	IntPtr _serverChangeToken,
    	IntPtr _clientChangeTokenData,
    	bool _moreComing,
    	IntPtr _recordZoneError);
    
	public delegate void FetchRecordZonesCompletionDelegate(
        IntPtr _this,
    	[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 3)]
		IntPtr[] _recordZonesByZoneIDKeys,
		[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 3)]
		IntPtr[] _recordZonesByZoneIDValues,
		long _recordZonesByZoneIDCount,
    	IntPtr _operationError);
    
	public delegate void ModifyRecordZonesCompletionDelegate(
        IntPtr _this,
    	[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 2)]
		IntPtr[] _savedRecordZones,
		long _savedRecordZonesCount,
    	[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 4)]
		IntPtr[] _deletedRecordZoneIDs,
		long _deletedRecordZoneIDsCount,
    	IntPtr _operationError);
    
	public delegate void PerShareCompletionDelegate(
        IntPtr _this,
    	IntPtr _shareMetadata,
    	IntPtr _acceptedShare,
    	IntPtr _error);
    
	public delegate void AcceptSharesCompletionDelegate(
        IntPtr _this,
    	IntPtr _operationError);
    
	public delegate void FetchRecordsCompletionDelegate(
        IntPtr _this,
    	[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 3)]
		IntPtr[] _recordsByRecordIDKeys,
		[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 3)]
		IntPtr[] _recordsByRecordIDValues,
		long _recordsByRecordIDCount,
    	IntPtr _operationError);
    
	public delegate void RecordZoneChangeTokensUpdatedDelegate(
        IntPtr _this,
    	IntPtr _recordZoneID,
    	IntPtr _serverChangeToken,
    	IntPtr _clientChangeTokenData);
    
	public delegate void UserIdentityDiscoveredDelegate(
        IntPtr _this,
    	IntPtr _identity,
    	IntPtr _lookupInfo);
    
	public delegate void FetchRecordZoneChangesCompletionDelegate(
        IntPtr _this,
    	IntPtr _operationError);
    
	public delegate void FetchShareMetadataCompletionDelegate(
        IntPtr _this,
    	IntPtr _operationError);
    
	public delegate void PerShareMetadataDelegate(
        IntPtr _this,
    	IntPtr _shareURL,
    	IntPtr _shareMetadata,
    	IntPtr _error);
    
	public delegate void FetchShareParticipantsCompletionDelegate(
        IntPtr _this,
    	IntPtr _operationError);
    
	public delegate void ShareParticipantFetchedDelegate(
        IntPtr _this,
    	IntPtr _participant);
    
	public delegate void LongLivedOperationWasPersistedDelegate(
        IntPtr _this);
    
	public delegate void QueryCompletionDelegate(
        IntPtr _this,
    	IntPtr _cursor,
    	IntPtr _operationError);
    
	public delegate void RecordFetchedDelegate(
        IntPtr _this,
    	IntPtr _record);
    
	public delegate void ModifySubscriptionsCompletionDelegate(
        IntPtr _this,
    	[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 2)]
		IntPtr[] savedSubscriptions,
		long savedSubscriptionsCount,
    	[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)]
		IntPtr[] deletedSubscriptionIDs,
		long deletedSubscriptionIDsCount,
    	IntPtr operationError);
    
	public delegate void FetchSubscriptionCompletionDelegate(
        IntPtr _this,
    	[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 3)]
		IntPtr[] subscriptionsBySubscriptionIDKeys,
		[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.SysInt, SizeParamIndex = 3)]
		IntPtr[] subscriptionsBySubscriptionIDValues,
		long subscriptionsBySubscriptionIDCount,
    	IntPtr operationError);
    

public delegate void NSUbiquitousKeyValueStoreDidChangeExternallyDelegate(IntPtr observerHandle, long reason, IntPtr changedKeysPtr, long count);

public delegate void NotificationDelegate(IntPtr observerHandle, IntPtr notification);
public delegate void CKNotificationDelegate(IntPtr notification, string className);

public delegate void RegisteredForNotificationsCallback(
    [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)]
    byte[] token,
    long tokenLength,
    IntPtr error);
}

