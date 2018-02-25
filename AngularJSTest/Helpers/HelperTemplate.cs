
namespace AngularJSTest.Helpers
{
    using AngularJSTest.Core;

    public abstract class HelperTemplate
    {
        /// <summary>
        /// The manager.
        /// </summary>
        protected ApplicationManager Manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelperTemplate"/> class.
        /// </summary>
        /// <param name="manager">
        /// The manager.
        /// </param>
        public HelperTemplate(ApplicationManager manager)
        {
            this.Manager = manager;
        }


    }
}
