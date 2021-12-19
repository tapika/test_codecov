# Overview

This git repo shows different approach of enabling code coverage testing for your application.



I've also collected various links specifying one or another approach.



### Visual studio: Auto detect runsettings file

Visual studio has built-in feature called "Auto Detect runsetting files" - by default Visual Studio will try to load `.runsetting` file from same place where solution file resides. (Based on this [link](https://developercommunity.visualstudio.com/t/auto-detect-runsettings-file-is-not-working/1033850))



### XUnit project set

`Numbers`, `XUnit.Coverlet.Collector`, `XUnit.Coverlet.MSBuild` projects were made based on following article: [Use code coverage for unit testing](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=windows).

#### Override .net core version

By default dotnet will use latest .net core installed, if you however want to override that version - use following command to list available sdks:

`dotnet --list-sdks`

https://docs.microsoft.com/en-us/dotnet/core/install/how-to-detect-installed-versions?pivots=os-windows#check-sdk-versions

And then force specific sdk by creating `global.json` file:

https://docs.microsoft.com/en-us/dotnet/core/versions/selection#the-sdk-uses-the-latest-installed-version





