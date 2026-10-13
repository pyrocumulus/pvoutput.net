# CLAUDE.md

This file gives Claude Code guidance for working in this repository.

## Project

PVOutput.Net is a .NET wrapper library around the [PVOutput.org](https://pvoutput.org) API, published to NuGet as `PVOutput.Net`.

- `src/PVOutput.Net`: the library. Targets `netstandard2.0;net8.0;net10.0`. PolySharp polyfills newer language features for `netstandard2.0`.
- `tests/PVOutput.Net.Tests`: NUnit 4 tests, NSubstitute and RichardSzalay.MockHttp. Targets `net8.0;net10.0`.
- `docfx/`: documentation site (published to GitHub Pages).

## Branches

- `develop` is the work branch. Branch from it and open PRs against it.
- `main` is the release branch. Do not target it for regular work.

## Build and test

```bash
dotnet build --configuration Release
dotnet test
dotnet test --framework net8.0 -p:LibraryTargetFramework=netstandard2.0
```

The last command runs the net8.0 tests against the `netstandard2.0` build of the library (see the conditional `ProjectReference` in the test csproj). CI (`.github/workflows/dotnet-build.yml`) runs all three; run all three locally before calling a change done.

Run a single test: `dotnet test --filter "FullyQualifiedName~StatusServiceTests"`.

Debug build output goes to `artifacts/`, Release to `artifacts/release/`.

## Code rules

- Nullable is enabled and nullable warnings are errors. `AnalysisMode` is `AllEnabledByDefault` with NetAnalyzers; do not suppress warnings without good reason.
- Library `LangVersion` is 12; the test project uses 13.
- Code must compile and behave the same on `netstandard2.0`. Avoid APIs missing there unless guarded or polyfilled.
- Argument validation uses `CommunityToolkit.Diagnostics` (`Guard.*`) plus `Objects/Core/GuardExtensions.cs`.
- Follow `.editorconfig` and the Framework Design Guidelines.
- Follow Test Driven Development for all work.

## Architecture

- `PVOutputClient` exposes one service per API area (`Modules/*Service.cs`, interfaces in `Modules/Interfaces`).
- A service builds a request object (`Requests/Modules`, based on `GetRequest`/`PostRequest` in `Requests/Base`) and executes it through `Requests/Handler/RequestHandler.cs`.
- Responses are plain text. `Objects/Factories` and the string readers in `Objects/Modules/Readers` and `Objects/Core` parse them into the public `I*` object interfaces in `Objects`. Implementations are internal.
- Builders in `Builders/` create post objects (status, output, batches).
- `DependencyInjection/` holds the `IServiceCollection` extensions.

Tests mirror this layout under `tests/PVOutput.Net.Tests/Modules/<Area>`, with mocked HTTP responses in `*TestsData.cs` files and helpers in `Utils/`.

## Workflow

- Work test-first: write a failing test that shows the bug or the new behavior, confirm it fails, then make it pass.
- Bug fixes and new features need a test. Docs-only changes do not.

## CHANGELOG

Add entries under `## [Unreleased]` in `CHANGELOG.md` (Keep a Changelog format):

- Consumer-visible breaking changes get a line starting with `- **BREAKING**:`, saying what changed and how consumers are affected.
- Each package bump in `src/` gets one line: ``- Updated `Package.Name` from `vX` to `vY` ``. Test-only and tooling bumps are not listed.
- Link the PR or issue where there is one.
