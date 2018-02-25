namespace AngularJSTest.Core
{
    using AngularJSTest.Core.AuxManagers;
    using AngularJSTest.Helpers;

    using NLog;

    using OpenQA.Selenium.Remote;

    /// <summary>
    /// The application manager.
    /// </summary>
    public class ApplicationManager
    {
        /// <summary>
        /// The base url.
        /// </summary>
        private string baseUrl;

        /// <summary>
        /// The capabilities.
        /// </summary>
        private DesiredCapabilities capabilities;

        /// <summary>
        /// The hub url.
        /// </summary>
        private string hubUrl;

        /// <summary>
        /// The _pages.
        /// </summary>
        private PageManager pages;

        /// <summary>
        /// The main.
        /// </summary>
        private MainHelper main;

        /// <summary>
        /// Gets or sets the NLog logger.
        /// </summary>
        public Logger Logger { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationManager"/> class.
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
        public ApplicationManager(DesiredCapabilities capabilities, string baseUrl, string hubUrl)
        {
            this.capabilities = capabilities;
            this.baseUrl = baseUrl;
            this.hubUrl = hubUrl;
        }

        /// <summary>
        /// Gets the pages.
        /// </summary>
        public PageManager Pages => this.pages ?? (this.pages = new PageManager(this.capabilities, this.baseUrl, this.hubUrl));

        /// <summary>
        /// The main helper.
        /// </summary>
        public MainHelper Main => this.main ?? (this.main = new MainHelper(this));
    }
}
