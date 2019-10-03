using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Task12_NewProduct
{
    [TestFixture]
    public class AddNewProduct
    {
        private IWebDriver driver;
        private readonly string _adminPage = "http://localhost:8080/litecart/admin/";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.Url = _adminPage;
            driver.FindElement(By.CssSelector("[name = username]")).SendKeys("admin");
            driver.FindElement(By.CssSelector("[name = password]")).SendKeys("admin");
            driver.FindElement(By.CssSelector("[name = login]")).Click();
        }

        [Test]
        public void Test1()
        {
            string productName = ProductName();

            driver.FindElement(By.XPath("//*[text()='Catalog']")).Click();
            driver.FindElement(By.XPath("//a[@class='button'][2]")).Click();

            driver.FindElement(By.XPath("//input[@type='radio'][@value='1']")).Click();
            driver.FindElement(By.XPath("//input[@name='name[en]']")).SendKeys(productName);
            driver.FindElement(By.XPath("//input[@value='1-3']")).Click();
            driver.FindElement(By.XPath("//input[@name='quantity']")).SendKeys("2.00");
            var absolute = Path.GetFullPath(@"..\..\..\..\JPG.jpg");
            driver.FindElement(By.XPath("//*[@name='new_images[]']")).SendKeys(absolute);
            driver.FindElement(By.XPath("//input[@name='date_valid_from']")).SendKeys("2019-09-01");
            driver.FindElement(By.XPath("//input[@name='date_valid_to']")).SendKeys("2019-11-01");

            driver.FindElement(By.XPath("//a[text()='Information']")).Click();
            driver.FindElement(By.XPath("//*[@name='manufacturer_id']/option[2]")).Click();
            driver.FindElement(By.XPath("//*[@name='keywords']")).SendKeys("testKeyword");
            driver.FindElement(By.XPath("//*[@name='short_description[en]']")).SendKeys("This is a short description of the product");
            driver.FindElement(By.XPath("//*[@class='trumbowyg-editor']")).SendKeys("This is a full description of the product. \\nCan't say nothing more about it.");
            driver.FindElement(By.XPath("//*[@name='head_title[en]']")).SendKeys("Test Product");
            driver.FindElement(By.XPath("//*[@name='meta_description[en]']")).SendKeys("*Meta Description*");

            driver.FindElement(By.XPath("//a[text()='Prices']")).Click();
            driver.FindElement(By.XPath("//*[@name='purchase_price']")).SendKeys("999");
            driver.FindElement(By.XPath("//*[@value='USD']")).Click();
            driver.FindElement(By.XPath("//*[@name='prices[USD]']")).SendKeys("999");
            driver.FindElement(By.XPath("//*[@name='prices[EUR]']")).SendKeys("910");

            driver.FindElement(By.XPath("//button[@name='save']")).Click();

            Assert.IsTrue(driver.FindElement(By.XPath($"//a[text()='{productName}']")).Displayed);

        }

        [TearDown]
        public void Stop()
        {
            Thread.Sleep(2000);
            driver.Quit();
            driver = null;
        }

        public string ProductName()
        {
            var characters = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            var random = new Random();
            var productName = new string(Enumerable.Repeat(characters, 10).Select(s => s[random.Next(s.Length)]).ToArray());
            return productName;
        }
    }
}