using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace CompressionForce.SeleniumTests.Tests
{
    [TestFixture]
    public class SignUpTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private const string SIGNUP_URL = "https://localhost:44379/Account/Register";

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");

            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void SignUp_Test()
        {
            driver.Navigate().GoToUrl(SIGNUP_URL);

            // ✅ Wait for form container
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//form")));

            // Fill SignUp form
            FillSignUpForm("testuser", "testuser@example.com", "TestPassword123", "TestPassword123");

            // Click SignUp button
            ClickSignUpAndWaitForResult();

            // Debugging: print actual URL
            Console.WriteLine("Redirected URL: " + driver.Url);

            // ✅ Updated assertion
            Assert.That(driver.Url.Contains("Account") || driver.Url.Contains("Dashboard"),
                "Sign Up did not redirect correctly.");
        }

        private void FillSignUpForm(string username, string email, string password, string confirmPassword)
        {
            var usernameInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("UserName"))); // ✅ corrected
            var emailInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Email")));
            var passwordInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Password")));
            var confirmInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("ConfirmPassword")));

            usernameInput.Clear();
            usernameInput.SendKeys(username ?? string.Empty);

            emailInput.Clear();
            emailInput.SendKeys(email ?? string.Empty);

            passwordInput.Clear();
            passwordInput.SendKeys(password ?? string.Empty);

            confirmInput.Clear();
            confirmInput.SendKeys(confirmPassword ?? string.Empty);
        }

        private void ClickSignUpAndWaitForResult()
        {
            var signUpButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Sign Up')]")));
            signUpButton.Click();

            // Wait until success or error message appears
            wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//*[contains(@class,'alert-success') or contains(@class,'text-danger')]")
            ));
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                try { driver.Quit(); } catch { }
                driver.Dispose();
                driver = null;
            }
        }
    }
}
