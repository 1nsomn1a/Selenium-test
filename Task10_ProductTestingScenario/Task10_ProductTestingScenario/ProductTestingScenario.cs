using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;
using System.Text.RegularExpressions;

namespace Task10_ProductTestingScenario
{
    [TestFixture]
    public class ProductTestingScenario
    {
        private IWebDriver driver;
        private readonly string _MainPage = "http://localhost:8080/litecart/en/";


        [OneTimeSetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            //driver = new FirefoxDriver();
            //driver = new EdgeDriver();
            //InternetExplorerOptions options = new InternetExplorerOptions();
            //options.RequireWindowFocus = true;
            //driver = new InternetExplorerDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void test()
        {
            driver.Url = _MainPage;

            IWebElement product = driver.FindElement(By.XPath("//div[@id='box-campaigns']//li"));

            string productName = product.FindElement(By.XPath(".//div[@class='name']")).GetAttribute("textContent");
            string productRegularPrice = product.FindElement(By.XPath(".//s[@class='regular-price']")).GetAttribute("textContent");
            string productCampaignPrice = product.FindElement(By.XPath(".//strong[@class='campaign-price']")).GetAttribute("textContent");

            string productRegularPriceStyle = product.FindElement(By.XPath(".//s[@class='regular-price']")).GetCssValue("text-decoration-line");
            Assert.AreEqual(productRegularPriceStyle, "line-through");
            Console.WriteLine(productRegularPriceStyle);

            string productRegularPriceColor = product.FindElement(By.XPath(".//s[@class='regular-price']")).GetCssValue("text-decoration-color");
            AreEqual_RGB(productRegularPriceColor); //Are R & G & B equal?

            string productCampaignPriceColor = product.FindElement(By.XPath(".//strong[@class='campaign-price']")).GetCssValue("text-decoration-color");
            AreZero_GB(productCampaignPriceColor); //Are G & B - Zero?

            int productCampaignPriceStyle = Convert.ToInt32(product.FindElement(By.XPath(".//strong[@class='campaign-price']")).GetCssValue("font-weight")); // bold
            Assert.That(productCampaignPriceStyle, Is.GreaterThan(699)); //IsBOLD?

            int productRegularPriceHeight = Convert.ToInt32(product.FindElement(By.XPath(".//s[@class='regular-price']")).GetAttribute("offsetHeight"));
            int productRegularPriceWidth = Convert.ToInt32(product.FindElement(By.XPath(".//s[@class='regular-price']")).GetAttribute("offsetWidth"));
            int productCampaignPriceHeight = Convert.ToInt32(product.FindElement(By.XPath(".//strong[@class='campaign-price']")).GetAttribute("offsetHeight"));
            int productCampaignPriceWidth = Convert.ToInt32(product.FindElement(By.XPath(".//strong[@class='campaign-price']")).GetAttribute("offsetWidth"));

            int regularPrice = productRegularPriceHeight + productRegularPriceWidth;
            int campaignPrice = productCampaignPriceHeight + productCampaignPriceWidth;
            Assert.That(campaignPrice, Is.GreaterThan(regularPrice)); //perimeter is larger than

            product.Click();

            IWebElement product2 = driver.FindElement(By.XPath("//div[@id='box-product']"));

            string productName2 = product2.FindElement(By.XPath(".//h1")).GetAttribute("textContent");
            string productRegularPrice2 = product2.FindElement(By.XPath(".//s[@class='regular-price']")).GetAttribute("textContent");
            string productCampaignPrice2 = product2.FindElement(By.XPath(".//strong[@class='campaign-price']")).GetAttribute("textContent");

            string productRegularPriceStyle2 = product2.FindElement(By.XPath(".//s[@class='regular-price']")).GetCssValue("text-decoration-line");
            Assert.AreEqual(productRegularPriceStyle2, "line-through");

            string productRegularPriceColor2 = product2.FindElement(By.XPath(".//s[@class='regular-price']")).GetCssValue("text-decoration-color");
            AreEqual_RGB(productRegularPriceColor2); //Are R & G & B equal?

            string productCampaignPriceColor2 = product2.FindElement(By.XPath(".//strong[@class='campaign-price']")).GetCssValue("text-decoration-color");
            AreZero_GB(productCampaignPriceColor2);

            int productCampaignPriceStyle2 = Convert.ToInt32(product2.FindElement(By.XPath(".//strong[@class='campaign-price']")).GetCssValue("font-weight")); 
            Assert.That(productCampaignPriceStyle, Is.GreaterThan(699));

            int productRegularPriceHeight2 = Convert.ToInt32(product2.FindElement(By.XPath(".//s[@class='regular-price']")).GetAttribute("offsetHeight"));
            int productRegularPriceWidth2 = Convert.ToInt32(product2.FindElement(By.XPath(".//s[@class='regular-price']")).GetAttribute("offsetWidth"));
            int productCampaignPriceHeight2 = Convert.ToInt32(product2.FindElement(By.XPath(".//strong[@class='campaign-price']")).GetAttribute("offsetHeight"));
            int productCampaignPriceWidth2 = Convert.ToInt32(product2.FindElement(By.XPath(".//strong[@class='campaign-price']")).GetAttribute("offsetWidth"));

            int regularPrice2 = productRegularPriceHeight2 + productRegularPriceWidth2;
            int campaignPrice2 = productCampaignPriceHeight2 + productCampaignPriceWidth2;
            Assert.That(campaignPrice2, Is.GreaterThan(regularPrice2)); //perimeter is larger than

            //names
            Assert.AreEqual(productName, productName2);
            //prices
            Assert.AreEqual(productRegularPrice, productRegularPrice2);
            Assert.AreEqual(productCampaignPrice, productCampaignPrice2);
        }

        [OneTimeTearDown]
        public void Stop()
        {
            Thread.Sleep(2000);
            driver.Quit();
            driver = null;
        }

        public void AreEqual_RGB(string productColor)
        {
            Regex regex = new Regex(@"rgb\((?<r>\d{1,3}), (?<g>\d{1,3}), (?<b>\d{1,3})\)");
            Match match = regex.Match(productColor);
            Color color = null;
            if (match.Success)
            {
                color = new Color(int.Parse(match.Groups["r"].Value), int.Parse(match.Groups["g"].Value), int.Parse(match.Groups["b"].Value));
            }

            Assert.AreEqual(color.r, color.g);
            Assert.AreEqual(color.r, color.b);
        }

        public void AreZero_GB(string productColor)
        {
            Regex regex = new Regex(@"rgb\((?<r>\d{1,3}), (?<g>\d{1,3}), (?<b>\d{1,3})\)");
            Match match = regex.Match(productColor);
            Color color = null;
            if (match.Success)
            {
                color = new Color(int.Parse(match.Groups["r"].Value), int.Parse(match.Groups["g"].Value), int.Parse(match.Groups["b"].Value));
            }

            Assert.AreEqual(color.g, 0);
            Assert.AreEqual(color.b, 0);

            //Assert.AreEqual(color.g, null);
            //Assert.AreEqual(color.b, null);
        }

        public class Color
        {

            public int r { get; set; }
            public int g { get; set; }
            public int b { get; set; }

            public Color(int r, int g, int b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }
        }
    }
}
