# Changelog
All notable changes to this project will be documented in this file.

## [Unreleased]
- Fix capitalization mistakes (Breaking Change)
- Change plugin's static "initWith" functions to proper constructors (Breaking Change)
- Callback functions for CKFetchRecordsOperation

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
