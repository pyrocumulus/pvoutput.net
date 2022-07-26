# Changelog

All notable changes to this project will be documented in this file.
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

## Unreleased

### Fixed

- Updated README formatting for NuGet package page [#132](https://github.com/pyrocumulus/pvoutput.net/pull/132)
- Updated all links to official API documentation [#133](https://github.com/pyrocumulus/pvoutput.net/pull/133)

## [0.11.1] - 2022-07-24

### Fixed

- Added support for undocumented panel orientation 'EastWest' [#131](https://github.com/pyrocumulus/pvoutput.net/pull/131) - Contribution [CodeCasterNL](https://github.com/CodeCasterNL)

## [0.11.0] - 2022-05-05

### Changed

- **BREAKING**: Removed add batch output from API. Please use the `AddOutputsAsync` method from now on. This is donation only. [#78](https://github.com/pyrocumulus/pvoutput.net/pull/78)
- **BREAKING**: Some implementations which have been properly interfaced, are reduced to internal visibility from now on. [#117](https://github.com/pyrocumulus/pvoutput.net/pull/117)

### Updated

- Updated `Microsoft.Extensions.DependencyInjection.Abstractions` from `v6.0.0` to `v6.0.1` [#114](https://github.com/pyrocumulus/pvoutput.net/pull/114)

### Fixed

- Do not throw exception on cancellation of request [#124](https://github.com/pyrocumulus/pvoutput.net/pull/124) - Contribution [CodeCasterNL](https://github.com/CodeCasterNL)

## [0.10.0] - 2022-03-16

### Fixed

- **BREAKING**: Many methods with a parameter of `DateTime` used the parameter name `date`; those have been renamed [#100](https://github.com/pyrocumulus/pvoutput.net/pull/100)
- Logging is now more performant and async flow has been slightly improved [#100](https://github.com/pyrocumulus/pvoutput.net/pull/100)
- `GetOwnSystem()` throws an exception when the system does not have a secondary panel [#112](https://github.com/pyrocumulus/pvoutput.net/pull/112) - Contribution [CodeCasterNL](https://github.com/CodeCasterNL)

## [0.9.2] - 2021-11-22

### Updated

- Updated `Microsoft.CodeAnalysis.NetAnalyzers` from `v5.0.3` to `v6.0.0` [#95](https://github.com/pyrocumulus/pvoutput.net/pull/95)
- Updated `Microsoft.Extensions.DependencyInjection.Abstractions` from `v5.0.0` to `v6.0.0` [ec56607](https://github.com/pyrocumulus/pvoutput.net/commit/ec566073ae7d5f1b81afe61536e0b480f5b128e4)
- Updated `Microsoft.Extensions.Logging.Abstractions` from `v5.0.0` to `v6.0.0` [9fb8db1](https://github.com/pyrocumulus/pvoutput.net/commit/9fb8db126ddad4ab86d28340af021b5eafa27e21)

## [0.9.1] - 2021-06-01

### Added

- Added interfaces to all public types by [#65](https://github.com/pyrocumulus/pvoutput.net/pull/65) - Contribution [CodeCasterNL](https://github.com/CodeCasterNL)

## [0.9.0] - 2021-03-13

### Updated

- Updated `Microsoft.Extensions.DependencyInjection.Abstractions` from `v3.1.7` to `v5.0.0` [#52](https://github.com/pyrocumulus/pvoutput.net/pull/52)
- Updated `Microsoft.Extensions.Logging.Abstractions` from `v3.1.7` to `v5.0.0` [#52](https://github.com/pyrocumulus/pvoutput.net/pull/52)
- Migrated analyzers `Microsoft.CodeAnalysis.FxCopAnalyzers` to `Microsoft.CodeAnalysis.NetAnalyzers` `v5.0.3` [#52](https://github.com/pyrocumulus/pvoutput.net/pull/52)

### Fixed

- **BREAKING**: Some methods will now return an `Orientation` enumeration value instead of a string representation [#49](https://github.com/pyrocumulus/pvoutput.net/pull/49)
- **BREAKING**: Some methods will now return a `Shade` enumeration value instead of a string representation [#50](https://github.com/pyrocumulus/pvoutput.net/pull/50)
- **BREAKING**: Marked `InstallDate` and `ArrayTilt` aspects as nullable, as they are optional in PVOutput [#50](https://github.com/pyrocumulus/pvoutput.net/pull/50)
- Marked assembly as `[CLSCompliant]` [#52](https://github.com/pyrocumulus/pvoutput.net/pull/52)

## [0.8.1] - 2020-11-07

### Added

- Added a logo for the NuGet package [#38](https://github.com/pyrocumulus/pvoutput.net/pull/38)
- Added method to `StatusService` to delete all statuses on a date [#45](https://github.com/pyrocumulus/pvoutput.net/pull/45)
- Added methods to `StatusService` to add net batch statusses and cumulative batch statusses [#47](https://github.com/pyrocumulus/pvoutput.net/pull/47)

### Fixed

- Corrected `SystemService` returning teams, estimates or extended properties with counts greater than 0, but with empty/null content
- Removed dead code from certain code paths found through coverage testing
- Increased code coverage to near 100%

## [0.8.0] - 2020-08-29

### Added

- Added NotificationService, a service to (de)register callbacks for certain PVOutput alerts [#37](https://github.com/pyrocumulus/pvoutput.net/pull/37)

### Fixed

- Updated multiple packages in both projects [0d71267](https://github.com/pyrocumulus/pvoutput.net/pull/37/commits/0d7126716f165829db31f06be14d3bd0143f411c)
- `CancellationToken` not propagating into the `BaseObjectStringReader`

## [0.7.1] - 2020-04-25

### Added

- Added SearchService.SearchByPostCodeOrSize, a method to search for both parameters at the same time [#31](https://github.com/pyrocumulus/pvoutput.net/pull/31)
- Added SystemService.PostSystem, enabling the modification of a system's name and/or extended value [#32](https://github.com/pyrocumulus/pvoutput.net/pull/32)

### Fixed

- Fixed a bug with some services manually UrlEncoding string values, which resulted in double encoding / possible value corruption [#33](https://github.com/pyrocumulus/pvoutput.net/issues/33)

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
