
namespace AngularJSTest.Pages.Components
{
    using OpenQA.Selenium;

    /// <summary>
    /// The to dos widget.
    /// </summary>
    public class ToDosWidget : ComponentTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToDosWidget"/> class.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        public ToDosWidget(IWebDriver driver)
            : base(driver)
        {
        }
    }
}
