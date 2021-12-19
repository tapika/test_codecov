using Cake.Core;
using Cake.Frosting;
using System.IO;
using System.Reflection;

namespace cakebuild
{
    public class BuildContext : FrostingContext
    {
        public BuildContext(ICakeContext context) : base(context)
        {
        }

        public string RootDirectory
        {
            get
            {
                string rootDir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"..\..\.."));
                return rootDir;
            }
        }
    }
}
