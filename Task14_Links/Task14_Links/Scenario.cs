using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;

namespace Task14_Links
{
    [TestFixture]
    public class Scenario
    {
        private IWebDriver driver;
        private readonly string _adminMainPage = "http://localhost:8080/litecart/admin/";

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver
            {
                Url = _adminMainPage
            };
            driver.FindElement(By.CssSelector("[name = username]")).SendKeys("admin");
            driver.FindElement(By.CssSelector("[name = password]")).SendKeys("admin");
            driver.FindElement(By.CssSelector("[name = login]")).Click();
        }
       
        [Test]
        public void Test()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            //driver.Url = _countriesPage;
            driver.FindElement(By.XPath("//*[text()='Countries']")).Click();
            driver.FindElement(By.XPath("//*[text()=' Add New Country']")).Click();

            string mainWindow = driver.CurrentWindowHandle;
            ICollection<string> oldWindows = driver.WindowHandles;

            IList<IWebElement> links = driver.FindElements(By.XPath("//*[@class='fa fa-external-link']"));
            foreach(var link in links)
            {
                link.Click();
                string newWindow = wait.Until(d => d.WindowHandles.Where(x => !oldWindows.Any(y => y == x)).FirstOrDefault());
                driver.SwitchTo().Window(newWindow);
                driver.Close();
                driver.SwitchTo().Window(mainWindow);
            }
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
