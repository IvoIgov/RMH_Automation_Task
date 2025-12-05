using AmazonAutomation.Config;
using AmazonAutomation.Utilities;
using OpenQA.Selenium;

namespace AmazonAutomation.Pages
{
    public class CartPage : BasePage
    {
        public CartPage(IWebDriver driver) : base(driver) { }

        private readonly By _cartContainer = By.Id("sc-active-cart");
        private By _cartItemTitle => By.CssSelector("span.a-truncate-cut");

        public void WaitForPageLoad(int timeoutInSeconds = 10)
        {
            try
            {
                WaitHelper.WaitUntilElementIsVisible(Driver, _cartContainer);
                return;
            }
            catch (WebDriverTimeoutException)
            {
            }

            WaitHelper.WaitUntilElementIsVisible(Driver, _cartItemTitle);
        }

        public bool IsProductAdded()
        {
            try
            {
                WaitHelper.WaitUntilElementIsVisible(Driver, _cartItemTitle);
                return ElementExists(_cartItemTitle);
            }
            catch
            {
                return false;
            }
        }

        public bool IsCorrectProductInCart()
        {
            var titleElement = WaitHelper.WaitUntilElementIsVisible(Driver, _cartItemTitle);
            string titleText = titleElement.Text.Trim();

            return titleText.Equals(Constants.BookNameCart);
        }
    }
}
