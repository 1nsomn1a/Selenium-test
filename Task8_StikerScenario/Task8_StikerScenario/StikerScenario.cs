using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;

namespace Task8_StikerScenario
{
    [TestFixture]
    public class StikerScenario
    {
        private IWebDriver driver;
        private readonly string _baseUrl = "http://localhost/litecart/";

        [OneTimeSetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            driver.Url = _baseUrl;
        }

        [Test]
        public void test()
        {
            IList<IWebElement> products = driver.FindElements(By.XPath("//li[contains(@class, 'product')]"));

            foreach (IWebElement element in products)
            {
                Assert.AreEqual(1, element.FindElements(By.XPath(".//*[contains(@class, 'sticker')]")).Count);
            }
        }

        [OneTimeTearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
