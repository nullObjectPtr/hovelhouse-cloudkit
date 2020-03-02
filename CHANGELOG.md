# Changelog
All notable changes to this project will be documented in this file.

## [Unreleased]
- Better error handling, passing exceptions from managed code up to C#

## [0.1.1] - 2020-03-02
### Changed
- Callbacks are now executed on the main thread (breaking change)
- Classes correctly implement IDisposable interface and release their managed pointers when they are finalized (destroyed)
- Updated Roadmap

## [0.1.0] - 2020-02-24
### Added
- Initial preview release
