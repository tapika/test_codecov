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
            return host.Run(args);
        }
    }
}
