# DotNetAppBase
 Base library for .NET applications, written at .NET Standard runtime.

## Package publishing

Each package workflow can be run manually from the GitHub Actions tab or by pushing a tag in the format `<PackageId>-v<version>`.

When publishing by tag, `<version>` must match the package `Version` evaluated by MSBuild.

Example:

```text
DotNetAppBase.Std.Library-v1.3.12
```
