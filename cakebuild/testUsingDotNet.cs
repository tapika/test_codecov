using Cake.Common.Tools.DotNetCore.Test;
using Cake.Coverlet;
using Cake.Frosting;
using System.IO;

namespace cakebuild
{
    [TaskName(nameof(testUsingDotNet))]
    public class testUsingDotNet : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            string rootDir = context.RootDirectory;
            string testResultsDir = context.TestResultsDirectory;
            string coverageRunsettings = Path.Combine(rootDir, ".runsettings"); ;
            //string projectPath = Path.Combine(rootDir, @"XUnit.Coverlet.Collector\XUnit.Coverlet.Collector.csproj"); ;
            string projectPath = Path.Combine(rootDir, @"XUnit.Coverlet.MSBuild\XUnit.Coverlet.MSBuild.csproj"); ;

            var coverletSettings = new CoverletSettings
            {
                CollectCoverage = true,
                CoverletOutputFormat = CoverletOutputFormat.opencover,
                CoverletOutputDirectory = testResultsDir,
                CoverletOutputName = "coverage.xml"
            };

            var testSettings = new DotNetCoreTestSettings
            {
                //NoBuild = true,
                Settings = coverageRunsettings,
                ArgumentCustomization = args =>
                {
                    //args.Append(new TextArgument("/p:SolutionName=XUnit.Coverage"));
                    //args.Append(new TextArgument($"/p:SolutionDir={rootDir}\\src\\"));
                    return args;
                },
                Configuration = "Release",
                ResultsDirectory = testResultsDir,
                //Verbosity = DotNetCoreVerbosity.Detailed,
                //OutputDirectory - is from where to take dll.
                //OutputDirectory = testResultsDir
            };

            if (!Directory.Exists(testResultsDir))
            {
                Directory.CreateDirectory(testResultsDir);
            }

            context.DotNetCoreTest(projectPath, testSettings, coverletSettings);
        }
    }
}
