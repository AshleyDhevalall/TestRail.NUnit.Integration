# TestRail.NUnit.Integration

The goal of this project is to provide automation capabilities to functional testing using NUnit as the testing framework. This project enables you to add the results of tests executed in NUnit to TestRail. If you aren't using TestRail as your Test Case Management tool, this article will not be beneficial.

### Nuget package
[TestRail.Service](https://www.nuget.org/packages/TestRail.Service/)
```
PM> Install-Package TestRail.Service -Version 1.0.0
```

### Prerequisites
* TestRail Server (On-premises / Cloud) 
* Visual Studio >= 2015
* NUnit Framework >= 3.7.1
* TestRail account to access the [TestRail API](http://docs.gurock.com/testrail-api2/start) for updating of test case results. 

### Assumptions
Each test class should map to a TestRail project and test suite.  

### Usage
* Include the TestRail server details in the app.config file.  
```
 <appSettings>
    <add key="testrailurl" value="testrail url" />
    <add key="username" value="username" />
    <add key="password" value="password" />
  </appSettings>
```


* Include a reference to the project.
* Inherit from the TestBase class in each of your test classes.
 
```
public class MyTests : TestBase
{
  ...
}
```

* In your test class include the TestRail project id [projectid] and the suite id [suiteid] as properties to the class.

```
[Property("suiteid", "your suite id here")]
[Property("projectid", "your project id here")]
public class MyTests : TestBase
{
  ...
}
```

* Include the TestRail case id [caseid] as a property to the test method. The steps contained in the test method should map to the test steps in the TestRail test case.

```
[Test, Property("caseid", "your case id here")]
public void GoogleTest()
{
  ..
}
```

* Execute the tests in your test class. 
* Open the project in TestRail and view the test runs. 
* A new test run will be created using the format (Test run - yyyy-MM-dd HH:mm)
* The results were updated by executing the NUnit tests.

## Getting the Source

This project is [hosted on GitHub](https://github.com/AshleyDhevalall/TestRail.NUnit.Integration). You can clone this project directly using this command:

```
git clone https://github.com/AshleyDhevalall/TestRail.NUnit.Integration.git
```

## Troubleshooting

### No test run is created in TestRail after executing NUnit tests?  
 
   - Check if the TestRail server details have been added to the app.config
   - Verify that the TestRail server details are correct.
   - Verify that the project id exists in TestRail and that you have access to the project.
   - Verify that the suite id exists in TestRail under the given project.
   - Verify that the case id's provided on each NUnit test exists under the given suite id.
   
### Properties empty when using parameterized test in NUnit
   Please see https://stackoverflow.com/questions/47434571/nunit-test-properties-not-accessible-in-parametrized-tests
   
To submit bug reports and feature suggestions, or to track changes:
  https://github.com/AshleyDhevalall/TestRail.NUnit.Integration/issues

### Further reading
[Push defect from existing result](https://discuss.gurock.com/t/push-defect-from-existing-result/893)

## Authors

[Ashley Dhevalall](https://github.com/AshleyDhevalall)

## Acknowledgements

* [NUnit](<http://nunit.org>)
* [Testrail Api](<https://github.com/gurock/testrail-api>)

## License

MIT License

Copyright (c) 2019 AshleyDhevalall

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
