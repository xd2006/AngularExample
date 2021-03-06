﻿namespace AngularJSTest.Pages
{
    using OpenQA.Selenium;

    /// <summary>
    /// The page template.
    /// </summary>
    public abstract class PageTemplate
    {
      
        /// <summary>
        /// Initializes a new instance of the <see cref="PageTemplate"/> class.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        protected PageTemplate(IWebDriver driver)
        {
            this.Driver = driver;
        }

        /// <summary>
        /// Gets the driver.
        /// </summary>
        protected IWebDriver Driver { get; }
    }
}
