using NUnit.Framework;
using OpenQA.Selenium;

namespace TestRail.NUnit.Tests
{
    [Property("suiteid", "your suite id here")]
    [Property("projectid", "your project id here")]
    public class Tests_1 : Hooks
    {
        [Test, Property("caseid", "your case id here")]
        public void GoogleTest()
        {
            Driver.Navigate().GoToUrl("http://www.google.com");
            Driver.FindElement(By.Name("q")).SendKeys("Selenium");
            System.Threading.Thread.Sleep(5000);
            Driver.FindElement(By.Name("btnK")).Click();
            Assert.AreEqual(true, Driver.FindElement(By.Id("btnN")).Displayed);
            Assert.That(Driver.PageSource.Contains("Selenium"), Is.EqualTo(true), "The text selenium doest not exist");
        }

        [Test, Property("caseid", "your case id here")]
        public void ExecuteAutomationTest()
        {
            Driver.Navigate().GoToUrl("http://executeautomation.com/demosite/Login.html");
            Driver.FindElement(By.Name("UserName")).SendKeys("admin");
            Driver.FindElement(By.Name("Password")).SendKeys("admin");
            Driver.FindElement(By.Name("Login")).Submit();
            System.Threading.Thread.Sleep(2000);
            Assert.That(Driver.PageSource.Contains("Selenium"), Is.EqualTo(true), "The text selenium doest not exist");
        }
    }    
}
