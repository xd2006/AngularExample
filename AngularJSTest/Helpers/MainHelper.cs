
namespace AngularJSTest.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AngularJSTest.Core;
    using AngularJSTest.Models;
    using AngularJSTest.Resources;
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
        /// Create items if needed.
        /// </summary>
        /// <param name="numberOfItems">
        /// The number of items.
        /// </param>
        public void CreateItemsIfNeeded(int numberOfItems)
        {
            var toDos = App.Pages.HomePage.ToDosWidget.GetActiveToDos();

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
            var randomNumbers = ServiceMethods.GetRandomNumbers(0, itemsList.Count - 1, numberOfItems);
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
        /// Remove all items.
        /// </summary>
        public void RemoveAllItems()
        {
            if (!this.GetToDoItemsNamesList().Count.Equals(0))
            {
                App.Pages.HomePage.ToDosWidget.CheckAllToDoItems();
                App.Pages.HomePage.ToDosWidget.ClickClearCompletedButton();
            }
        }

        /// <summary>
        /// Complete any items.
        /// </summary>
        /// <param name="numberOfItems">
        /// The number of items.
        /// </param>
        /// <returns>
        /// List of completed items names <see cref="List"/>.
        /// </returns>
        public List<string> CompleteAnyItems(int numberOfItems = 1)
        {
            var itemsList = App.Pages.HomePage.ToDosWidget.GetActiveToDos();
            var randomNumbers = ServiceMethods.GetRandomNumbers(0, itemsList.Count - 1, numberOfItems);
            List<string> completedItems = new List<string>();
            foreach (var index in randomNumbers)
            {
                var item = itemsList[index].Name;
                App.Pages.HomePage.ToDosWidget.CheckToDoItem(item);
                completedItems.Add(item);
            }

            return completedItems;
        }

        /// <summary>
        /// The rename any items.
        /// </summary>
        /// <param name="numberOfItems">
        /// The number of items.
        /// </param>
        /// <returns>
        /// original and new items names <see cref="List"/>.
        /// </returns>
        public List<Tuple<string, string>> RenameAnyItems(int numberOfItems = 1)
        {
            var itemsList = App.Pages.HomePage.ToDosWidget.GetToDos();
            var randomNumbers = ServiceMethods.GetRandomNumbers(0, itemsList.Count - 1, numberOfItems);

            List<Tuple<string, string>> renamedItems = new List<Tuple<string, string>>();
            foreach (var index in randomNumbers)
            {
                var originalItemName = itemsList[index].Name;

                string newItemName = "item" + DateTime.Now.Second + DateTime.Now.Millisecond + "updated";
                App.Pages.HomePage.ToDosWidget.EditItemsName(originalItemName, newItemName);
                renamedItems.Add(new Tuple<string, string>(originalItemName, newItemName));
            }

            App.Pages.Driver.WaitForPageReady();
            return renamedItems;
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

        /// <summary>
        /// The get active to do items names list.
        /// </summary>
        /// <returns>
        /// The names list <see cref="List"/>.
        /// </returns>
        public List<string> GetActiveToDoItemsNamesList()
        {
            return App.Pages.HomePage.ToDosWidget.GetActiveToDos().Select(e => e.Name).ToList();
        }

        /// <summary>
        /// The get completed to do items names list.
        /// </summary>
        /// <returns>
        /// The names list <see cref="List"/>.
        /// </returns>
        public List<string> GetCompletedToDoItemsNamesList()
        {
            return App.Pages.HomePage.ToDosWidget.GetCompletedToDos().Select(e => e.Name).ToList();
        }

        /// <summary>
        /// Get number of items displayed.
        /// </summary>
        /// <returns>
        /// The number of items<see cref="int"/>.
        /// </returns>
        public int GetNumberOfItems()
        {
            return App.Pages.HomePage.ToDosWidget.GetNumberOfItems();
        }

        /// <summary>
        /// Complete all available items.
        /// </summary>
        public void CompleteAllItems()
        {
            App.Pages.HomePage.ToDosWidget.CheckAllToDoItems();
        }

        /// <summary>
        /// Set filter.
        /// </summary>
        /// <param name="filterName">
        /// The filter name.
        /// </param>
        public void SetFilter(string filterName)
        {
            Enums.Filters filter = Enums.Filters.All;

            switch (filterName)
            {
                case "Completed":
                    filter = Enums.Filters.Completed;
                    break;
                case "Active":
                    filter = Enums.Filters.Active;
                    break;
            }

            App.Pages.HomePage.ToDosWidget.ClickFilter(filter);
        }
    }
}
