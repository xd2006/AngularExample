
namespace AngularJSTest.StepsDefinitions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Threading;

    using AngularJSTest.Core;

    using NLog;

    using OpenQA.Selenium;

    using TechTalk.SpecFlow;

    /// <summary>
    /// The steps template.
    /// </summary>
    public class StepsTemplate
    {
        /// <summary>
        /// The app manager
        /// </summary>
        private static ApplicationManager appM;

        /// <summary>
        /// Gets the app. manager
        /// </summary>
        protected static ApplicationManager App
        {
            get
            {
                appM = appM ?? new Starter().StartApplicationManager();
                appM.Logger = LogManager.GetCurrentClassLogger();
                return appM;
            }
            set
            {
                appM = value;
            }
        }

        /// <summary>
        /// The take screenshot.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="logFolder">
        /// The log folder.
        /// </param>
        protected static void TakeScreenshot(IWebDriver driver, ScenarioContext context, string logFolder)
        {
            try
            {
                string fileNameBase = $"{context.ScenarioInfo.Title}_{DateTime.Now:yyyy-MM-dd_HHmmss}";
                string folderNameBase = $"{context.ScenarioInfo.Title}_{DateTime.Now:yyyy-MM-dd_HHmm}";

                string artifactDirectory = Path.GetFullPath(Path.Combine(logFolder, folderNameBase));

                if (!Directory.Exists(artifactDirectory))
                {
                    Directory.CreateDirectory(artifactDirectory);
                }

                ITakesScreenshot takesScreenshot = driver as ITakesScreenshot;

                if (takesScreenshot != null)
                {
                    var screenshot = takesScreenshot.GetScreenshot();

                    string screenshotFilePath = Path.Combine(artifactDirectory, fileNameBase + "_screenshot.png");

                    screenshot.SaveAsFile(screenshotFilePath, ScreenshotImageFormat.Png);

                    App.Logger.Info("Screenshot: {0}", new Uri(screenshotFilePath));
                }
            }
            catch (Exception ex)
            {
                App.Logger.Error(ex, "Error while taking screenshot");
            }
        }

        /// <summary>
        /// The default log folder.
        /// </summary>
        /// <returns>
        /// path to log folder<see cref="string"/>.
        /// </returns>
        protected static string DefaultLogFolder()
        {
            string driveCandidates = "CDEFGH";
            string validDrive = driveCandidates.Select(c => $"{c}:").FirstOrDefault(Directory.Exists);

            NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            string folder = appSettings["LogFolder"] ?? string.Empty;

            return $"{validDrive}\\{folder}\\{DateTime.Now:ddMM}";
        }

        /// <summary>
        /// Set scenario context parameter.
        /// </summary>
        /// <param name="parameterName">
        /// The parameter name.
        /// </param>
        /// <param name="parameterValue">
        /// The parameter value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public void SetScenarioContextParameter<T>(string parameterName, T parameterValue)
        {
//            try
//            {
//                ScenarioContext.Current.Get<T>(parameterName);
                ScenarioContext.Current[parameterName] = parameterValue;
//            }
//            catch (KeyNotFoundException e)
//            {
//                ScenarioContext.Current.Add(parameterName, parameterValue);
//            }

        }

        /// <summary>
        /// Get scenario context parameter.
        /// </summary>
        /// <param name="parameterName">
        /// The parameter name.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T GetScenarioContextParameter<T>(string parameterName)
        {
            T parameter = default(T);
            try
            {
                parameter = ScenarioContext.Current.Get<T>(parameterName);
            }
            catch (KeyNotFoundException e)
            {
                SetScenarioContextParameter(parameterName, parameter);
                parameter = ScenarioContext.Current.Get<T>(parameterName);
            }

            return parameter;

        }

        public static void Clean()
        {
            WebDriverFactory.DismissAll();
            App = null;
            Thread.Sleep(2000);
        }
    }
}
