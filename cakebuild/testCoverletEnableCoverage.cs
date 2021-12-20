using Cake.Common.Tools.DotNetCore.Test;
using Cake.Common.Tools.ReportGenerator;
using Cake.Core.Diagnostics;
using Cake.Coverlet;
using Cake.Frosting;
using System.IO;

namespace cakebuild
{
    [TaskName(nameof(testCoverletEnableCoverage))]
    public class testCoverletEnableCoverage : FrostingTask<BuildContext>
    {
        ICakeLog log;

        public testCoverletEnableCoverage(ICakeLog _log)
        {
            log = _log;
        }

        public void LogInfo(string value)
        {
            log.Information(value);
        }

        public override void Run(BuildContext context)
        {
            string rootDir = context.RootDirectory;
            string testResultsDir = System.IO.Path.Combine(rootDir, $@"build\testResults_{nameof(testCoverletEnableCoverage)}");

            if (Directory.Exists(testResultsDir))
            {
                Directory.Delete(testResultsDir, true);
            }

            if (!Directory.Exists(testResultsDir))
            {
                Directory.CreateDirectory(testResultsDir);
            }

            string coverageRunsettings = Path.Combine(rootDir, ".runsettings"); ;
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

            context.DotNetCoreTest(projectPath, testSettings, coverletSettings);

            string coverageHtml = System.IO.Path.Combine(testResultsDir, "html");
            if (Directory.Exists(coverageHtml))
            {
                Directory.Delete(coverageHtml, true);
            }

            string coverageXml = System.IO.Path.Combine(testResultsDir, "coverage.xml");

            LogInfo($"Generating report in {coverageHtml}");
            var repSettings = new ReportGeneratorSettings();
            //repSettings.ReportTypes.Add(ReportGeneratorReportType.HtmlSummary);
            context.ReportGenerator(
                new Cake.Core.IO.GlobPattern(coverageXml), 
                new Cake.Core.IO.DirectoryPath(coverageHtml), repSettings);
        }
    }
}
