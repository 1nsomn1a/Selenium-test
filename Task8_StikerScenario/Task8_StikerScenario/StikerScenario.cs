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
        private readonly string _baseUrl = "http://localhost:8080/litecart/";

        private const string _mostPopularSection = "//*[@id='box-most-popular']//li";
        private const string _campaignSection = "//*[@id='box-campaigns']//li";
        private const string _latestProductsSection = "//*[@id='box-latest-products']//li";

        [OneTimeSetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            driver.Url = _baseUrl;
        }

        [TestCase(_mostPopularSection, TestName = "Most Popular section")]
        [TestCase(_campaignSection, TestName = "Campaigns section")]
        [TestCase(_latestProductsSection, TestName = "Latest products section")]
        public void SectionTest(string section)
        {
            IList<IWebElement> elements = driver.FindElements(By.XPath(section));

            if (elements.Count != 0)
            {
                for (int i = 1; i <= elements.Count; i++)
                {
                    Assert.AreEqual(1, driver.FindElements(By.XPath($"{section}[{i}]//div[contains(@class, 'sticker')]")).Count);
                }
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
