# PVOutput.Net

> A .NET wrapper library for API of the popular [PVOutput](https://pvoutput.org) service.
> PVOutput is a free service for sharing and comparing PV output data.

## Installation

Installation can be done through installation of the [NuGet package](https://www.nuget.org/packages/PVOutput.Net/).

## Usage

At this time, most of the API calls of the official service are exposed through the library. For a complete list of the supported calls, see the [coverage](articles/coverage.md) page.

### Getting data out of PVOutput.org

```csharp
var client = new PVOutputClient(apiKey: "myPvOutputKey", systemId: 1);

// Request output for today
var outputResponse = await client.Output.GetOutputForDateAsync(DateTime.Today);
var output = outputResponse.Value;
Console.WriteLine($"Output for date {output.OutputDate.ToShortDateString()}, {output.EnergyGenerated} Wh generated");
```

### Adding data to a system in PVOutput.org

```csharp
var client = new PVOutputClient(apiKey: "myPvOutputKey", systemId: 1);
var builder = new StatusPostBuilder<IStatusPost>();

// Build the status
var status = builder.SetTimeStamp(DateTime.Now)
                .SetGeneration(200, null)
                .Build();

// Push the status back to PVOutput
var response = await client.Status.AddStatusAsync(status);
```

See the [API reference](api/PVOutput.Net.yml) for details on all the classes provided by this library.

## Contribute

See [Contributing](https://github.com/pyrocumulus/pvoutput.net/blob/develop/CONTRIBUTING.md) on how you can contribute to this project.
