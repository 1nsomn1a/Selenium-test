using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;

namespace Task9_SortScenario
{
    [TestFixture]
    public class Scenario
    {
        private IWebDriver driver;
        private readonly string _baseUrl = "http://localhost:8080/litecart/admin/";
        private readonly string _countriesUrl = "http://localhost:8080/litecart/admin/?app=countries&doc=countries";
        private readonly string _geoZonesUrl = "http://localhost:8080/litecart/admin/?app=geo_zones&doc=geo_zones";

        [OneTimeSetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }

        [Test]
        public void _Login()
        {
            driver.Url = _baseUrl;
            driver.FindElement(By.CssSelector("[name = username]")).SendKeys("admin");
            driver.FindElement(By.CssSelector("[name = password]")).SendKeys("admin");
            driver.FindElement(By.CssSelector("[name = login]")).Click();
        }

        [Test(Description = "Task9_1a")]
        public void Countries()
        {
            driver.Url = _countriesUrl;
            AreEqual("//tr[@class='row']//a[text()]");
        }

        [Test(Description = "Task9_1b")]
        public void CountryZones()
        {
            driver.Url = _countriesUrl;
            IList<IWebElement> countries = driver.FindElements(By.XPath("//tr[@class='row']"));

            for (int i=0; i < countries.Count; i++)
            {
                string zone = countries[i].FindElement(By.XPath(".//td[6]")).Text;
                if (zone != "0")
                {
                    countries[i].FindElement(By.XPath(".//a[text()]")).Click();

                    AreEqual("//*[@id='table-zones']//td[3][text()]");

                    driver.FindElement(By.XPath("//*[@name='cancel']")).Click();
                    countries = driver.FindElements(By.XPath("//tr[@class='row']"));
                }
            }
        }

        [Test(Description = "Task9_2")]
        public void GeoZones()
        {
            driver.Url = _geoZonesUrl;
            IList<IWebElement> countries = driver.FindElements(By.XPath("//tr[@class='row']//a[not(@title='Edit')]"));

            for(int i=0; i < countries.Count; i++)
            {
                countries[i].Click();

                AreEqual("//select[not(@class='select2-hidden-accessible')]//option[@selected='selected']");

                driver.FindElement(By.XPath("//*[@name='cancel']")).Click();
                countries = driver.FindElements(By.XPath("//tr[@class='row']//a[not(@title='Edit')]"));
            }

        }

        [OneTimeTearDown]
        public void Stop()
        {
            Thread.Sleep(2000);
            driver.Quit();
            driver = null;
        }

        public void AreEqual( string xpath )
        {
            IList<IWebElement> elements = driver.FindElements(By.XPath(xpath));
            List<string> expectedValues = new List<string>();

            foreach (var element in elements)
            {
                expectedValues.Add(element.Text);
            }

            List<string> actualValues = new List<string>(expectedValues);
            actualValues.Sort();

            CollectionAssert.AreEqual(expectedValues, actualValues);
        }
    }
}
