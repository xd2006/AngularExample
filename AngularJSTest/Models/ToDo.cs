
namespace AngularJSTest.Models
{
    using OpenQA.Selenium;

    /// <summary>
    /// The to do item.
    /// </summary>
    public class ToDo
    {      
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
        /// The equals.
        /// </summary>
        /// <param name="obj">
        /// The object
        /// </param>
        /// <returns>
        /// If equals <see cref="bool"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((ToDo)obj);
        }

        /// <summary>
        /// The get hash code.
        /// </summary>
        /// <returns>
        /// HashCode <see cref="int"/>.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.Name != null ? this.Name.GetHashCode() : 0) * 397) ^ this.Completed.GetHashCode();
            }
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// If equals <see cref="bool"/>.
        /// </returns>
        protected bool Equals(ToDo other)
        {
            return string.Equals(this.Name, other.Name) && this.Completed == other.Completed;
        }
    }
}
