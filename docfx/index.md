# PVOutput.Net

> A .NET wrapper library for API of the popular [PVOutput](https://pvoutput.org) service.
> PVOutput is a free service for sharing and comparing PV output data.

## Installation

Installation can be done through installation of the [NuGet package](https://www.nuget.org/packages/PVOutput.Net/):

```posh
PM> Install-Package PVOutput.Net
```

## Changelog

See [Changelog](https://github.com/pyrocumulus/pvoutput.net/blob/main/CHANGELOG.md) for information on changes per version.To see coming but unreleased changes, see the `develop` branch.

## Support

This library is targeting .NET Standard 2.0 and above. For full compatibility details, check the [Microsoft Docs](https://docs.microsoft.com/nl-nl/dotnet/standard/net-standard#net-implementation-support).

## Usage

As of 0.8.0, all of the public API calls of the official service are exposed through the library. For a complete list of the supported calls, see the [coverage](articles/coverage.md) page.
Note that the 'Managed Systems' and 'Data Services' APIs are both unsupported as those are not public.

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

### How to log calls made by the library

The client also supports logging through the standard .NET Core ILogger interface.
In a web app or hosted service you can supply the `ILogger` through dependency injection (DI).
For non-host console applications, use the `LoggerFactory` to instantiate the logger and then provide it to the relevant constructor overload.

See the official [Logging in .NET Core and ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-3.1) for more information.

The various logging levels:

- **Information**: this will only the success state of requests that are made with the used service and parameters; including errors.
- **Debug**: also log the exact requested uri (including query parameters), and api rate information.
- **Trace**: also log 'raw' response content, at a minimal overhead (duplicating memory streams).

**NOTE**:

Your API key and owned system ID are always sent through headers, not the request uri. None of the logging levels also log headers and never will, so logging information should never contain those two aspects. However it is always good to inspect logs before sharing them with anyone, including the maintainer of this project.

### API reference

See the [API reference](api/PVOutput.Net.yml) for details on all the classes provided by this library.
