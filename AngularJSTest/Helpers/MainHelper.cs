
namespace AngularJSTest.Helpers
{
    using AngularJSTest.Core;
    using AngularJSTest.Service;

    /// <summary>
    /// The main helper.
    /// </summary>
    public class MainHelper : HelperTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainHelper"/> class.
        /// </summary>
        /// <param name="app">
        /// The application app.
        /// </param>
        public MainHelper(ApplicationManager app)
            : base(app)
        {
        }

        /// <summary>
        /// The click footer link.
        /// </summary>
        /// <param name="linkText">
        /// The link text.
        /// </param>
        public void ClickFooterLink(string linkText)
        {
            this.App.Pages.HomePage.Footer.ClickFooterLink(linkText);
        }

        /// <summary>
        /// The get page url.
        /// </summary>
        /// <returns>
        /// <see cref="string"/>.
        /// </returns>
        public string GetPageUrl()
        {
            return this.App.Pages.Driver.Url;
        }
    }
}
