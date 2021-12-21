using Cake.Frosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cakebuild
{
    class Program
    {
        static int Main(string[] _args)
        {
            var host = new CakeHost();
            host.UseContext<BuildContext>();
            //host.InstallTool(new Uri("dotnet:?package=coverlet.console&version=3.1.0"));
            // Needed for testVsTest
            host.InstallTool(new Uri("nuget:?package=Microsoft.CodeCoverage&version=17.0.0"));
            // Needed for all tools
            host.InstallTool(new Uri("nuget:?package=ReportGenerator&version=5.0.0"));
            List<String> args = _args.ToList();

            if (args.Contains("--show"))
            {
                args.Remove("--show");
                args.Add("--Settings_ShowProcessCommandLine");
                args.Add("true");
            }

            return host.Run(args.ToArray());
        }
    }
}
