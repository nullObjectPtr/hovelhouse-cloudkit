# Changelog
All notable changes to this project will be documented in this file.

## [Unreleased]

## [1.1.9] - 2021-03-07
### Added
- Added properties 'LongLived', 'TimeoutIntervalForRequest', 'TimeoutIntervalForResource', and 'AllowCellularAccess' to CKOperationConfiguration

## [1.1.8] - 2021-02-03
### Added
- Added support for Date fields in CKRecord
- Added method PartialErrorForItemId to NSError which can be used to retrieve more specific error information on a per-record basis from an error of type "CkPartialFailure""
- Added property "resultsLimit" to CKQueryOperation. 
- Added Examlpe_QueryCursor to demonstrate CKQueryOperations usage

## [1.1.7] - 2021-01-27
### Changed
- Added missing constructor function to NSSortDescriptor. Added a code sample for how to use it
- Fixed issue where CKQueryOperation did not inherit from CKDatabaseOperation

## [1.1.6] - 2020-11-30
### Changed
- MacOS bundle built with arm64 architecture for new apple silicon macs

### Added
- added functions for fetching information from the NSError userInfo dictionary
- added CKErrorCode enum

## [1.1.5] - 2020-11-10
### Changed
- added the QueuePriority property from NSOperation to CKOperation, this helps address a bug iOS 14.1

## [1.1.4] - 2020-10-28
### Changed
- added additional properties to NSURL to make dealing with CKAsset a bit easier
- fixed a potential crash caused by not calling the completion handler when recieving remote notifications
- fixed errors in MacOS signing script
- fixed an issue where Unity could generate an entitlements file that could not be parsed by the codesign utility if no containers were specified

## [1.1.3] - 2020-09-29
### Changed
- zip file unzips to the properly named directory now, fixes issue where the build post process script could not find the resigning script path unless the user renamed the folder after installation
- the MacOS settings now use relative paths for the info.plist and entitlements file
- fixes bug in signing script that could not handle signing apps with spaces in the path

## [1.1.2] - 2020-07-29
### Changed
- adding a missing didRecieveRemoteNotifications:fetchCompletion handler, the absence of which prevented some types of CloudKitNotifications from being recieved by the client
- fixed a crash that occured when attempting to register for remote notifications twice in a single session

## [1.1.1]- 2020-07-17
### Changed
- the iOS library is now a fat binary with with armv7 architecture included in it. armv7 is set to be deprecated, but building a universal binary is still the default in many unity versions. Also, it doesn't hurt to be backwards compatible if its easy to do. This reduces the minimum target iOS version from 11 to 10.3

## [1.1.0] - 2020-06-11
### Changed
- CloudKit notifications working for all platforms now
- Swizzled app delegate methods will call the original method if present now
- Build process adds the APS entitlement if cloudkit notifications are enabled

## [1.0.1] - 2020-06-04
### Changed
- Callbacks are now invoked on the calling methods syncronization context instead of the unity game thread. This makes callbacks usable in a wider variety of contexts, such as a separate worker thread. Potentially a breaking change for anyone who had coded around this limitation. Most users should be fine.
### Added
- Added support for CloudKit notifications. Added missing API classes and methods: CKDatabaseSubscription, CKFetchSubscriptionsOperation, CKModifySubscriptionsOperation, and CKRecordZoneSubscription.
- Added an example script that demo's how to use subscriptions
### Removed
- Removed the link to the git url from the setup instructions. The repository will become private once the plugin goes live on the asset store. 

## [1.0.0] - 2020-05-02
### Changed
- this version is exactly the same as version 0.3.1 - it was submitted to the asset store for consideration as a paid plugin and the version number was bumped to 1.0.0 to take it out of "preview" status in the package manager

## [0.3.1] - 2020-04-22
### Changed
- fixed bug where the signing script could not be located if the plugin was not imported as an embedded package
- fixed a bug in the signing script that did not handle paths with spaces
- fixed a bug with the MacOS build process where the creation of the entitlements file would fail if no partial entitlements file was specified in the build settings (you can now build a MacOS project without needing to specify a partial entitlements file)
- MacOS standalone project now automatically adds the (required) ApplicationIdentifier entitlement
- KVS disabled by default in build settings due to an issue with UnityCloudBuild

## [0.3.0] - 2020-04-17
### Added
- Added integration test stubs (and a few) integration tests. You can run them by adding the entry "testables":["com.hovelhouse.cloudkit"] to the manifest.json file in the packages directory

### Changed
- Overrode equality for CKObject so that managed objects are considered equal when they point to the same underlying unmanaged pointer (breaking change)
- Added missing methods to CKShare
- Adjusted post process build step for MacOS target, so that it does not run the signing script when the "Create XCode Project" option is checked in build settings
- Renamed MacOS bundle to HHCloudKitMacOS to match the other libraries better

## [0.2.3] - 2020-04-09
### Added
- Added methods for the class CKShare
- Stubs for Integration Tests

## [0.2.2] - 2020-04-04
### Added
- A post process build script that handles MacOS signing

## [0.2.1] - 2020-03-30
### Added
- Added some basic documentation to C# API
- Updated readme with links to support, formus and web documentaiton.
### Changed
- Fixed issue where libraries prevented an app from being archived if BitCode was enabled in build settings

## [0.2.0] - 2020-03-13
### Added
- Added support and examples for ubiquitous key value stores (see NSFileManager and NSUbiquitousKeyValueStore)
- Added account status change notifications and examples
- Implemented more event handlers for less commonly used CKOperations

### Changed
- Fixed bugs where some database and container operations did not subclass from CKOperation or CKDatabaseOperation
- Converted static initWith methods to Constructors (breaking change)
- Fixed an issue where concrete classes with concrete parent classes were not property disposed (CKShare)

## [0.1.2] - 2020-03-05
### Added
- Post process build step to automatically add the appropriate CloudKit entilements and capabilities the xcode project
### Changed
- Plugin no longer requires building out to an xcode project. (Build and Run works)
- Better error handling. Most exceptions thrown from unmanaged code are caught and passed as parameters to C# where they are re-thrown as managed exceptions
- Removed deprecated GUI Layer component from camera's in example scenes
- Updated build instructions in the readme

## [0.1.1] - 2020-03-02
### Changed
- Callbacks are now executed on the main thread (breaking change)
- Classes correctly implement IDisposable interface and release their managed pointers when they are finalized (destroyed)
- Updated Roadmap

## [0.1.0] - 2020-02-24
### Added
- Initial preview release
