using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.MSBuild;
using Cake.Common.Tools.DotNetCore.Test;
using Cake.Common.Tools.ReportGenerator;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Coverlet;
using Cake.Frosting;
using System.IO;

namespace cakebuild
{
    [TaskName(nameof(testCoverletXPlatCollector))]
    public class testCoverletXPlatCollector : FrostingTask<BuildContext>
    {
        ICakeLog log;

        public testCoverletXPlatCollector (ICakeLog _log)
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

            //string solutionPath = Path.Combine(rootDir, "XUnit.Coverage.sln"); ;
            //context.DotNetBuild(solutionPath, new DotNetBuildSettings { Configuration = "Release", NoLogo = true });

            string testResultsDir = System.IO.Path.Combine(rootDir, $@"build\testResults_{nameof(testCoverletXPlatCollector)}");

            if (Directory.Exists(testResultsDir))
            {
                Directory.Delete(testResultsDir, true);
            }

            if (!Directory.Exists(testResultsDir))
            {
                Directory.CreateDirectory(testResultsDir);
            }

            string coverageRunsettings = System.IO.Path.Combine(rootDir, "coverlet.runsettings"); ;
            string projectPath = System.IO.Path.Combine(rootDir, @"XUnit.Coverlet.MSBuild\XUnit.Coverlet.MSBuild.csproj"); ;

            string outPath = System.IO.Path.Combine(rootDir, $@"XUnit.Coverlet.MSBuild\bin\Release\XUnit.Coverlet.MSBuild.dll");

            var testSettings = new DotNetCoreTestSettings
            {
                Settings = coverageRunsettings,
                Collectors = new[] { "XPlat Code Coverage" },
                Configuration = "Release",
                ResultsDirectory = testResultsDir,
            };

            context.DotNetCoreTest(projectPath, testSettings);

            string coverageHtml = System.IO.Path.Combine(testResultsDir, "html");
            LogInfo($"Generating report in {coverageHtml}");
            var repSettings = new ReportGeneratorSettings();
            //repSettings.ReportTypes.Add(ReportGeneratorReportType.HtmlSummary);
            context.ReportGenerator(
                new GlobPattern($@"{testResultsDir}\**\*.xml"), 
                new DirectoryPath(coverageHtml), repSettings);
        }
    }
}
