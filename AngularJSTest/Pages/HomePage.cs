namespace AngularJSTest.Pages
{
    using AngularJSTest.Pages.Components;

    using OpenQA.Selenium;

    /// <summary>
    /// The main page.
    /// </summary>
    public class HomePage : PageTemplate
    {
        /// <summary>
        /// The footer.
        /// </summary>
        private Footer footer;

        /// <summary>
        /// The to dos widget.
        /// </summary>
        private ToDosWidget toDosWidget;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomePage"/> class.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        public HomePage(IWebDriver driver)
            : base(driver)
        {
        }

        /// <summary>
        /// The footer.
        /// </summary>
        public Footer Footer => this.footer ?? (this.footer = new Footer(this.Driver));

        /// <summary>
        /// The to dos widget.
        /// </summary>
        public ToDosWidget ToDosWidget => this.toDosWidget ?? (this.toDosWidget = new ToDosWidget(this.Driver));



    }
}
