# Changelog

All notable changes to this project will be documented in this file.
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

## Unreleased

- Added default values to parameters of the StatusPostBuilder
- Fixed a bug with AddBatchOutputRequest only sending Maximum temperature if the Minimum was set [#26](https://github.com/pyrocumulus/pvoutput.net/issues/26)
- Breaking - incorrect signature for adding batch outputs (missing properties).  [#29](https://github.com/pyrocumulus/pvoutput.net/issues/29)
- Breaking - moved all builders to new namespace `PVOutput.Net.Builders`.  [#29](https://github.com/pyrocumulus/pvoutput.net/issues/29)
- Fixed bugs with encoding of text parameters `Comments` and `TextMessage` in `OutputPost` and `StatusPost` requests respectively. [#29](https://github.com/pyrocumulus/pvoutput.net/issues/29)
- Fixed bug with `BatchOutputPostBuilder` accepting an output without both `EnergyGenerated` as wel as `EnergyUsed`. [#29](https://github.com/pyrocumulus/pvoutput.net/issues/29)
- Breaking - renamed multiple operations on the new `OutputPostBuilders` to accurately map to the corresponding property on IOutputPost etc. [#29](https://github.com/pyrocumulus/pvoutput.net/issues/29)
- Added structured search methods to the SearchService. [#27](https://github.com/pyrocumulus/pvoutput.net/issues/27)
- Fixed a bug with the SearchService not properly encoding the query text. [#27](https://github.com/pyrocumulus/pvoutput.net/issues/27)
- Breaking - all properties respresenting a time are now respresented as `TimeSpan` objects instead of `DateTime`. [#30](https://github.com/pyrocumulus/pvoutput.net/issues/30)
- Breaking - `PVCoordinate` now uses the `decimal` type for storing the `Latitude` and `Longitude` properties, to avoid weird approximation issues. [#30](https://github.com/pyrocumulus/pvoutput.net/issues/30)

## [0.6.0] - 2020-03-28

### Added

- ASP.&#8203;Net Core support though DI and an extension method [#21](https://github.com/pyrocumulus/pvoutput.net/issues/21)
- Logging support through the ILogger abstractions [#24](https://github.com/pyrocumulus/pvoutput.net/pull/24)
- Almost all public methods in the library now validate arguments [#23](https://github.com/pyrocumulus/pvoutput.net/pull/23)

## [0.5.0] - 2020-02-27

### Added

- Initial release
