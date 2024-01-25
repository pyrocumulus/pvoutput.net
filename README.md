# PVOutput.Net

> A .NET Core (Standard 2.0 compatible) wrapper library for API of the popular [PVOutput](https://pvoutput.org) service.
> PVOutput is a free service for sharing and comparing PV output data.

![GitHub last commit (main)](https://img.shields.io/github/last-commit/pyrocumulus/PVOutput.Net/main?label=last%20commit%20%28main%29)
[![NuGet Version](https://img.shields.io/nuget/v/PVOutput.Net.svg?logo=nuget)](https://www.nuget.org/packages/PVOutput.Net/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/PVOutput.Net.svg?logo=nuget)](https://www.nuget.org/packages/PVOutput.Net/)
[![NET Core](https://github.com/pyrocumulus/pvoutput.net/actions/workflows/dotnet-build.yml/badge.svg?branch=develop)](https://github.com/pyrocumulus/pvoutput.net/actions?query=workflow%3A%22.NET+build%22)
[![Code coverage](https://img.shields.io/codecov/c/github/pyrocumulus/PVOutput.Net/develop)](https://codecov.io/gh/pyrocumulus/pvoutput.net)

## Installation

Installation can be done through installation of the [NuGet package](https://www.nuget.org/packages/PVOutput.Net/):

```posh
PM> Install-Package PVOutput.Net
```

## Changelog

See [Changelog](CHANGELOG.md) for information on changes per version, including coming but yet unreleased changes.

## Support

This library is targeting .NET Standard 2.0 and above. For full compatibility details, check the [Microsoft Docs](https://docs.microsoft.com/nl-nl/dotnet/standard/net-standard#net-implementation-support).

**Please note:** that the default branch of the repository is `develop`. This means that it can contain bugfixes/features that are not yet available in the NuGet package.
See [main](https://github.com/pyrocumulus/pvoutput.net/tree/main) for the source code, that was used for building the NuGet package.

## Basic usage

This section describes examples of functionality that the library provides.

### Getting data out of PVOutput.org

```csharp
var client = new PVOutputClient(apiKey: "myPvOutputKey", ownedSystemId: 1);

// Request output for today
var outputResponse = await client.Output.GetOutputForDateAsync(DateTime.Today);
var output = outputResponse.Value;
Console.WriteLine($"Output for date {output.OutputDate.ToShortDateString()}, {output.EnergyGenerated} Wh generated");

```

### Adding data to a system in PVOutput.org

```csharp
var client = new PVOutputClient(apiKey: "myPvOutputKey", ownedSystemId: 1);
var builder = new StatusPostBuilder<IStatusPost>();

// Build the status
var status = builder.SetTimeStamp(DateTime.Now)
                .SetGeneration(200)
                .Build();

// Push the status back to PVOutput
var response = await client.Status.AddStatusAsync(status);

```

### Using the client in an ASP.Net Core application

```csharp
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddPVOutputClient(options =>
        {
            options.ApiKey = "myPvOutputKey";
            options.OwnedSystemId = 1;
        });
    }
```

For more information on usage, please see the [documentation](https://pyrocumulus.github.io/pvoutput.net/).

## API Coverage

As of 0.8.0 this library contains the complete public API that official PVOutput exposes. See [documentation](https://pyrocumulus.github.io/pvoutput.net/) for details.

## Contribute

See [Contributing](CONTRIBUTING.md) for information on how to contribute to this project.

## Contributors

- [CodeCasterNL](https://github.com/CodeCasterNL)

## Building the project

As the whole solution has all that dotnet magic, you can just run:

```posh
dotnet build
```

to build the solution as a whole or a single project.

Running the Nunit tests can also be done from the cli, just run:

```posh
dotnet test
```

## License

This project's structure and Request handling have been seriously inspired (in part, copied even) by Henrik Fröhling's work on [Trakt.NET](https://github.com/henrikfroehling/Trakt.NET), when it was still called TraktApiSharp. While this project is licensed under the same license as Trakt.NET, I'd still like to make this absolutely clear.

MIT © Marcel Boersma
