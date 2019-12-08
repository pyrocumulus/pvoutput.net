# PVOutput.Net

> A .NET wrapper library for the [PVOutput api](https://pvoutput.org/help.html#api-spec).

## Install

Nuget package will come when the implementation reaches a usable version. A version that's getting the basic information out of PVOutput and is able to push basic data back to PVOutput, is regarded as usable.
The project will adhere to [Semver](https://semver.org/) for versioning. Version 1.x will take a good amount of time, because for that there needs to be stability in the API and 100% API coverage.

## Usage

For now sample code can be found in the samples/ folder of the project. API documentation will come eventually.

## API Coverage

| Operation  | Object       | (Expected) Module | Status      | Documentation link      |
|------------|--------------|-------------------|-------------|-------------------------|
| Add        | Output       | Output            | Implemented | [Add Output](https://pvoutput.org/help.html#api-addoutput) |
| Add        | Status       | Status            | Implemented | [Add Status](https://pvoutput.org/help.html#api-addstatus) |
| Add        | Batch Output | Output            |             | [Add Batch Output](https://pvoutput.org/help.html#api-addbatchoutput) |
| Add        | Batch Status | Status            |             | [Add Batch Status](https://pvoutput.org/help.html#api-addbatchstatus) |
| Get        | Status       | Status            | Implemented | [Get Status](https://pvoutput.org/help.html#api-getstatus) |
| Get        | Statistic    | Staticics         | Implemented | [Get Statistic](https://pvoutput.org/help.html#api-getstatistic) |
| Get        | System       | System            | Implemented | [Get System](https://pvoutput.org/help.html#api-getsystem) |
| Post       | System       | System            |             | [Post System](https://pvoutput.org/help.html#api-postsystem) |
| Get        | Output       | Output            | Implemented | [Get Output](https://pvoutput.org/help.html#api-getoutput) |
| Get        | Extended     | Extended          | Implemented | [Get Extended](https://pvoutput.org/help.html#api-getextended) |
| Get        | Favourite    | Favourite         | Implemented | [Get Favourite](https://pvoutput.org/help.html#api-getfavourite) |
| Get        | Missing      | Missing           | Implemented | [Get Missing](https://pvoutput.org/help.html#api-getmissing) |
| Get        | Insolation   | Insolation        | Implemented | [Get Insolation](https://pvoutput.org/help.html#api-getinsolation) |
| Delete     | Status       | Status            |             | [Delete Status](https://pvoutput.org/help.html#api-deletestatus) |
| Search     |              |                   |             | [Search](https://pvoutput.org/help.html#api-search) |
| Get        | Team         | Team              | Implemented | [Get Team](https://pvoutput.org/help.html#api-getteam) |
| Join       | Team         | Team              |             | [Join Team](https://pvoutput.org/help.html#api-jointeam) |
| Leave      | Team         | Team              |             | [Leave Team](https://pvoutput.org/help.html#api-leaveteam) |
| Get        | Supply       | Supply            |             | [Get Supply](https://pvoutput.org/help.html#api-getsupply) |
| Register   | Notification | Notification      |             | [Register Notification](https://pvoutput.org/help.html#api-registernotification) |
| Deregister | Notification | Notification      |             | [Deregister Notification](https://pvoutput.org/help.html#api-deregisternotification) |

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
