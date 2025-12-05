using AmazonAutomation.Config;
using AmazonAutomation.Utilities;
using OpenQA.Selenium;

namespace AmazonAutomation.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver) { }

        private readonly By _searchBox = By.Id("twotabsearchtextbox");
        private readonly By _searchSubmitButton = By.Id("nav-search-submit-button");
        private readonly By _continueShoppingButton = By.CssSelector("button[alt='Continue shopping']");

        private void HandleContinueShoppingPopup()
        {
            try
            {
                ClickElementIfExists(_continueShoppingButton);
            }
            catch
            {
            }
        }

        public HomePage GoToHomePage()
        {
            NavigateTo(Constants.AmazonUrl);
            HandleContinueShoppingPopup();
            return this;
        }

        public void WaitForPageLoad()
        {
            WaitHelper.WaitUntilElementIsVisible(Driver, _searchBox);
        }

        public SearchResultsPage SearchForBook(string bookName)
        {
            EnterText(_searchBox, bookName);
            ClickElement(_searchSubmitButton);

            return new SearchResultsPage(Driver);
        }

        public SearchResultsPage SearchForConfiguredBook()
        {
            return SearchForBook(Constants.BookName);
        }
    }
}
