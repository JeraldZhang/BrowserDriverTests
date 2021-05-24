using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace EdgeTests
{
    [TestClass]
    public class CalculatorTests
    {

        private EdgeDriver _driver;

        [TestInitialize]
        public void EdgeInitialize()
        {
            // Initialize browser driver 
            var options = new EdgeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal,
                UseChromium = true
            };
            _driver = new EdgeDriver(Path.GetFullPath(@".\Drivers\edgedriver_win64\"), options);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(10);
        }

        [TestMethod]
        public void VerifyCosine()
        {
            _driver.Url = "http://www.calculator.net";


            // Click AC button
            new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                    .ElementToBeClickable(_driver.FindElement(By.XPath("//*[@id=\"sciout\"]/tbody/tr[2]/td[2]/div/div[5]/span[3]")))).Click();
            // click COS button
            new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                    .ElementToBeClickable(_driver.FindElement(By.XPath("//*[@id=\"sciout\"]/tbody/tr[2]/td[1]/div/div[1]/span[2]")))).Click();
            // Click Rad 
            new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                    .ElementToBeClickable(_driver.FindElement(By.XPath("//*[@id=\"scirdsettingr\"]")))).Click();
            // click 1 button to input 1 and caculate cos 1
            new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                    .ElementToBeClickable(_driver.FindElement(By.XPath("//*[@id=\"sciout\"]/tbody/tr[2]/td[2]/div/div[3]/span[1]")))).Click();

            var actual = double.Parse(_driver.FindElement(By.XPath("//*[@id=\"sciOutPut\"]")).Text);
            var expcted = Math.Cos(1);// keep 10 scale

            Assert.AreEqual(expcted, actual, 0.00001);
        }


        [TestMethod]
        public void VerifyLog()
        {
            _driver.Url = "http://www.calculator.net";
            // Click AC button
            new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(_driver.FindElement(By.XPath("//*[@id=\"sciout\"]/tbody/tr[2]/td[2]/div/div[5]/span[3]")))).Click();
            // click Log button
            new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
               .ElementToBeClickable(_driver.FindElement(By.XPath("//*[@id=\"sciout\"]/tbody/tr[2]/td[1]/div/div[4]/span[5]")))).Click();
            // Click 1 button to caculate log 1
            new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
               .ElementToBeClickable(_driver.FindElement(By.XPath("//*[@id=\"sciout\"]/tbody/tr[2]/td[2]/div/div[3]/span[1]")))).Click();

            var actual = double.Parse(_driver.FindElement(By.XPath("//*[@id=\"sciOutPut\"]")).Text);
            var expcted = Math.Log(1);// keep 10 scale

            Assert.AreEqual(expcted, actual, 0.00001);
        }

        [TestMethod]
        public void VerifyAdd()
        {
            _driver.Url = "http://www.calculator.net";
            // Click AC button
            new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(_driver.FindElement(By.XPath("//*[@id=\"sciout\"]/tbody/tr[2]/td[2]/div/div[5]/span[3]")))).Click();
            // Click 1 
            new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(_driver.FindElement(By.XPath("//*[@id=\"sciout\"]/tbody/tr[2]/td[2]/div/div[3]/span[1]")))).Click();
            // Click +
            new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(_driver.FindElement(By.XPath("//*[@id=\"sciout\"]/tbody/tr[2]/td[2]/div/div[1]/span[4]")))).Click();
            // Click 1 
            new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(_driver.FindElement(By.XPath("//*[@id=\"sciout\"]/tbody/tr[2]/td[2]/div/div[3]/span[1]")))).Click();

            var actual = double.Parse(_driver.FindElement(By.XPath("//*[@id=\"sciOutPut\"]")).Text);
            var expcted = 1 + 1;

            Assert.AreEqual(expcted, actual, 0.00001);
        }



        [TestMethod]
        public void VerifyNFactorial()
        {
            _driver.Url = "http://www.calculator.net";
            // Click AC button
            new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(_driver.FindElement(By.XPath("//*[@id=\"sciout\"]/tbody/tr[2]/td[2]/div/div[5]/span[3]")))).Click();
            // Click 9 
            new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(_driver.FindElement(By.XPath("//*[@id=\"sciout\"]/tbody/tr[2]/td[2]/div/div[1]/span[3]")))).Click();
            // Click n!
            new WebDriverWait(_driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(_driver.FindElement(By.XPath("//*[@id=\"sciout\"]/tbody/tr[2]/td[1]/div/div[5]/span[5]")))).Click();

            var actual = double.Parse(_driver.FindElement(By.XPath("//*[@id=\"sciOutPut\"]")).Text);
            var expcted = 9 * 8 * 7 * 6 * 5 * 4 * 3 * 2;

            Assert.AreEqual(expcted, actual);
        }

        [TestCleanup]
        public void EdgeCleanup()
        {
            _driver.Quit();
        }
    }
}
