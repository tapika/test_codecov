using Cake.Frosting;
using System;

namespace cakebuild
{
    class Program
    {
        static int Main(string[] args)
        {
            var host = new CakeHost();
            host.UseContext<BuildContext>();
            host.InstallTool(new Uri("nuget:?package=ReportGenerator&version=5.0.0"));
            return host.Run(args);
        }
    }
}
