
namespace AngularJSTest.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AngularJSTest.Core;
    using AngularJSTest.Models;
    using AngularJSTest.Service;

    /// <summary>
    /// The main helper.
    /// </summary>
    public class MainHelper : HelperTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainHelper"/> class.
        /// </summary>
        /// <param name="app">
        /// The application app.
        /// </param>
        public MainHelper(ApplicationManager app)
            : base(app)
        {
        }

        /// <summary>
        /// The click footer link.
        /// </summary>
        /// <param name="linkText">
        /// The link text.
        /// </param>
        public void ClickFooterLink(string linkText)
        {
            this.App.Pages.HomePage.Footer.ClickFooterLink(linkText);
        }

        /// <summary>
        /// The get page url.
        /// </summary>
        /// <returns>
        /// <see cref="string"/>.
        /// </returns>
        public string GetPageUrl()
        {
            return this.App.Pages.Driver.Url;
        }

        /// <summary>
        /// The add new to do item.
        /// </summary>
        /// <param name="itemName">
        /// The item name.
        /// </param>
        public void AddNewToDoItem(string itemName)
        {
            App.Pages.HomePage.ToDosWidget.AddNewToDoItem(itemName);
        }

        /// <summary>
        /// The get to do item.
        /// </summary>
        /// <param name="itemName">
        /// The item name.
        /// </param>
        /// <returns>
        /// The <see cref="ToDo"/>.
        /// </returns>
        public ToDo GetToDoItem(string itemName)
        {
            return App.Pages.HomePage.ToDosWidget.GetToDos().First(e => e.Name.Equals(itemName));
        }

        /// <summary>
        /// The create items if needed.
        /// </summary>
        /// <param name="numberOfItems">
        /// The number of items.
        /// </param>
        public void CreateItemsIfNeeded(int numberOfItems)
        {
            var toDos = App.Pages.HomePage.ToDosWidget.GetToDos();

            if (toDos.Count < numberOfItems)
            {
                for (int i = 1; i <= numberOfItems - toDos.Count; i++)
                {
                   string itemName = "item" + DateTime.Now.Second + DateTime.Now.Millisecond;
                   App.Pages.HomePage.ToDosWidget.AddNewToDoItem(itemName); 
                }
            }
        }

        /// <summary>
        /// Remove number of any items.
        /// </summary>
        /// <param name="numberOfItems">
        /// The number Of Items.
        /// </param>
        /// <returns>
        /// Removed items names<see cref="List"/>.
        /// </returns>
        public List<string> RemoveAnyItems(int numberOfItems = 1)
        {
            var itemsList = this.GetToDoItemsNamesList();
            var randomNumbers = ServiceMethods.GetRandomNumbers(0, itemsList.Count - 1, numberOfItems).ToList();
            List<string> removedItems = new List<string>();
            foreach (var index in randomNumbers)
            {
                var name = itemsList[index];
                App.Pages.HomePage.ToDosWidget.RemoveToDoItem(name);
                removedItems.Add(name);
            }
            
            return removedItems;
        }

        /// <summary>
        /// The get to do items names list.
        /// </summary>
        /// <returns>
        /// The names list <see cref="List"/>.
        /// </returns>
        public List<string> GetToDoItemsNamesList()
        {
            return App.Pages.HomePage.ToDosWidget.GetToDos().Select(e => e.Name).ToList();
        }
    }
}
