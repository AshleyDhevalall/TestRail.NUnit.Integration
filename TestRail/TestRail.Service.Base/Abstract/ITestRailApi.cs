using System.Collections.Generic;
using TestRail.Service.Base.Entities;

namespace TestRail.Service.Base.Abstract
{
    public interface ITestRailApi
    {
        int CreateRun(Run run);
        void AddResultsForCases(int runId, List<Result> results);
    }
}
