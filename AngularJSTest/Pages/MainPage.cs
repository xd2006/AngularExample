namespace AngularJSTest.Pages
{
    using OpenQA.Selenium;

    /// <summary>
    /// The main page.
    /// </summary>
    public class MainPage : PageTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        public MainPage(IWebDriver driver)
            : base(driver)
        {
        }
    }
}
