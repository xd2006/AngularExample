
namespace AngularJSTest.Pages.Components
{
    using OpenQA.Selenium;

    /// <summary>
    /// The component template.
    /// </summary>
    public class ComponentTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentTemplate"/> class.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        public ComponentTemplate(IWebDriver driver)
        {
            this.Driver = driver;
        }

        /// <summary>
        /// Gets the driver.
        /// </summary>
        protected IWebDriver Driver { get; }
    }
}
