using AmazonAutomation.Config;
using AmazonAutomation.Utilities;
using OpenQA.Selenium;

namespace AmazonAutomation.Pages
{
    public class SearchResultsPage : BasePage
    {
        private readonly By _resultsContainer = By.CssSelector("div.s-main-slot");

        public SearchResultsPage(IWebDriver driver) : base(driver) { }

        public void WaitForPageLoad()
        {
            WaitHelper.WaitUntilElementIsVisible(Driver, _resultsContainer);
        }

        private By ExactBookLocator(string bookName)
        {
            return By.XPath($"(//div[@data-component-type='s-search-result']//h2//span[normalize-space()='{bookName}'])[1]");
        }

        public ProductPage SelectExactBook(string bookName)
        {
            var locator = ExactBookLocator(bookName);

            // Wait until at least one exact match is clickable
            var element = WaitHelper.WaitUntilElementIsClickable(Driver, locator);

            if (element == null)
                throw new NoSuchElementException($"No visible results found for '{bookName}'");

            element.Click();

            return new ProductPage(Driver);
        }

        public ProductPage SelectConfiguredBook()
        {
            return SelectExactBook(Constants.BookName);
        }
    }
}
