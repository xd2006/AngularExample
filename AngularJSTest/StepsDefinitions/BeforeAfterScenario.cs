
namespace AngularJSTest.Tests
{
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Threading;

    using AngularJSTest.Core;
    using AngularJSTest.Service.Logger.NLogTemplates;
    using AngularJSTest.StepsDefinitions;
    using NLog;
    using TechTalk.SpecFlow;

    /// <summary>
    /// The before after scenario.
    /// </summary>
    [Binding]
    public class BeforeAfterScenario : StepsTemplate
    {
        private static readonly NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;


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
            App.Logger.Info("Test scenario finished - " + ScenarioContext.Current.ScenarioInfo.Title);
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
