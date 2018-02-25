
namespace AngularJSTest.Helpers
{
    using AngularJSTest.Core;

    /// <summary>
    /// The helper template.
    /// </summary>
    public abstract class HelperTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelperTemplate"/> class.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        protected HelperTemplate(ApplicationManager app)
        {
            this.App = app;
        }

        /// <summary>
        /// Gets app. manager
        /// </summary>
        public ApplicationManager App { get; }
    }
}
