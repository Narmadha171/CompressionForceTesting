using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace CompressionForce.SeleniumTests
{
    [TestFixture]
    public class PostLogin_VerifySidebarAndUnitsTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        private readonly string baseUrl = "https://localhost:44379/";

        [OneTimeSetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--ignore-certificate-errors");

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            // Ensure fresh session
            driver.Manage().Cookies.DeleteAllCookies();
        }

        // ==============================
        // 🔐 Login Before Post-Login Tests
        // ==============================
        [Test, Order(1)]
        public void Login_As_Valid_User()
        {
            driver.Navigate().GoToUrl(baseUrl);

            EnterCredentials("rameshmesh117@gmail.com", "22csr080");
            ClickLogin();

            // Verify login by checking username span
            var userSpan = wait.Until(d =>
                d.FindElement(By.XPath("//header//nav//span[contains(@class,'text-white')]"))
            );

            Assert.That(userSpan.Displayed, Is.True);
            Console.WriteLine("Logged in user: " + userSpan.Text);
        }

        // ======================================
        // 📌 Verify Sidebar Buttons + Unit Cards
        // ======================================
        [Test, Order(2)]
        public void Verify_Sidebar_Buttons_And_Units()
        {
            driver.Navigate().GoToUrl(baseUrl);

            // ---------- Sidebar XPaths ----------
            string[] sidebarXPaths =
            {
                "//button[@data-nav-url='/Home/Diagnostics']",
                "//button[@data-nav-url='/Recipe/RecipeParameter']",
                "//button[@data-nav-url='/Home/Batch']",
                "//button[@data-nav-url='/Home/AutoMode']",
                "//button[@data-nav-url='/Report/Report']",
                "//button[@data-nav-url='/Home/Calibration']",
                "//button[@data-nav-url='/Home/Alarm']",
                "//button[@data-nav-url='/Home/HelpMenu']",
                "//button[@data-nav-url='/Home/AutoTare']",
                "//button[@data-nav-url='/Home/AccessManagement']",
                "//button[@data-nav-url='/AuditTrail/AuditTrail']",
                "//button[@data-nav-url='/Account/ChangePassword']"
            };

            foreach (var xpath in sidebarXPaths)
            {
                var button = wait.Until(d => d.FindElement(By.XPath(xpath)));
                Assert.That(button.Displayed, Is.True);
                Console.WriteLine("Sidebar button verified: " + button.Text.Trim());
            }

            // ---------- Unit Cards ----------
            for (int i = 1; i <= 3; i++)
            {
                var unitCard = wait.Until(d =>
                    d.FindElement(By.XPath($"//div[contains(@class,'bord-rad-4px')][.//div[text()='Unit {i}']]"))
                );

                Assert.That(unitCard.Displayed, Is.True);
                Console.WriteLine($"Unit {i} card is visible");
            }
        }

        // ==============================
        // 🔁 Helper Methods
        // ==============================
        private void EnterCredentials(string email, string password)
        {
            var emailInput = wait.Until(d => d.FindElement(By.Id("Email")));
            emailInput.Clear();
            emailInput.SendKeys(email);

            var passwordInput = wait.Until(d => d.FindElement(By.Id("Password")));
            passwordInput.Clear();
            passwordInput.SendKeys(password);
        }

        private void ClickLogin()
        {
            var loginBtn = wait.Until(d =>
                d.FindElement(By.XPath("//button[contains(text(),'Login')]"))
            );
            loginBtn.Click();
        }
        [OneTimeTearDown]
        public void Cleanup()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}

