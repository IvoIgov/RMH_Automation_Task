using AmazonAutomation.Pages;
using NUnit.Framework;
using Allure.NUnit.Attributes;
using Allure.Net.Commons;

namespace AmazonAutomation.Tests
{
    [TestFixture]
    [AllureSuite("Amazon Tests")]

    public class AddProductToCartTest : TestBase
    {
        private HomePage _homePage;

        [SetUp]
        public void TestSetUp()
        {
            _homePage = new HomePage(Driver);
        }

        [Test]
        [AllureName("AddBookToCartWorkflow")]
        [AllureFeature("Product Search")]
        [AllureStory("Search and add exact product")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("IvoIgov")]
        [AllureTag("Smoke", "Regression")]
        public void AddBookToCartWorkflow()
        {
            _homePage.GoToHomePage();
            _homePage.WaitForPageLoad();

            var searchResultsPage = _homePage.SearchForConfiguredBook();
            searchResultsPage.WaitForPageLoad();

            var productPage = searchResultsPage.SelectConfiguredBook();
            productPage.WaitForPageLoad();

            productPage.SelectHardcover()
                       .AddToCart();

            var cartPage = productPage.GoToCart();

            Assert.That(cartPage.IsProductAdded(), Is.True, "Product was not added to the cart");
            Assert.That(cartPage.IsCorrectProductInCart(), Is.True, "The product in the cart is not correct");
        }
    }
}
