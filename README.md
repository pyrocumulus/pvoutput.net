# PVOutput.Net

> A .NET wrapper library for API of the popular [PVOutput](https://pvoutput.org) service.
> PVOutput is a free service for sharing and comparing PV output data.

![.NET Core](https://github.com/pyrocumulus/pvoutput.net/workflows/.NET%20Core/badge.svg) 
[![NuGet Badge](https://buildstats.info/nuget/PVOutput.Net)](https://www.nuget.org/packages/PVOutput.Net/)

## Install

Installation can be done through installation of the [NuGet package](https://www.nuget.org/packages/PVOutput.Net/).

## Usage

For now sample code can be found in the samples/folder of the project. Full API documentation will come soon.

## API Coverage

Currently all read operations of the API are implemented as are all of the operations for adding data.

| Operation  | Object       | Module            | Status             | Documentation link      |
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

The project adheres to [Semver 2.0](https://semver.org/) for versioning. As the code base is still changing a lot, PRs will probably be more practical, benefical and succesful after things have settled down a bit. But you can try.

## License

This project's structure and Request handling have been seriously inspired (in part, copied even) by Henrik Fröhling's work on [Trakt.NET](https://github.com/henrikfroehling/Trakt.NET), when it was still called TraktApiSharp. While this project is licensed under the same license as Trakt.NET, I'd still like to make this absolutely clear.

MIT © Marcel Boersma
