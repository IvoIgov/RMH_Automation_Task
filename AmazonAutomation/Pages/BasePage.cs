using AmazonAutomation.Drivers;
using AmazonAutomation.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace AmazonAutomation.Pages
{
    public abstract class BasePage
    {
        protected IWebDriver Driver => DriverFactory.Driver;

        protected BasePage(IWebDriver driver) { }

        protected void NavigateTo(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        protected IWebElement WaitAndFindElement(By locator)
        {
            return WaitHelper.WaitUntilElementIsVisible(Driver, locator);
        }

        protected void ClickElement(By locator)
        {
            var element = WaitHelper.WaitUntilElementIsClickable(Driver, locator);
            element.Click();
        }

        protected void ClickElementIfExists(By locator, int timeoutInSeconds = 5)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
                wait.PollingInterval = TimeSpan.FromMilliseconds(500);
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

                IWebElement element = wait.Until(drv =>
                {
                    try
                    {
                        var el = drv.FindElement(locator);
                        return (el.Displayed && el.Enabled) ? el : null;
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                    catch (StaleElementReferenceException)
                    {
                        return null;
                    }
                });

                element?.Click();
            }
            catch (WebDriverTimeoutException)
            {
            }
        }

        protected void EnterText(By locator, string text, bool clearFirst = true)
        {
            var element = WaitHelper.WaitUntilElementIsVisible(Driver, locator);

            if (clearFirst)
                element.Clear();

            element.SendKeys(text);
        }

        protected string GetElementText(By locator)
        {
            var element = WaitHelper.WaitUntilElementIsVisible(Driver, locator);
            return element.Text;
        }

        protected bool ElementExists(By locator)
        {
            try
            {
                return WaitHelper.WaitUntilElementExists(Driver, locator) != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
