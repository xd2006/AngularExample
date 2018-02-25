namespace AngularJSTest.Pages
{
    using OpenQA.Selenium;

    /// <summary>
    /// The page template.
    /// </summary>
    public abstract class PageTemplate
    {
        /// <summary>
        /// The driver.
        /// </summary>
        private IWebDriver driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageTemplate"/> class.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        protected PageTemplate(IWebDriver driver)
        {
            this.driver = driver;
        }

        /// <summary>
        /// The driver.
        /// </summary>
        protected IWebDriver Driver => this.driver;
    }
}
