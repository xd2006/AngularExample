
namespace AngularJSTest.Service
{
    using System;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// The webdriver extensions.
    /// </summary>
    public static class DriverExtensions
    {
        /// <summary>
        /// The wait for page ready.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        /// <param name="timeoutSeconds">
        /// The timeout Seconds.
        /// </param>
        public static void WaitForPageReady(this IWebDriver driver, int timeoutSeconds = 30)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));

            // Check if document is ready
            try
            {
                wait.Until(CustomExpectedConditions.PageReadyCondition);
            }
            catch (WebDriverTimeoutException e)
            {
                throw new Exception(e.Message + " .Page can't be completely loaded");
            }
        }

        /// <summary>
        /// The double click.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        /// <param name="locator">
        /// The locator.
        /// </param>
        public static void DoubleClick(this IWebDriver driver, By locator)
        {
            Actions action = new Actions(driver);
            action.DoubleClick(driver.FindElement(locator)).Perform();
        }

        /// <summary>
        /// The double click.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        /// <param name="element">
        /// The element.
        /// </param>
        public static void DoubleClick(this IWebDriver driver, IWebElement element)
        {
            Actions action = new Actions(driver);
            action.DoubleClick(element).Perform();
        }

        /// <summary>
        /// The hover element.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        /// <param name="locator">
        /// The locator.
        /// </param>
        public static void HoverElement(this IWebDriver driver, By locator)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(locator)).Perform();
        }

        /// <summary>
        /// The hover element.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        /// <param name="element">
        /// The element.
        /// </param>
        public static void HoverElement(this IWebDriver driver, IWebElement element)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform();
        }

        /// <summary>
        /// The hover element and click.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        /// <param name="element">
        /// The element.
        /// </param>
        public static void HoverElementAndClick(this IWebDriver driver, IWebElement element)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(element).Click().Perform();
        }

        /// <summary>
        /// Disable implicit timeout.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        public static void DisableTimeout(this IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        }

        /// <summary>
        /// Set implicit timeout.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        /// <param name="timeoutSec">
        /// The timeout sec.
        /// </param>
        public static void SetTimeout(this IWebDriver driver, int timeoutSec = 10)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutSec);
        }
    }
}
