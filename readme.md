## Overview

This git repo shows different approach of enabling code coverage testing for your application.

I've also collected various links specifying one or another approach.



### Visual studio: Auto detect runsettings file

Visual studio has built-in feature called "Auto Detect runsetting files" - by default Visual Studio will try to load `.runsetting` file from same place where solution file resides. (Based on this [link](https://developercommunity.visualstudio.com/t/auto-detect-runsettings-file-is-not-working/1033850))



### Methods to enable code coverage in Visual Studio

There are various methods enabling code coverage in Visual Studio - I will list them with known problems and limitations.

1. Coverlet.MsBuild (How to make it - [link](#xunit-project-set))

According to coverlet git has some issues, see [Known Issues](https://github.com/coverlet-coverage/coverlet/blob/master/Documentation/KnownIssues.md).

Cake integration - see `testCoverletEnableCoverage`.



2. Coverlet , VsTest integration - see [link](https://github.com/coverlet-coverage/coverlet/blob/master/Documentation/VSTestIntegration.md).

Cake integration - see `testCoverletXPlatCollector`.

Code coverage is configured via `coverlet.runsettings`.



### Referenced / used documentation

##### XUnit project set

`Numbers`, `XUnit.Coverlet.Collector`, `XUnit.Coverlet.MSBuild` projects were made based on following article: [Use code coverage for unit testing](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=windows).

##### Override .net core version

By default dotnet will use latest .net core installed, if you however want to override that version - use following command to list available sdks:

`dotnet --list-sdks`

https://docs.microsoft.com/en-us/dotnet/core/install/how-to-detect-installed-versions?pivots=os-windows#check-sdk-versions

And then force specific sdk by creating `global.json` file:

https://docs.microsoft.com/en-us/dotnet/core/versions/selection#the-sdk-uses-the-latest-installed-version

##### Cake build system

Cake build system was added using following commands:

```
dotnet new console -n cakebuild
dotnet add cakebuild/cakebuild.csproj package Cake.Common
dotnet add cakebuild/cakebuild.csproj package Cake.Frosting
dotnet add cakebuild/cakebuild.csproj package Cake.Coverlet
```

And after that some manual coding.

