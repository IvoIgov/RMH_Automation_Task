using AmazonAutomation.Utilities;
using OpenQA.Selenium;

namespace AmazonAutomation.Pages
{
    public class ProductPage : BasePage
    {
        private readonly By _hardcoverOption = By.XPath("//span[text()='Hardcover']");
        private readonly By _addToCartButton = By.Id("add-to-cart-button");
        private readonly By _cartConfirmationMessage = By.Id("NATC_SMART_WAGON_CONF_MSG_SUCCESS");
        private readonly By _viewCartButton = By.Id("sw-gtc");
        private readonly By _productTitle = By.Id("productTitle");

        public ProductPage(IWebDriver driver) : base(driver) { }

        public void WaitForPageLoad()
        {
            WaitHelper.WaitUntilElementIsVisible(Driver, _productTitle);
        }

        public ProductPage SelectHardcover()
        {
            try
            {
                ClickElement(_hardcoverOption);
            }
            catch
            {
            }

            return this;
        }

        public ProductPage AddToCart()
        {
            ClickElement(_addToCartButton);
            return this;
        }

        public bool IsItemAddedToCart()
        {
            return ElementExists(_cartConfirmationMessage);
        }

        public CartPage GoToCart()
        {
            try
            {
                ClickElement(_viewCartButton);
            }
            catch
            {
            }

            return new CartPage(Driver);
        }
    }
}
