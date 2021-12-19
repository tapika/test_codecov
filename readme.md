# Overview

This git repo shows different approach of enabling code coverage testing for your application.



I've also collected various links specifying one or another approach.



### Visual studio: Auto detect runsettings file

Visual studio has built-in feature called "Auto Detect runsetting files" - by default Visual Studio will try to load `.runsetting` file from same place where solution file resides. (Based on this [link](https://developercommunity.visualstudio.com/t/auto-detect-runsettings-file-is-not-working/1033850))



### Use code coverage for unit testing

`Numbers`, `XUnit.Coverlet.Collector`, `XUnit.Coverlet.MSBuild` projects were made based on following article:

https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=windows



