
using System.IO;

namespace AngularJSTest.Core.AuxManagers
{
    using System;

    using AngularJSTest.Pages;

    using OpenQA.Selenium;

    public class PageManager
    {
        /// <summary>
        /// The driver.
        /// </summary>
        private IWebDriver driver;

        /// <summary>
        /// The base url.
        /// </summary>
        private string BaseUrl;

        /// <summary>
        /// The home page.
        /// </summary>
        private HomePage homePage;

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
            new WebDriverManager().SetupDriver(capabilities.BrowserName);
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

        public IWebDriver Driver
        {
            get
            {
                return this.driver;
            }

            set
            {
                this.driver = value;
            }
        }

        #region pages declaration

        /// <summary>
        /// The home page.
        /// </summary>
        public HomePage HomePage => this.homePage ?? (this.homePage = new HomePage(this.Driver));

        #endregion

        /// <summary>
        /// The navigate to base url.
        /// </summary>
        public void NavigateToBaseUrl()
        {
            this.Driver.Navigate().GoToUrl(this.BaseUrl);
        }
    }
}
