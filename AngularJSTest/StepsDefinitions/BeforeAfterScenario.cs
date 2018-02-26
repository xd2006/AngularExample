
namespace AngularJSTest.Tests
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Linq.Expressions;
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
        /// The after scenario fixture.
        /// </summary>
        [AfterScenario]
        public static void After()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                TakeScreenshot(App.Pages.Driver, ScenarioContext.Current, DefaultLogFolder());
            }

            App.Logger.Info("Test scenario finished - " + ScenarioContext.Current.ScenarioInfo.Title);

            // to avoid driver freezing after changing filters
            if (ScenarioContext.Current.ScenarioInfo.Tags[0].Contains("end2end"))
            {
                Clean();
            }
            else
            {
                try
                {
                    App.Pages.NavigateToBaseUrl();
                }
                catch (Exception e)
                {
                    Clean();
                }
            }
        }

        /// <summary>
        /// The after scenario fixture for security tests.
        /// </summary>
        [AfterScenario("security")]
        public static void AfterSecurity()
        {
            try
            {
                App.Pages.Driver.SwitchTo().Alert().Accept();
            }
            catch (NoAlertPresentException e)
            {
                // supress exception
            }          
        }

         /// <summary>
        /// The after test fixture.
        /// </summary>
        [AfterTestRun]
        public static void AfterTest()
        {
            Clean();
            App.Logger.Info("Test run finished");
        }
    }
}
