# Changelog

All notable changes to this project will be documented in this file.
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

## Unreleased

- Added default values to parameters of the StatusPostBuilder
- Fixed a bug with AddBatchOutputRequest only sending Maximum temperature if the Minimum was set [#26](https://github.com/pyrocumulus/pvoutput.net/issues/26)

## [0.6.0] - 2020-03-28

### Added

- ASP.&#8203;Net Core support though DI and an extension method [#21](https://github.com/pyrocumulus/pvoutput.net/issues/21)
- Logging support through the ILogger abstractions [#24](https://github.com/pyrocumulus/pvoutput.net/pull/24)
- Almost all public methods in the library now validate arguments [#23](https://github.com/pyrocumulus/pvoutput.net/pull/23)

## [0.5.0] - 2020-02-27

### Added

- Initial release