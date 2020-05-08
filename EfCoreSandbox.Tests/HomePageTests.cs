using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.IO;



namespace EfCoreSandbox.Tests
{
    [TestClass]
    public class HomePageTests
    {
        private const string webAppBaseUrl = "https://www.flashback.org/aktuella-amnen";



        private static IWebDriver driver;




        [ClassInitialize]
        public static void ClassInitialise(TestContext testContext)
        {
            SetupDriver();
            driver.Url = webAppBaseUrl;
            driver.Navigate();
        }



        private static void SetupDriver()
        {
            try
            {
                InternetExplorerOptions ieOptions = new InternetExplorerOptions
                {
                    EnableNativeEvents = false,
                    UnhandledPromptBehavior = UnhandledPromptBehavior.Accept,
                    EnablePersistentHover = true,
                    IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                    IgnoreZoomLevel = true,
                    EnsureCleanSession = true,
                };



                // Attempt to read the IEWebDriver environment variable that exists on the Azure
                // platform and then fall back to the local directory.
                string ieWebDriverPath = Environment.GetEnvironmentVariable("IEWebDriver");
                if (string.IsNullOrEmpty(ieWebDriverPath))
                {
                    ieWebDriverPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
                }



                driver = new InternetExplorerDriver(ieWebDriverPath, ieOptions)
                {
                    Url = webAppBaseUrl
                };
            }
            catch (Exception ex)
            {
                TeardownDriver();
                throw new ApplicationException("Could not setup IWebDriver.", ex);
            }
        }



        [TestMethod]
        public void HomePageHeadingContainsWelcome()
        {



            // Arrange/Act/Assert
            //driver.FindElement(By.TagName("h1")).Text.Should().Contain("Forum");
        }



        [ClassCleanup]
        public static void ClassCleanup()
        {
            TeardownDriver();
        }



        private static void TeardownDriver()
        {
            if (driver != null)
            {
                driver.Close();
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }
    }



}