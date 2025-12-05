using AmazonAutomation.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.IO;

namespace AmazonAutomation.Tests
{
    public abstract class TestBase
    {
        protected IWebDriver Driver;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Driver = DriverFactory.Driver;
            Driver.Manage().Window.Maximize();
        }

        [SetUp]
        public void SetUp()
        {
            Driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            var context = TestContext.CurrentContext;

            // Only capture screenshots on test failure
            if (context.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                try
                {
                    var screenshot = ((ITakesScreenshot)DriverFactory.Driver).GetScreenshot();

                    string screenshotsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
                    if (!Directory.Exists(screenshotsDir))
                        Directory.CreateDirectory(screenshotsDir);

                    string fileName = $"{context.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                    string filePath = Path.Combine(screenshotsDir, fileName);

                    screenshot.SaveAsFile(filePath);

                    Console.WriteLine($"Screenshot saved: {filePath}");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to capture screenshot: " + e.Message);
                }
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            DriverFactory.QuitDriver();
        }
    }
}
