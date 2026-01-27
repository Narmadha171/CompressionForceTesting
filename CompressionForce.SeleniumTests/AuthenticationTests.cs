using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Newtonsoft.Json;
using CompressionForce.SeleniumTests.Models;
using System;
using System.IO;

namespace CompressionForce.SeleniumTests.Tests
{
    [TestFixture]
    public class AuthenticationTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private AuthConfig config;

        // ================= SETUP =================
        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--ignore-certificate-errors");

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            string jsonPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "TestData",
                "authData.json"
            );

            config = JsonConvert.DeserializeObject<AuthConfig>(File.ReadAllText(jsonPath));
        }

        // ==================================================
        // LOGIN USING USERNAME + PASSWORD (3 COMBINATIONS)
        // ==================================================

        [Test, Order(1)]
        public void UsernameLogin_ValidUsername_ValidPassword()
        {
            NavigateToLogin();

            EnterLoginCredentials(
                config.LoginByUsername.ValidUsername,
                config.LoginByUsername.ValidPassword
            );

            ClickLogin();
            AssertLoginSuccess();
            Logout();
        }

        [Test, Order(2)]
        public void UsernameLogin_ValidUsername_InvalidPassword()
        {
            NavigateToLogin();

            EnterLoginCredentials(
                config.LoginByUsername.ValidUsername,
                config.LoginByUsername.InvalidPassword
            );

            ClickLogin();
            AssertLoginFailure();
        }

        [Test, Order(3)]
        public void UsernameLogin_InvalidUsername_InvalidPassword()
        {
            NavigateToLogin();

            EnterLoginCredentials(
                config.LoginByUsername.InvalidUsername,
                config.LoginByUsername.InvalidPassword
            );

            ClickLogin();
            AssertLoginFailure();
        }

        // ===========================================
        // LOGIN USING EMAIL + PASSWORD (3 COMBINATIONS)
        // ===========================================

        [Test, Order(4)]
        public void EmailLogin_ValidEmail_ValidPassword()
        {
            NavigateToLogin();

            EnterLoginCredentials(
                config.LoginByEmail.ValidEmail,
                config.LoginByEmail.ValidPassword
            );

            ClickLogin();
            AssertLoginSuccess();
            Logout();
        }

        [Test, Order(5)]
        public void EmailLogin_InvalidEmail_InvalidPassword()
        {
            NavigateToLogin();

            EnterLoginCredentials(
                config.LoginByEmail.InvalidEmail,
                config.LoginByEmail.InvalidPassword
            );

            ClickLogin();
            AssertLoginFailure();
        }

        [Test, Order(6)]
        public void EmailLogin_InvalidEmail_ValidPassword()
        {
            NavigateToLogin();

            EnterLoginCredentials(
                config.LoginByEmail.InvalidEmail,
                config.LoginByEmail.ValidPassword
            );

            ClickLogin();
            AssertLoginFailure();
        }

        // ================= SIGN UP TESTS =================

        [Test, Order(7)]
        public void SignUp_With_Valid_Details()
        {
            NavigateToSignUp();

            string username = "User" + DateTime.Now.Ticks;
            string email = "user" + DateTime.Now.Ticks + "@test.com";

            EnterSignUpDetails(
                username,
                email,
                config.SignUp.ValidPassword,
                config.SignUp.ValidPassword
            );

            ClickSignUp();
            AssertSignUpSuccess();
        }

        [Test, Order(8)]
        public void SignUp_With_Invalid_Email()
        {
            NavigateToSignUp();

            EnterSignUpDetails(
                "User" + DateTime.Now.Ticks,
                config.SignUp.InvalidEmail,
                config.SignUp.ValidPassword,
                config.SignUp.ValidPassword
            );

            ClickSignUp();
            AssertValidationError();
        }

        [Test, Order(9)]
        public void SignUp_With_Password_Mismatch()
        {
            NavigateToSignUp();

            EnterSignUpDetails(
                "User" + DateTime.Now.Ticks,
                "mail@test.com",
                config.SignUp.ValidPassword,
                "Mismatch123"
            );

            ClickSignUp();
            AssertValidationError();
        }

        // ================= HELPER METHODS =================

        private void NavigateToLogin()
        {
            driver.Navigate().GoToUrl(config.BaseUrl + "Account/Login");
            wait.Until(d => d.FindElement(By.Id("Email")));
        }

        private void NavigateToSignUp()
        {
            driver.Navigate().GoToUrl(config.BaseUrl + "Account/SignUp");
            wait.Until(d => d.FindElement(By.Id("UserName")));
        }

        private void EnterLoginCredentials(string user, string password)
        {
            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys(user);

            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys(password);
        }

        private void ClickLogin()
        {
            driver.FindElement(By.XPath("//button[contains(text(),'Login')]")).Click();
        }

        private void EnterSignUpDetails(string user, string email, string pass, string confirm)
        {
            driver.FindElement(By.Id("UserName")).SendKeys(user);
            driver.FindElement(By.Id("Email")).SendKeys(email);
            driver.FindElement(By.Id("Password")).SendKeys(pass);
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys(confirm);
        }

        private void ClickSignUp()
        {
            driver.FindElement(By.XPath("//button[contains(text(),'Sign Up')]")).Click();
        }

        private void AssertLoginSuccess()
        {
            var welcome = wait.Until(d => d.FindElement(By.XPath("//header//nav//span")));
            Assert.That(welcome.Displayed, Is.True);
        }

        private void AssertLoginFailure()
        {
            var error = wait.Until(d =>
                d.FindElement(By.XPath("//*[contains(@class,'text-danger') or contains(@class,'alert-danger')]"))
            );
            Assert.That(error.Displayed, Is.True);
        }

        private void AssertSignUpSuccess()
        {
            var success = wait.Until(d =>
                d.FindElement(By.XPath("//*[contains(@class,'text-success') or contains(@class,'alert-success')]"))
            );
            Assert.That(success.Displayed, Is.True);
        }

        private void AssertValidationError()
        {
            var error = wait.Until(d =>
                d.FindElement(By.XPath("//*[contains(@class,'text-danger') or contains(@class,'field-validation-error')]"))
            );
            Assert.That(error.Displayed, Is.True);
        }

        private void Logout()
        {
            try
            {
                driver.FindElement(By.XPath("//a[contains(text(),'Logout')]")).Click();
            }
            catch { }
        }

        // ================= TEARDOWN =================
        [TearDown]
        public void TearDown()
        {
            //driver.Quit();
        }
    }
}
