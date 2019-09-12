using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Support.UI;

namespace LoginScenario
{
    [TestFixture]
    public class LogScenario
    {
        private IWebDriver driver;
        private readonly string _baseUrl = "http://localhost:8080/litecart/admin/";

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void Login()
        {
            driver.Url = _baseUrl;
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            string currentUrl = driver.Url;
            Assert.AreEqual(_baseUrl, currentUrl);
        }

        [TearDown]
        public void Stop()
        {
            Thread.Sleep(2000);
            driver.Quit();
            driver = null;
        }
    }
}
