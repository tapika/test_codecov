using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.DotNetCore.Test;
using Cake.Common.Tools.ReportGenerator;
using Cake.Common.Tools.VSTest;
using Cake.Core.Diagnostics;
using Cake.Core.IO.Arguments;
using Cake.Coverlet;
using Cake.Frosting;
using cakebuild.CodeCoverageTool;
using System.IO;
using System.Linq;

namespace cakebuild
{
    [TaskName(nameof(testVsTest))]
    public class testVsTest : FrostingTask<BuildContext>
    {
        ICakeLog log;

        public testVsTest(ICakeLog _log)
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
            string testResultsDir = System.IO.Path.Combine(rootDir, $@"build\testResults_{nameof(testVsTest)}");

            // Directory must be cleaned and re-created, as *.coverage filename changes everytime
            if (Directory.Exists(testResultsDir)) Directory.Delete(testResultsDir, true);
            if (!Directory.Exists(testResultsDir)) Directory.CreateDirectory(testResultsDir);

            string project2build = "XUnit.VsTest";

            string coverageRunsettings = Path.Combine(rootDir, ".runsettings"); ;

            string solutionPath = Path.Combine(rootDir, "XUnit.Coverage.sln"); ;
            context.DotNetBuild(solutionPath, new DotNetBuildSettings { Configuration = "Release", NoLogo = true });

            string outPath = System.IO.Path.Combine(rootDir, $@"{project2build}\bin\Release\{project2build}.dll");

            var settings = new VSTestSettings
            {
                WorkingDirectory = testResultsDir,
                //ArgumentCustomization = args =>
                //{
                //    args.Append(new TextArgument($"/help"));
                //    return args;
                //},
                ResultsDirectory = testResultsDir,
                EnableCodeCoverage = true,
                SettingsFile = coverageRunsettings,
            };

            var codeCoveragePath = context.Tools.Resolve("CodeCoverage.exe");

            context.VSTest(
                new string[] { outPath }.Select(x => new Cake.Core.IO.FilePath(x)),
                settings
            );

            string coveragePath = Directory.GetFiles(testResultsDir, "*.coverage", SearchOption.AllDirectories).First();
            string coverageXml = Path.Combine(testResultsDir, "cobertura_coverage.xml");

            LogInfo($"Converting *.coverage to *.cobertura_coverage.xml file format...");
            context.ConvertCoverageReport(coveragePath, coverageXml);

            string coverageHtml = System.IO.Path.Combine(testResultsDir, "html");

            LogInfo($"Generating report in {coverageHtml}");

            context.ReportGenerator(
                new Cake.Core.IO.GlobPattern(coverageXml),
                new Cake.Core.IO.DirectoryPath(coverageHtml), new ReportGeneratorSettings());
        }
    }
}
