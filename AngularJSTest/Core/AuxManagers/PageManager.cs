
namespace AngularJSTest.Core.AuxManagers
{
    using System;

    using OpenQA.Selenium;

    public class PageManager
    {
        /// <summary>
        /// The driver.
        /// </summary>
        public IWebDriver Driver;

        /// <summary>
        /// The base url.
        /// </summary>
        protected string BaseUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageManager"/> class.
        /// </summary>
        /// <param name="capabilities">
        /// The capabilities.
        /// </param>
        /// <param name="baseUrl">
        /// The base url.
        /// </param>
        /// <param name="hubUrl">
        /// The hub url.
        /// </param>
        public PageManager(ICapabilities capabilities, string baseUrl, string hubUrl)
        {
            Driver = WebDriverFactory.GetDriver(hubUrl, capabilities);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            Driver.Manage().Window.Maximize();
            if (!Driver.Url.StartsWith(baseUrl))
            {
                Driver.Navigate().GoToUrl(baseUrl);
            }

            this.BaseUrl = baseUrl;
        }

        public void NavigateToBaseUrl()
        {
            this.Driver.Navigate().GoToUrl(this.BaseUrl);
        }


    }
}
