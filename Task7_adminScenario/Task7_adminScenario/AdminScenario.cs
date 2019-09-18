using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Collections.Generic;


namespace Task7_adminScenario
{
    [TestFixture]
    public class AdminScenario
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
            driver.FindElement(By.CssSelector("[name = username]")).SendKeys("admin");
            driver.FindElement(By.CssSelector("[name = password]")).SendKeys("admin");
            driver.FindElement(By.CssSelector("[name = login]")).Click();

            IList<IWebElement> elements = driver.FindElements(By.XPath("//div[@id='box-apps-menu-wrapper']//li"));

            for (int i = 1; i <= elements.Count; i++)
            {
                driver.FindElement(By.XPath($"//div[@id='box-apps-menu-wrapper']//li[@id='app-'][{i}]")).Click();
                Assert.That(driver.FindElements(By.XPath("//h1")).Count, Is.GreaterThan(0));

                if (driver.FindElements(By.XPath("//li[@class='selected']//ul")).Count != 0)
                {
                    IList<IWebElement> el = driver.FindElements(By.XPath("//li[@class='selected']//li[not(@id='app-')]"));

                    for (int j = 2; j <= el.Count; j++)
                    {
                        driver.FindElement(By.XPath($"//li[@class='selected']//li[not(@id='app-')][{j}]")).Click();
                        Assert.That(driver.FindElements(By.XPath("//h1")).Count, Is.GreaterThan(0));
                    }                        
                }
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
