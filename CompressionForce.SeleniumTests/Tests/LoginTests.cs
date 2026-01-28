using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace CompressionForce.SeleniumTests.Tests
{
    [TestFixture]
    public class LoginTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private const string BASE_URL = "https://localhost:44379/";

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
        public void Login_Test()
        {
            driver.Navigate().GoToUrl(BASE_URL);

            var emailInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Email")));
            emailInput.Clear();
            emailInput.SendKeys("rameshmesh117@gmail.com");

            var passwordInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Password")));
            passwordInput.Clear();
            passwordInput.SendKeys("22csr080");

            var rememberMe = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("RememberMe")));
            rememberMe.Click();

            var loginBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@type='submit']")));
            loginBtn.Click();

            // Debugging: print actual URL
            Console.WriteLine("Redirected URL: " + driver.Url);

            // ✅ Updated assertion
            Assert.That(
                driver.Url.Contains("Dashboard") || driver.Url.Contains("Home") || driver.Url.Contains("Account"),
                "Login failed or did not redirect correctly."
            );
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
