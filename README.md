# DotNetAppBase

Base library for .NET applications, written at .NET Standard runtime.

## Package publishing

Each package workflow can be run manually from the GitHub Actions tab or by pushing a tag in the format `<PackageId>-v<version>`.

When publishing by tag, `<version>` must match the package `Version` evaluated by MSBuild.

Current package version: `3.0.0`.

Tags for publishing all packages:

```text
DotNetAppBase.Std.Exceptions-v3.0.0
DotNetAppBase.Std.Library-v3.0.0
DotNetAppBase.Std.Extensions-v3.0.0
DotNetAppBase.Std.Db-v3.0.0
DotNetAppBase.Std.Db.Work-v3.0.0
DotNetAppBase.Std.Db.SqlServer-v3.0.0
DotNetAppBase.Std.RestClient-v3.0.0
DotNetAppBase.Std.Rmq-v3.0.0
DotNetAppBase.Std.Worker-v3.0.0
```
