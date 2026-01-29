using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using CompressionForce.SeleniumTests.Models;
using CompressionForce.SeleniumTests.TestDataSources;

namespace CompressionForce.SeleniumTests.Tests
{
    [TestFixture]
    public class SignUpTests
    {
        private ChromeDriver? driver;
        private WebDriverWait? wait;
        private const string SIGNUP_URL = "https://localhost:44379/Account/Register";

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--start-maximized");

            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver!, TimeSpan.FromSeconds(10));
        }

        [Test]
        [TestCaseSource(typeof(TestCaseSourceProvider), nameof(TestCaseSourceProvider.GetSignUpTestData))]
        public void SignUp_Test(SignUpTestCase data)
        {
            driver!.Navigate().GoToUrl(SIGNUP_URL);

            FillSignUpForm(data);

            var signUpButton = wait!.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Sign Up')]")));
            signUpButton.Click();

            if (data.ExpectedResult.Equals("Success", StringComparison.OrdinalIgnoreCase))
            {
                wait.Until(d => d.Url.Contains("Dashboard") || d.Url.Contains("Account"));
                Assert.That(driver.Url, Does.Contain("Dashboard").Or.Contains("Account"));
            }
            else
            {
                var errorVisible = wait.Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("//*[contains(@class,'text-danger') or contains(@class,'alert-danger')]")));
                Assert.That(errorVisible.Displayed, Is.True);
            }
        }

        private void FillSignUpForm(SignUpTestCase data)
        {
            wait!.Until(ExpectedConditions.ElementIsVisible(By.Id("Email")));

            driver!.FindElement(By.Id("UserName")).SendKeys(data.Username ?? string.Empty);
            driver.FindElement(By.Id("Email")).SendKeys(data.Email ?? string.Empty);
            driver.FindElement(By.Id("Password")).SendKeys(data.Password ?? string.Empty);
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys(data.ConfirmPassword ?? string.Empty);
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }
    }
}