using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Task13_Cart
{
    [TestFixture]
    public class Cart
    {
        private IWebDriver driver;
        private readonly string _baseUrl = "http://localhost:8080/litecart/en/";

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void Scenario()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            AddProducts(3);
            
            driver.FindElement(By.XPath("//a[@class='link'][text()='Checkout »']")).Click();

            IList<IWebElement> productsCount = driver.FindElements(By.XPath("//tr//td[@class='item']"));
            for (int i = 0; i < productsCount.Count; i++)
            {
                IWebElement removeButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@name='remove_cart_item']")));
                removeButton.Click();
                wait.Until(d => d.FindElements(By.XPath("//tr//td[@class='item']")).Count == (productsCount.Count-1)-i);
            }
        }

        [TearDown]
        public void Stop()
        {
            Thread.Sleep(3000);
            driver.Quit();
            driver = null;
        }

        public void AddProducts(int count)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            for (int i = 0; i < count; i++)
            {
                driver.Url = _baseUrl;
                string quantity = driver.FindElement(By.XPath("//*[@class='quantity']")).Text;

                driver.FindElement(By.XPath("//*[@id='box-most-popular']//li")).Click();

                if (driver.FindElements(By.XPath("//*[@name='options[Size]']")).Count != 0)
                {
                    driver.FindElement(By.XPath("//*[@name='options[Size]']/option[2]")).Click();
                    driver.FindElement(By.XPath("//*[@name='add_cart_product']")).Click();
                }
                else
                {
                    driver.FindElement(By.XPath("//*[@name='add_cart_product']")).Click();
                }

                wait.Until(d => d.FindElement(By.XPath($"//*[@class='quantity']")).Text != quantity);
            }
        }
    }
}
