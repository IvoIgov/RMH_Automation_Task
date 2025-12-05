using AmazonAutomation.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace AmazonAutomation.Utilities
{
    public static class WaitHelper
    {
        private static WebDriverWait GetWait(IWebDriver driver)
        {
            return new WebDriverWait(driver,
                TimeSpan.FromSeconds(Constants.ExplicitWait));
        }

        public static IWebElement WaitUntilElementIsVisible(IWebDriver driver, By locator)
        {
            return GetWait(driver).Until(drv =>
            {
                var element = drv.FindElement(locator);
                return element.Displayed ? element : null;
            });
        }

        public static IWebElement WaitUntilElementIsClickable(IWebDriver driver, By locator)
        {
            return GetWait(driver).Until(drv =>
            {
                var element = drv.FindElement(locator);
                return (element.Displayed && element.Enabled) ? element : null;
            });
        }

        public static IWebElement WaitUntilElementExists(IWebDriver driver, By locator)
        {
            return GetWait(driver).Until(drv => drv.FindElement(locator));
        }

        public static bool WaitUntilTextIsPresent(IWebDriver driver, By locator, string text)
        {
            return GetWait(driver).Until(drv =>
                drv.FindElement(locator).Text.Contains(text));
        }
    }
}
