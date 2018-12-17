using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using TestRail.Service.Base.Concrete;
using TestRail.Service.Base.Entities;

public class TestBase
{
    private TestContext _fixtureContext;
    private TestRailApi _testRailApi;
    private string _suiteid, _projectid;
    private bool IgnoreAddResults = false;
    private int _projectIdInt, _suiteIdInt, _caseId;
    private List<Result> _resultsForCases;
    public IWebDriver Driver { get; set; }

    [OneTimeSetUp]
    public void FixtureSetup()
    {
        try
        {
            _fixtureContext = TestContext.CurrentContext;
            InitTestRailConfig();
            ValidateSuiteIdAndProjectId();
            _resultsForCases = new List<Result>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            IgnoreAddResults = true;
        }
    }

    [OneTimeTearDown]
    public void FixtureTearDown()
    {
        if (!IgnoreAddResults)
        {
            if (_resultsForCases.Count > 0)
            {
                var runId = _testRailApi.CreateRun(new Run { project_id = _projectIdInt, suite_id = _suiteIdInt, include_all = false });
                if (runId > 0) _testRailApi.AddResultsForCases(runId, _resultsForCases);
            }
        }
    }

    [TearDown]
    public void Cleanup()
    {
        if (!IgnoreAddResults)
        {
            var caseid = TestContext.CurrentContext.Test.Properties.Get("caseid")?.ToString();
            if (Int32.TryParse(caseid, out _caseId))
            {
                var result = new Result { case_id = _caseId, comment = TestContext.CurrentContext.Result.Message };
                var resultState = TestContext.CurrentContext.Result.Outcome;

                if (resultState == ResultState.Success) result.status_id = 1;
                else if (resultState == ResultState.Inconclusive) result.status_id = 4;
                else result.status_id = 5;

                _resultsForCases.Add(result);
            }
        }
    }

    private void InitTestRailConfig()
    {
        var testrailurl = ConfigurationManager.AppSettings["testrailurl"];
        var username = ConfigurationManager.AppSettings["username"];
        var password = ConfigurationManager.AppSettings["password"];

        if (string.IsNullOrEmpty(testrailurl)) throw new Exception("Invalid testrail url");
        if (string.IsNullOrEmpty(username)) throw new Exception("Invalid testrail username");
        if (string.IsNullOrEmpty(password)) throw new Exception("Invalid testrail password");

        _testRailApi = new TestRailApi(testrailurl, username, password);
    }

    private void ValidateSuiteIdAndProjectId()
    {
        _suiteid = _fixtureContext.Test.Properties.Get("suiteid")?.ToString();
        _projectid = _fixtureContext.Test.Properties.Get("projectid")?.ToString();

        if (string.IsNullOrEmpty(_suiteid)) throw new Exception("Invalid suite id");
        if (string.IsNullOrEmpty(_projectid)) throw new Exception("Invalid project id");

        if (!Int32.TryParse(_projectid, out _projectIdInt)) throw new Exception("Project id not valid int");
        if (!Int32.TryParse(_suiteid, out _suiteIdInt)) throw new Exception("Suite id not valid int");
    }
}