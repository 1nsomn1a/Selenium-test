using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;

namespace Task11_RegScenario
{
    [TestFixture]
    public class Scenario
    {
        private IWebDriver driver;
        private readonly string _MainPage = "http://localhost:8080/litecart/en/";
        private readonly string _password = "password123";

        [OneTimeSetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void SignUp()
        {
            driver.Url = _MainPage;
            string email = EmailAddress();
            //string password = "password123";

            driver.FindElement(By.XPath("//tbody//a")).Click();

            driver.FindElement(By.XPath("//*[@name='firstname']")).SendKeys("Arnold");
            driver.FindElement(By.XPath("//*[@name='lastname']")).SendKeys("Schwarzenegger");
            driver.FindElement(By.XPath("//*[@name='address1']")).SendKeys("RandomStreet str.");
            driver.FindElement(By.XPath("//*[@name='postcode']")).SendKeys("12345");
            driver.FindElement(By.XPath("//*[@name='city']")).SendKeys("Thal");

            driver.FindElement(By.XPath("//*[@class='select2 select2-container select2-container--default']")).Click();
            driver.FindElement(By.XPath("//*[@class='select2-search__field']")).SendKeys("united states");
            driver.FindElement(By.XPath("//*[@class='select2-results__options']/li[1]")).Click();


            driver.FindElement(By.XPath("//*[@name='email']")).SendKeys(email);
            driver.FindElement(By.XPath("//*[@name='phone']")).SendKeys("+235294583498");
            driver.FindElement(By.XPath("//*[@name='password']")).SendKeys(_password);
            driver.FindElement(By.XPath("//*[@name='confirmed_password']")).SendKeys(_password);
            driver.FindElement(By.XPath("//*[@name='create_account']")).Click();

            driver.FindElement(By.XPath("//*[@id='box-account']//ul/li[4]/a")).Click();

            driver.FindElement(By.XPath("//*[@name='email']")).SendKeys(email);
            driver.FindElement(By.XPath("//*[@name='password']")).SendKeys(password);
            driver.FindElement(By.XPath("//*[@name='login']")).Click();

            driver.FindElement(By.XPath("//*[@id='box-account']//ul/li[4]/a")).Click();
        }

        [OneTimeTearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }

        public string EmailAddress()
        {
            var characters = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            var random = new Random();
            var EmailAddress = new string(Enumerable.Repeat(characters, 10).Select(s => s[random.Next(s.Length)]).ToArray());
            EmailAddress = EmailAddress + "@test123.com";
            return EmailAddress;
        }
    }
}
