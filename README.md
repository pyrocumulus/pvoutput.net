# PVOutput.Net

[![Actions Status](https://github.com/pyrocumulus/pvoutput.net/workflows/dotnetcore/badge.svg)](https://github.com/pyrocumulus/pvoutput.net/actions)

> A .NET wrapper library for the [PVOutput api](https://pvoutput.org/help.html#api-spec).

## Install

Nuget package will come when the implementation reaches a usable version. A version that's getting the basic information out of PVOutput and is able to push basic data back to PVOutput, is regarded as usable.
The project will adhere to [Semver](https://semver.org/) for versioning. Version 1.x will take a good amount of time, because for that there needs to be stability in the API and 100% API coverage.

## Usage

For now sample code can be found in the samples/folder of the project. Documentation will come eventually.

## API Coverage

Currently all read operations of the API are implemented as are all of the operations for adding data.

| Operation  | Object       | (Expected) Module | Status             | Documentation link      |
|------------|--------------|-------------------|--------------------|-------------------------|
| Add        | Output       | Output            | :heavy_check_mark: | [Add Output](https://pvoutput.org/help.html#api-addoutput) |
| Add        | Batch Output | Output            | :heavy_check_mark: | [Add Batch Output](https://pvoutput.org/help.html#api-addbatchoutput) |
| Get        | Output       | Output            | :heavy_check_mark: | [Get Output](https://pvoutput.org/help.html#api-getoutput) |
| Add        | Status       | Status            | :heavy_check_mark: | [Add Status](https://pvoutput.org/help.html#api-addstatus) |
| Add        | Batch Status | Status            | :heavy_check_mark: | [Add Batch Status](https://pvoutput.org/help.html#api-addbatchstatus) |
| Get        | Status       | Status            | :heavy_check_mark: | [Get Status](https://pvoutput.org/help.html#api-getstatus) |
| Delete     | Status       | Status            | :heavy_check_mark: | [Delete Status](https://pvoutput.org/help.html#api-deletestatus) |
| Get        | Statistic    | Statistics        | :heavy_check_mark: | [Get Statistic](https://pvoutput.org/help.html#api-getstatistic) |
| Get        | System       | System            | :heavy_check_mark: | [Get System](https://pvoutput.org/help.html#api-getsystem) |
| Post       | System       | System            |                    | [Post System](https://pvoutput.org/help.html#api-postsystem) |
| Get        | Extended     | Extended          | :heavy_check_mark: | [Get Extended](https://pvoutput.org/help.html#api-getextended) |
| Get        | Favourite    | Favourite         | :heavy_check_mark: | [Get Favourite](https://pvoutput.org/help.html#api-getfavourite) |
| Get        | Missing      | Missing           | :heavy_check_mark: | [Get Missing](https://pvoutput.org/help.html#api-getmissing) |
| Get        | Insolation   | Insolation        | :heavy_check_mark: | [Get Insolation](https://pvoutput.org/help.html#api-getinsolation) |
| Get        | Team         | Team              | :heavy_check_mark: | [Get Team](https://pvoutput.org/help.html#api-getteam) |
| Join       | Team         | Team              | :heavy_check_mark: | [Join Team](https://pvoutput.org/help.html#api-jointeam) |
| Leave      | Team         | Team              | :heavy_check_mark: | [Leave Team](https://pvoutput.org/help.html#api-leaveteam) |
| Get        | Supply       | Supply            | :heavy_check_mark: | [Get Supply](https://pvoutput.org/help.html#api-getsupply) |
| Search     | Search       | Search            | :heavy_check_mark: | [Search](https://pvoutput.org/help.html#api-search) |
| Register   | Notification | Notification      |                    | [Register Notification](https://pvoutput.org/help.html#api-registernotification) |
| Deregister | Notification | Notification      |                    | [Deregister Notification](https://pvoutput.org/help.html#api-deregisternotification) |

## Building the source

As the whole solution has all that dotnet magic, you can just run:

```posh
dotnet build
```

to build the solution or a single project. 

For unit testing, just run:

```posh
dotnet test
```

## Contribute

As the code base is still changing a lot, PRs will probably be more practical, benefical and succesful after things have settled down a bit. But you can try.

## License

This project's structure and Request handling have been seriously inspired (in part, copied even) by Henrik Fröhling's work on [Trakt.NET](https://github.com/henrikfroehling/Trakt.NET), when it was still called TraktApiSharp. While this project is licensed under the same license as Trakt.NET, I'd still like to make this absolutely clear.

MIT © Marcel Boersma
