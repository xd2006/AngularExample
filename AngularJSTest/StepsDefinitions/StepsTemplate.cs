
namespace AngularJSTest.StepsDefinitions
{
    using AngularJSTest.Core;

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
    }
}
