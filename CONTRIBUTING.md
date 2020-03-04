# Contributing to PVOutput.Net

One of the easiest ways to contribute is to test the library and add issues when you find them. The PVOutput API is quite extensive and has a lot of options and combinations to test. All calls have a certain amount of unit testing applied to them and the basic API calls have been tested with actual API calls. That said, PVOutput sometimes exibits strange behaviour, even for legal calls, so unit testing results cannot solely be relied upon.

You can obviously also contribute by submitting pull requests with code changes into **`develop`** branch. This project has adopted the code of conduct defined by the [Contributor Covenant](http://contributor-covenant.org/) to clarify the expected behavior should you want to contribute.

## Filing issues

The best way to get your bug fixed is to be as detailed as you can be about the problem. At minimum include the version of the library you are using. Obviously include a detailed reproduction of the problem you are facing. If certain properties of the PV system you are using are important, share those too.

**DO BEWARE** to not leak your `PVOutput API key` when including reproductions! Leaking your `SystemID` could hurt your privacy but cannot easily be abused.
Most issues can probably do without either of them though, so it's best to leave them out.

GitHub supports [markdown](https://guides.github.com/features/mastering-markdown/), so when filing bugs make sure you check the formatting before clicking submit.

## Contributing code

- The project adheres to [Semver 2.0](https://semver.org/) for versioning.
- The source code contains an `.editorconfig` file. Please make sure the IDE of your choosing applies it when developing.
- Apart from just proper clean code practises, this project also uses [Microsoft.CodeAnalysis.FxCopAnalyzers](https://www.nuget.org/packages/Microsoft.CodeAnalysis.FxCopAnalyzers/) to enforce Microsoft's coding guidelines. Please do not supress warnings without good reason.
- Finally this project tries to apply the guidelines found in the [Open-source library guidance](https://docs.microsoft.com/en-us/dotnet/standard/library-guidance/) and [Framework Design Guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/index) also set by Microsoft.

### Commit/Pull Request Format

```plaintext
Summary of the changes (Less than 80 chars)
 - Detail 1
 - Detail 2

#bugnumber (in this specific format)
```

### Tests

- New features should come with unit tests, proving the feature works.
- Bug fixes should come with a unit test, proving the fix succeeded.
- Tests only need to be present for issues that include changes to source code, not for assets like documentation.
- If there is a scenario that is far too hard to test there does not need to be a test for it.
