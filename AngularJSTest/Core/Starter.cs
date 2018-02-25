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
        private readonly NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        /// <summary>
        /// The start application manager.
        /// </summary>
        /// <returns>
        /// The <see cref="ApplicationManager"/>.
        /// </returns>
        public ApplicationManager StartApplicationManager()
        {
            // Initially try to get values from env variables and then form app.config
            var browser = Environment.GetEnvironmentVariable("BROWSER") ?? this.appSettings["Browser"] ?? "chrome";
            var baseUrl = Environment.GetEnvironmentVariable("URL") ?? this.appSettings["Url"];
            var hubUrl = Environment.GetEnvironmentVariable("HUBURL") ?? this.appSettings["HubUrl"];

            var capabilities = this.DefineCapabilities(browser);
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
        private DesiredCapabilities DefineCapabilities(string browser)
        {
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability(CapabilityType.BrowserName, browser);
            return capabilities;
        }
    }
}
