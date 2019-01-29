using Gurock.TestRail;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using TestRail.Service.Base.Abstract;
using TestRail.Service.Base.Entities;

namespace TestRail.Service.Base.Concrete
{
    public class TestRailApi : ITestRailApi
    {        
        private APIClient _apiClient;

        public TestRailApi(string testrailUrl, string userName, string password)
        {
            _apiClient = new APIClient(testrailUrl);
            _apiClient.User = userName;
            _apiClient.Password = password;
        }

        public int CreateRun(Run run)
        {
            try
            {
                var project = ReadProject(run.project_id);
                var suite = ReadSuite(run.suite_id);

                if ((project == null) && (suite == null)) return 0;
                
                var data = new Dictionary<string, object>
                {
                    { "suite_id", run.suite_id },
                    { "name", string.IsNullOrEmpty(run.name) ? GetDefaultTestRunName() :  run.name },
                    { "description", run.description },
                    { "include_all", run.include_all }
                };

                var result = (JObject)_apiClient.SendPost("add_run/" + run.project_id, data);
                if (result == null) return 0;

                var createRunResponse = JsonConvert.DeserializeObject<CreateRunResponse>(result.ToString());
                return createRunResponse.id;
            }
            catch
            {
                return 0;
            }
        }

        public void AddResultsForCases(int runId, List<Result> results)
        {
            var result = false;
            var caseIds = results.Select(x => x.case_id).ToList();

            if (UpdateTestRun(runId, caseIds))
            {
                try
                {
                    var testResult = new Dictionary<string, object>();
                    testResult["results"] = results;
                    var addResultUrl = "add_results_for_cases/" + runId;

                    var response = (JArray)_apiClient.SendPost(addResultUrl, testResult);
                    if (response == null) result = true;
                }
                catch
                {
                    //output result to nunit output here
                    Console.WriteLine("Failed to add results to TestRail");
                }
            }
        }

        private bool UpdateTestRun(int runId, List<int> ids)
        {
            try
            {
                var testToAdd = new Dictionary<string, object>();
                testToAdd["case_ids"] = ids;
                var updateRunUrl = "update_run/" + runId;
                _apiClient.SendPost(updateRunUrl, testToAdd);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private Project ReadProject(int projectId)
        {
            try
            {
                var result = (JObject)_apiClient.SendGet("get_project/" + projectId);
                if (result == null) return null;

                var project = JsonConvert.DeserializeObject<Project>(result.ToString());
                return project;
            }
            catch
            {
                return null;
            }
        }

        private Suite ReadSuite(int suiteId)
        {
            try
            {
                var result = (JObject)_apiClient.SendGet("get_suite/" + suiteId);
                if (result == null) return null;

                var suite = JsonConvert.DeserializeObject<Suite>(result.ToString());
                return suite;
            }
            catch
            {
                return null;
            }
        }

        private string GetDefaultTestRunName()
        {
            DateTime dateValue = DateTime.Now;
            return "Test run - " + (dateValue.ToString("yyyy-MM-dd HH:mm"));
        }
    }
}
