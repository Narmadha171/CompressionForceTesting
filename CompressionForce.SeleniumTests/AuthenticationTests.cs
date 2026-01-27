using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;

namespace CompressionForce.SeleniumTests
{
    // ===== JSON Models =====
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class Config
    {
        public string Url { get; set; }
        public User ValidUser { get; set; }
        public User InvalidUser { get; set; }
    }

    // ===== Selenium Tests =====
    [TestFixture]
    public class AuthenticationTests
    {
        IWebDriver driver;
        WebDriverWait wait;
        public Config config;

        // ===== OneTime Setup =====
        [OneTimeSetUp]
        public void Setup()
        {
            // 1️⃣ Load JSON config
            string jsonText = File.ReadAllText("config.json");
            config = JsonConvert.DeserializeObject<Config>(jsonText);

            // 2️⃣ Setup Chrome
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--ignore-certificate-errors");
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            driver.Navigate().GoToUrl(config.Url);
        }

        // ===== VALID LOGIN =====
        [Test, Order(1)]
        public void Login_Valid_User()
        {
            Thread.Sleep(2000);
            EnterCredentials(config.ValidUser.Email, config.ValidUser.Password);
            ClickLogin();
            Thread.Sleep(4000);

            Assert.That(IsUserLoggedIn(), Is.True);
        }

        // ===== LOGOUT =====
        [Test, Order(2)]
        public void Logout_User()
        {
            Thread.Sleep(2000);
            try
            {
                var logoutBtn = wait.Until(d => d.FindElement(By.XPath("//a[contains(text(),'Logout')]")));
                logoutBtn.Click();
            }
            catch { /* already logged out */ }

            Thread.Sleep(3000);
            Assert.That(IsLoginPageVisible(), Is.True);
        }

        // ===== INVALID LOGIN =====
        [Test, Order(3)]
        public void Login_Invalid_User()
        {
            EnterCredentials(config.InvalidUser.Email, config.InvalidUser.Password);
            Thread.Sleep(2000);
            ClickLogin();
            Thread.Sleep(4000);

            Assert.That(IsErrorDisplayed(), Is.True);
        }

        // ===== SIGNUP PAGE =====
        [Test, Order(4)]
        public void Signup_Page_Test()
        {
            EnsureLoggedOut(); // ensures user is not logged in
            var signupLink = wait.Until(d => d.FindElement(By.XPath("//a[contains(text(),'Sign Up')]")));
            signupLink.Click();
            Thread.Sleep(3000);

            Assert.That(driver.Url.Contains("Signup"), Is.True);
        }

        // ===== HELPERS =====
        void EnterCredentials(string user, string pass)
        {
            var email = wait.Until(d => d.FindElement(By.Id("Email")));
            email.Clear();
            email.SendKeys(user);

            var pwd = driver.FindElement(By.Id("Password"));
            pwd.Clear();
            pwd.SendKeys(pass);
        }

        void ClickLogin()
        {
            driver.FindElement(By.XPath("//button[contains(text(),'Login')]")).Click();
        }

        bool IsUserLoggedIn()
        {
            try
            {
                return driver.FindElement(By.XPath("//header//nav//span")).Displayed;
            }
            catch { return false; }
        }

        bool IsLoginPageVisible()
        {
            try
            {
                return driver.FindElement(By.Id("Email")).Displayed;
            }
            catch { return false; }
        }

        bool IsErrorDisplayed()
        {
            try
            {
                return driver.FindElement(By.XPath(
                    "//*[contains(@class,'alert-danger') or contains(@class,'text-danger')]")).Displayed;
            }
            catch { return false; }
        }

        void EnsureLoggedOut()
        {
            try
            {
                var logoutBtn = driver.FindElement(By.XPath("//a[contains(text(),'Logout')]"));
                if (logoutBtn.Displayed)
                    logoutBtn.Click();
                Thread.Sleep(2000);
            }
            catch { /* already logged out */ }
        }

        // ===== OneTime TearDown =====
        [OneTimeTearDown]
        public void TearDown()
        {
            Console.WriteLine("Tests completed. Close browser manually.");
            Thread.Sleep(30000); // keeps browser open for 30 seconds
            driver.Quit();
        }
    }
}
