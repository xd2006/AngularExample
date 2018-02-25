
namespace AngularJSTest.StepsDefinitions
{
    using System;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;

    using AngularJSTest.Core;

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
        protected static ApplicationManager App => appM ?? (appM = new Starter().StartApplicationManager());

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
            string driveCandidates = "CDEFGHF";
            var validDrive = driveCandidates.Select(c => $"{c}:").FirstOrDefault(Directory.Exists);

            return $"{validDrive}\\{DateTime.Now:ddMM}";
        }
    }
}
