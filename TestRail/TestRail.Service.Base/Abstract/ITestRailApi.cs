using System.Collections.Generic;
using TestRail.Service.Base.Entities;

namespace TestRail.Service.Base.Abstract
{
    public interface ITestRailApi
    {
        int CreateRun(Run run);
       bool AddResultsForCases(int runId, List<Result> results);
    }
}
