using OpenQA.Selenium;

namespace AngularJSTest.Core
{
    using System;
    using System.Collections.Specialized;

    using OpenQA.Selenium.Remote;

    /// <summary>
    /// The starter.
    /// </summary>
    public class Starter
    {
        /// <summary>
        /// The application settings.
        /// </summary>
        private static readonly NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        /// <summary>
        /// The start application manager.
        /// </summary>
        /// <returns>
        /// The <see cref="ApplicationManager"/>.
        /// </returns>
        public ApplicationManager StartApplicationManager()
        {
            // Initially try to get values from env variables and then form app.config
            var browser = Environment.GetEnvironmentVariable("BROWSER") ?? appSettings["Browser"] ?? "chrome";
            var baseUrl = Environment.GetEnvironmentVariable("URL") ?? appSettings["Url"];
            var hubUrl = Environment.GetEnvironmentVariable("HUBURL") ?? appSettings["HubUrl"];

            var capabilities = DefineCapabilities(browser);
      
            var app = new ApplicationManager(capabilities, baseUrl, hubUrl);
            return app;
        }

        /// <summary>
        /// The define capabilities.
        /// </summary>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <returns>
        /// The <see cref="DesiredCapabilities"/>.
        /// </returns>
        private static DesiredCapabilities DefineCapabilities(string browser)
        {
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability(CapabilityType.BrowserName, browser);
            return capabilities;
        }
    }
}
