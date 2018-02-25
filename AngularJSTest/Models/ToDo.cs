
namespace AngularJSTest.Models
{
    using OpenQA.Selenium;

    /// <summary>
    /// The to do item.
    /// </summary>
    public class ToDo
    {
        /// <summary>
        /// Gets or sets the parent element.
        /// </summary>
        public IWebElement ParentElement { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets completed.
        /// </summary>
        public bool Completed { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToDo"/> class.
        /// </summary>
        /// <param name="parentElement">
        /// The parent element.
        /// </param>
        public ToDo(IWebElement parentElement)
        {
            this.ParentElement = parentElement;
        }
    }
}
