using Cake.Common.Tools.ReportGenerator;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Frosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cakebuild
{
    [TaskName(nameof(generateCoverageReport))]
    [IsDependentOn(typeof(testUsingDotNetCoverlet))]
    public class generateCoverageReport : FrostingTask<BuildContext>
    {
        ICakeLog log;

        public generateCoverageReport(ICakeLog _log)
        {
            log = _log;
        }

        public void LogInfo(string value)
        {
            log.Information(value);
        }

        public override void Run(BuildContext context)
        {
            string coverageXml = System.IO.Path.Combine(context.TestResultsDirectory, "coverage.xml");
            string coverageHtml = System.IO.Path.Combine(context.RootDirectory, @"build\html");
            if (Directory.Exists(coverageHtml))
            {
                Directory.Delete(coverageHtml, true);
            }

            LogInfo($"Generating report in {coverageHtml}");
            var repSettings = new ReportGeneratorSettings();
            //repSettings.ReportTypes.Add(ReportGeneratorReportType.HtmlSummary);
            context.ReportGenerator(new GlobPattern(coverageXml), new DirectoryPath(coverageHtml), repSettings);
        }
    }
}
