using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AmazonAutomation.Drivers
{
    public class DriverFactory
    {
        private static IWebDriver _driver;

        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    var options = new ChromeOptions();
                    _driver = new ChromeDriver(options);
                    _driver.Manage().Window.Maximize();
                }
                return _driver;
            }
        }

        public static void QuitDriver()
        {
            _driver?.Quit();
            _driver = null;
        }
    }
}
