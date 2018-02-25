
namespace AngularJSTest.Tests
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Threading;

    using AngularJSTest.Core;
    using AngularJSTest.Service.Logger.NLogTemplates;
    using AngularJSTest.StepsDefinitions;
    using NLog;

    using OpenQA.Selenium;

    using TechTalk.SpecFlow;

    /// <summary>
    /// The before after scenario.
    /// </summary>
    [Binding]
    public class BeforeAfterScenario : StepsTemplate
    {
        /// <summary>
        /// The before test fixture.
        /// </summary>
        [BeforeTestRun]
        public static void BeforeTest()
        {
            App.Logger = LogManager.GetCurrentClassLogger();
            App.Logger.Info("Test run started");
        }

        /// <summary>
        /// The before scenario fixture.
        /// </summary>
        [BeforeScenario]
        public static void Before()
        {
            App.Logger.Info("Test scenario started - " + ScenarioContext.Current.ScenarioInfo.Title);
        }

        /// <summary>
        /// The before scenario fixture.
        /// </summary>
        [AfterScenario]
        public static void After()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                TakeScreenshot(App.Pages.Driver, ScenarioContext.Current, DefaultLogFolder());
            }

            App.Logger.Info("Test scenario finished - " + ScenarioContext.Current.ScenarioInfo.Title);
            App.Pages.NavigateToBaseUrl();
        }
        

        /// <summary>
        /// The after test fixture.
        /// </summary>
        [AfterTestRun]
        public static void AfterTest()
        {
            WebDriverFactory.DismissAll();
            Thread.Sleep(3000);
            App.Logger.Info("Test run finished");
        }
    }
}
