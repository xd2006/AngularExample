
namespace AngularJSTest.Pages.Components
{
    using System.Linq;

    using OpenQA.Selenium;

    /// <summary>
    /// The footer.
    /// </summary>
    public class Footer : ComponentTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Footer"/> class.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        public Footer(IWebDriver driver)
            : base(driver)
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
            this.Driver.FindElements(By.XPath("//footer[@id='info']//a")).First(e => e.Text.Equals(linkText)).Click();
        }
    }
}
