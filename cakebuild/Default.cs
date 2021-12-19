using Cake.Frosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace cakebuild
{
    [TaskName(nameof(Default))]
    [IsDependentOn(typeof(generateCoverageReport))]
    public class Default : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
        }
    }
}
