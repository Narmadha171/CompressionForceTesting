using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using CompressionForce.SeleniumTests.Models;
using CompressionForce.SeleniumTests.TestDataSources;
using CompressionForce.SeleniumTests.Utilities;

namespace CompressionForce.SeleniumTests.Tests
{
    [TestFixture]
    public class LoginTests
    {
        // Fix for CS8618/CS8625: Mark as nullable
        private ChromeDriver? driver;
        private WebDriverWait? wait;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--ignore-certificate-errors");

            driver = new ChromeDriver(options);
            // Fix for CS8602: Use ! to tell compiler driver is initialized
            wait = new WebDriverWait(driver!, TimeSpan.FromSeconds(10));
        }
      

        [Test]
        [TestCaseSource(typeof(TestCaseSourceProvider), nameof(TestCaseSourceProvider.GetLoginTestData))]
        public void Login_Test(LoginTestCase data)
        {
            var root = TestDataReader.LoadTestData();
            // Fix for CS8602: driver! ensures we know it's not null here
            driver!.Navigate().GoToUrl(root.BASE_URL);

            var emailInput = wait!.Until(ExpectedConditions.ElementIsVisible(By.Id("Email")));
            emailInput.Clear();
            emailInput.SendKeys(data.GetLoginIdentifier() ?? string.Empty);

            driver.FindElement(By.Id("Password")).SendKeys(data.Password ?? string.Empty);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            bool isSuccess = driver.Url.Contains("Dashboard") || driver.Url.Contains("Home");
            bool expected = data.ExpectedResult.Equals("Success", StringComparison.OrdinalIgnoreCase);

            Assert.That(isSuccess, Is.EqualTo(expected), $"Login result mismatch for: {data.TestName}");
        }

        [TearDown]
        public void TearDown()
        {
            // Fix for NUnit1032 and null safety
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }
    }
}