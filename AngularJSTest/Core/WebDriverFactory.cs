using System;
using System.Collections.Generic;

namespace AngularJSTest.Core
{
    using System.Threading;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.PhantomJS;
    using OpenQA.Selenium.Remote;
    using OpenQA.Selenium.Safari;

    /// <summary>
    /// The web driver factory.
    /// </summary>
    public class WebDriverFactory
    {
        /// <summary>
        /// The instance.
        /// </summary>
        private static WebDriverFactory instance;

        /// <summary>
        /// The thread local driver.
        /// </summary>
        private static ThreadLocal<IWebDriver> threadLocalDriver = new ThreadLocal<IWebDriver>();

        /// <summary>
        /// The driver to key map.
        /// </summary>
        private Dictionary<IWebDriver, string> driverToKeyMap = new Dictionary<IWebDriver, string>();

        /// <summary>
        /// The get driver.
        /// </summary>
        /// <param name="hub">
        /// The hub.
        /// </param>
        /// <param name="capabilities">
        /// The capabilities.
        /// </param>
        /// <returns>
        /// The <see cref="IWebDriver"/>.
        /// </returns>
        public static IWebDriver GetDriver(string hub, ICapabilities capabilities)
        {
            return FactoryInstance.__GetDriver(hub, capabilities);
        }

        /// <summary>
        /// The get driver.
        /// </summary>
        /// <param name="capabilities">
        /// The capabilities.
        /// </param>
        /// <returns>
        /// The <see cref="IWebDriver"/>.
        /// </returns>
        public static IWebDriver GetDriver(ICapabilities capabilities)
        {
            return FactoryInstance.__GetDriver(capabilities);
        }

        /// <summary>
        /// The dismiss driver.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        public static void DismissDriver(IWebDriver driver)
        {
            FactoryInstance.__DismissDriver(driver);
        }

        /// <summary>
        /// The dismiss all.
        /// </summary>
        public static void DismissAll()
        {
            FactoryInstance.__DismissAll();
        }

        /// <summary>
        /// Gets the factory instance.
        /// </summary>
        public static WebDriverFactory FactoryInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WebDriverFactory();
                }
                return instance;
            }
        }

        /// <summary>
        /// The __ get driver.
        /// </summary>
        /// <param name="hub">
        /// The hub.
        /// </param>
        /// <param name="capabilities">
        /// The capabilities.
        /// </param>
        /// <returns>
        /// The <see cref="IWebDriver"/>.
        /// </returns>
        private IWebDriver __GetDriver(string hub, ICapabilities capabilities)
        {
            string newKey = CreateKey(capabilities, hub);

            if (!threadLocalDriver.IsValueCreated)
            {
                CreateNewDriver(capabilities, hub);
            }
            else
            {
                IWebDriver currentDriver = threadLocalDriver.Value;
                string currentKey = null;
                if (!driverToKeyMap.TryGetValue(currentDriver, out currentKey))
                {
                    // The driver was dismissed
                    CreateNewDriver(capabilities, hub);
                }
                else
                {
                    if (newKey != currentKey)
                    {
                        // A different flavour of WebDriver is required
                        __DismissDriver(currentDriver);
                        CreateNewDriver(capabilities, hub);
                    }
                    else
                    {
                        // Check the browser is alive
                        try
                        {
                            string currentUrl = currentDriver.Url;
                        }
                        catch (WebDriverException)
                        {
                            CreateNewDriver(capabilities, hub);
                        }
                    }
                }
            }
            return threadLocalDriver.Value;
        }

        /// <summary>
        /// The __ get driver.
        /// </summary>
        /// <param name="capabilities">
        /// The capabilities.
        /// </param>
        /// <returns>
        /// The <see cref="IWebDriver"/>.
        /// </returns>
        private IWebDriver __GetDriver(ICapabilities capabilities)
        {
            return __GetDriver(null, capabilities);
        }

        /// <summary>
        /// The __ dismiss driver.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        private void __DismissDriver(IWebDriver driver)
        {
            if (!driverToKeyMap.ContainsKey(driver))
            {
                throw new Exception("The driver is not owned by the factory: " + driver);
            }
            if (driver != threadLocalDriver.Value)
            {
                throw new Exception("The driver does not belong to the current thread: " + driver);
            }
            driver.Quit();
            driverToKeyMap.Remove(driver);
            threadLocalDriver.Dispose();
        }

        /// <summary>
        /// The __ dismiss all.
        /// </summary>
        private void __DismissAll()
        {
            foreach (IWebDriver driver in new List<IWebDriver>(driverToKeyMap.Keys))
            {
                driver.Quit();
                driverToKeyMap.Remove(driver);
            }
        }

        /// <summary>
        /// The create key.
        /// </summary>
        /// <param name="capabilities">
        /// The capabilities.
        /// </param>
        /// <param name="hub">
        /// The hub.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected static string CreateKey(ICapabilities capabilities, string hub)
        {
            return capabilities.ToString() + ":" + hub;
        }

        /// <summary>
        /// The create new driver.
        /// </summary>
        /// <param name="capabilities">
        /// The capabilities.
        /// </param>
        /// <param name="hub">
        /// The hub.
        /// </param>
        private void CreateNewDriver(ICapabilities capabilities, string hub)
        {
            string newKey = CreateKey(capabilities, hub);
            IWebDriver driver = (hub == null)
                ? CreateLocalDriver(capabilities)
                : CreateRemoteDriver(hub, capabilities);
            driverToKeyMap.Add(driver, newKey);
            threadLocalDriver.Value = driver;
        }

        /// <summary>
        /// The create remote driver.
        /// </summary>
        /// <param name="hub">
        /// The hub.
        /// </param>
        /// <param name="capabilities">
        /// The capabilities.
        /// </param>
        /// <returns>
        /// The <see cref="IWebDriver"/>.
        /// </returns>
        private static IWebDriver CreateRemoteDriver(string hub, ICapabilities capabilities)
        {
            return new RemoteWebDriver(new Uri(hub), capabilities);
        }

        /// <summary>
        /// The create local driver.
        /// </summary>
        /// <param name="capabilities">
        /// The capabilities.
        /// </param>
        /// <returns>
        /// The <see cref="IWebDriver"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        private static IWebDriver CreateLocalDriver(ICapabilities capabilities)
        {
            // Implementation is incomplete: the capabilities are not converted to the options for all browsers
            string browserType = capabilities.BrowserName;
            if (browserType == DesiredCapabilities.Firefox().BrowserName)
            {
                return new FirefoxDriver(capabilities);
            }

            if (browserType == DesiredCapabilities.InternetExplorer().BrowserName)
            {
                return new InternetExplorerDriver();
            }

            if (browserType == DesiredCapabilities.Chrome().BrowserName)
            {
                return new ChromeDriver();
            }

            if (browserType == DesiredCapabilities.Safari().BrowserName)
            {
                return new SafariDriver();
            }

            if (browserType == DesiredCapabilities.PhantomJS().BrowserName)
            {
                return new PhantomJSDriver();
            }

            throw new Exception("Unrecognized browser type: " + browserType);
        }
    }
}
