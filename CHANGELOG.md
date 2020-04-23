# Changelog

All notable changes to this project will be documented in this file.
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

### Unreleased

- Added SearchService.SearchByPostCodeOrSize, a method to search for both parameters at the same time [#31](https://github.com/pyrocumulus/pvoutput.net/pull/31)
- Added SystemService.PostSystem, enabling the modification of a system's name and/or extended value [#32](https://github.com/pyrocumulus/pvoutput.net/pull/32)

## [0.7.0] - 2020-04-08

### Added

- Default values to parameters of the StatusPostBuilder.
- Structured search methods to the SearchService. [#27](https://github.com/pyrocumulus/pvoutput.net/issues/27)

### Fixed

- Fixed a bug with AddBatchOutputRequest only sending Maximum temperature if the Minimum was set. [#26](https://github.com/pyrocumulus/pvoutput.net/issues/26)
- Fixed bugs with encoding of text parameters `Comments` and `TextMessage` in `OutputPost` and `StatusPost` requests respectively. [#29](https://github.com/pyrocumulus/pvoutput.net/issues/29)
- Fixed bug with `BatchOutputPostBuilder` accepting an output without both `EnergyGenerated` as wel as `EnergyUsed`. [#29](https://github.com/pyrocumulus/pvoutput.net/issues/29)
- Fixed a bug with the SearchService not properly encoding the query text. [#27](https://github.com/pyrocumulus/pvoutput.net/issues/27)

### Breaking changes

- Incorrect signature for adding batch outputs (missing properties). Builders for creating `IOutputPost` and `IBatchOutputPost` have changed significantly. [#29](https://github.com/pyrocumulus/pvoutput.net/issues/29)
- Moved all builders to new namespace `PVOutput.Net.Builders`. [#29](https://github.com/pyrocumulus/pvoutput.net/issues/29)
- Renamed multiple operations on the new `OutputPostBuilders` to accurately map to the corresponding property on IOutputPost etc. [#29](https://github.com/pyrocumulus/pvoutput.net/issues/29)
- All properties respresenting a time are now respresented as `TimeSpan` objects instead of `DateTime`. [#30](https://github.com/pyrocumulus/pvoutput.net/issues/30)
- `PVCoordinate` now uses the `decimal` type for storing the `Latitude` and `Longitude` properties, to avoid weird approximation issues. [#30](https://github.com/pyrocumulus/pvoutput.net/issues/30)

## [0.6.0] - 2020-03-28

### Added

- ASP.&#8203;Net Core support though DI and an extension method [#21](https://github.com/pyrocumulus/pvoutput.net/issues/21)
- Logging support through the ILogger abstractions [#24](https://github.com/pyrocumulus/pvoutput.net/pull/24)
- Almost all public methods in the library now validate arguments [#23](https://github.com/pyrocumulus/pvoutput.net/pull/23)

## [0.5.0] - 2020-02-27

### Added

- Initial release
