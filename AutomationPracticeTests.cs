using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EdgeTests
{
    [TestClass]
    public class AutomationPracticeTests
    {
        private EdgeDriver _driver;
        [TestInitialize]
        public void EdgeInitialize()
        {
            // Initialize browser driver 
            var options = new EdgeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal,
            };
            _driver = new EdgeDriver(Path.GetFullPath(@".\Drivers\edgedriver_win64\"), options);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(10);
        }


        [TestMethod]
        public void AvailabilityOfAllLinks()
        {
            _driver.Url = "http://automationpractice.com/";

            var linksElements = _driver.FindElementsByTagName("a");
            var linksElementsCount = linksElements.Count;
            for (int i = 0; i < linksElementsCount; i++)
            {
                var link = linksElements.ElementAt(i);
                var href = link.GetAttribute("href");
                var target = link.GetAttribute("target");
                Console.WriteLine(href);
                Console.WriteLine(target);
                if (!string.Equals(target, "_blank") && href.Contains("automationpractice"))
                {
                    _driver.Navigate().GoToUrl(href);
                    _driver.Navigate().Back();
                    linksElements = _driver.FindElementsByTagName("a");
                }
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void AmountOfThreeItems()
        {
            _driver.Navigate().GoToUrl("http://automationpractice.com/");
            var actions = new Actions(_driver);

            // add three goods to car
            for (int i = 0; i < 3; i++)
            {
                var goodsElemnet = _driver.FindElement(By.XPath($"//*[@id=\"homefeatured\"]/li[{i + 1}]"));
                var addToCarButton = _driver.FindElement(By.XPath($"//*[@id=\"homefeatured\"]/li[{i + 1}]/div/div[2]/div[2]/a[1]/span"));

                // move to googs to show add-To-Car Button
                actions.Reset();
                actions.MoveToElement(goodsElemnet).Perform();

                // add to car
                new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                    .ElementToBeClickable(addToCarButton)).Click();

                // close pop up window
                new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                  .ElementToBeClickable(
                        _driver.FindElement(By.XPath($"//*[@id=\"layer_cart\"]/div[1]/div[1]/span"))))
                    .Click();
            }

            // expand car list
            var carToggle = _driver.FindElement(By.XPath("//*[@id=\"header\"]/div[3]/div/div/div[3]/div/a"));
            actions.Reset();
            actions.MoveToElement(carToggle).Perform();

            double expected = 0;
            // find each price of three items and add together
            for (int i = 0; i < 3; i++)
            {
                new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                    .ElementIsVisible(
                       By.XPath($"//*[@id=\"header\"]/div[3]/div/div/div[3]/div/div/div/div/dl/dt[{i + 1}]/div/span")));

                // add goods price
                expected += GetPrice(_driver.FindElement(By.XPath($"//*[@id=\"header\"]/div[3]/div/div/div[3]/div/div/div/div/dl/dt[{i + 1}]/div/span")).Text);
            }
            actions.Reset();
            actions.MoveToElement(carToggle).Perform();

            // add shipping price
            expected += GetPrice(_driver.FindElement(By.XPath($"//*[@id=\"header\"]/div[3]/div/div/div[3]/div/div/div/div/div/div[1]/span[1]")).Text);

            var actual = GetPrice(_driver.FindElement(By.XPath("//*[@id=\"header\"]/div[3]/div/div/div[3]/div/div/div/div/div/div[2]/span[1]")).Text);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task AmountOfThreeSubtractTwoItems()
        {
            _driver.Navigate().GoToUrl("http://automationpractice.com/");
            var actions = new Actions(_driver);

            // add three items to car
            for (int i = 0; i < 3; i++)
            {
                var goodsElemnet = _driver.FindElement(By.XPath($"//*[@id=\"homefeatured\"]/li[{i + 1}]"));
                var addToCarButton = _driver.FindElement(By.XPath($"//*[@id=\"homefeatured\"]/li[{i + 1}]/div/div[2]/div[2]/a[1]/span"));

                // move to items to show add-To-Car Button only for large screen
                actions.Reset();
                actions.MoveToElement(goodsElemnet).Perform();

                // add to car
                new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                    .ElementToBeClickable(addToCarButton)).Click();

                // close pop up window
                new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                  .ElementToBeClickable(
                        _driver.FindElement(By.XPath($"//*[@id=\"layer_cart\"]/div[1]/div[1]/span"))))
                    .Click();
            }

            // expand car list
            var carToggle = _driver.FindElement(By.XPath("//*[@id=\"header\"]/div[3]/div/div/div[3]/div/a"));
            actions.Reset();
            actions.MoveToElement(carToggle).Perform();

            // remove two items to car
            for (int i = 0; i < 2; i++)
            {
                var pathOfRemoveButton = By.XPath($"//*[@id=\"header\"]/div[3]/div/div/div[3]/div/div/div/div/dl/dt[{i + 1}]/span/a");

                new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                    .ElementToBeClickable(_driver.FindElement(pathOfRemoveButton))).Click();

                await Task.Delay(3000);
            }

            double expected = 0;
            // find each price of three items and add together

            new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementIsVisible(
                   By.XPath($"//*[@id=\"header\"]/div[3]/div/div/div[3]/div/div/div/div/dl/dt[1]/div/span")));

            // add goods price
            expected += GetPrice(_driver.FindElement(By.XPath($"//*[@id=\"header\"]/div[3]/div/div/div[3]/div/div/div/div/dl/dt[1]/div/span")).Text);

            // add shipping price
            expected += GetPrice(_driver.FindElement(By.XPath($"//*[@id=\"header\"]/div[3]/div/div/div[3]/div/div/div/div/div/div[1]/span[1]")).Text);

            var actual = GetPrice(_driver.FindElement(By.XPath("//*[@id=\"header\"]/div[3]/div/div/div[3]/div/div/div/div/div/div[2]/span[1]")).Text);

            Assert.AreEqual(expected, actual);
        }

        [TestCleanup]
        public void EdgeCleanup()
        {
            _driver.Quit();
        }

        private double GetPrice(string price)
        {
            return double.Parse(price.Remove(0, 1));
        }
    }
}
