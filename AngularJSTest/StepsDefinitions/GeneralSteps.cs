

using System.IO;
using Bogus;

namespace AngularJSTest.StepsDefinitions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AngularJSTest.Service;

    using NUnit.Framework;

    using OpenQA.Selenium;

    using TechTalk.SpecFlow;

    [Binding]
    public class GeneralSteps : StepsTemplate
    {
        #region Given

        [Given(@"""(.*)"" to do items are created")]
        public void GivenSeveralToDoAreCreated(int numberOfItems)
        {
            App.Logger.Info($"Create {numberOfItems} to do items is needed");
            App.Main.CreateItemsIfNeeded(numberOfItems);
        }

        [Given(@"All items are removed")]
        public void GivenAllItemsAreRemoved()
        {
            App.Logger.Info("Removing all items");
            App.Main.RemoveAllItems();
        }

        #endregion

        #region When

        [When(@"I add new to do item (.*)")]
        public void WhenIAddNewToDoItemAlert(string itemName)
        {
            App.Logger.Info($"Adding new to do item {itemName}");
            App.Main.AddNewToDoItem(itemName);
        }

        [When(@"I add items")]
        public void WhenIAddItems(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                var itemName = row["item name"];
                App.Logger.Info($"Adding new to do item {itemName}");
                App.Main.AddNewToDoItem(itemName);
            }
        }


        [When(@"I add (.*) items")]
        public void WhenIAddNumberofItems(int itemsNumber)
        {
            List<string> addedItems = new List<string>();
            var faker = new Faker("en");

            for (int i = 0; i < itemsNumber; i++)
            {
                var itemName = i % 2==0 ? faker.Lorem.Word() + faker.Lorem.Slug(): faker.Hacker.Adjective() + " " + faker.Hacker.Noun();
                App.Logger.Info($"Adding new to do item {itemName}");
                App.Main.AddNewToDoItem(itemName);
                addedItems.Add(itemName);
             }
            SetScenarioContextParameter("Items", addedItems);

        }


        /// <summary>
        /// Remove any items and write their name to context variable 'RemovedItemsNames'.
        /// Writes initial items list to context - 'Items'
        /// </summary>
        [When(@"I remove ""(.*)"" items")]
        public void WhenIRemoveAnyItem(int numberOfItems)
        {
            App.Logger.Info("Removing any item");
            var itemsNames = App.Main.GetToDoItemsNamesList();
            var removedItems = App.Main.RemoveAnyItems(numberOfItems);
            this.SetScenarioContextParameter("RemovedItemsNames", removedItems);
            this.SetScenarioContextParameter("Items", itemsNames);
        }

        /// <summary>
        /// Complete any items and write their name to context variable 'CompletedItemsNames'.
        /// Writes initial active items list to context - 'ActiveItems'
        /// </summary>
        [When(@"I complete ""(.*)"" items")]
        public void WhenICompleteItems(int numberOfItems)
        {
            App.Logger.Info($"Make any {numberOfItems} items complete");
            var itemsNames = App.Main.GetActiveToDoItemsNamesList();
            var completedItems = App.Main.CompleteAnyItems(numberOfItems);
            this.SetScenarioContextParameter("CompletedItemsNames", completedItems);
            this.SetScenarioContextParameter("ActiveItems", itemsNames);

        }


        [When(@"I complete all items")]
        public void WhenICompleteAllItems()
        {
            App.Logger.Info("Make all items complete");
            var itemsNames = App.Main.GetToDoItemsNamesList();
            this.SetScenarioContextParameter("Items", itemsNames);
            App.Main.CompleteAllItems();
        }

        /// <summary>
        /// Rename any items and put old name and new nams to context 'RenamedItems' as tuples
        /// Item1 - old name
        /// Item2 - new name
        /// </summary>
        [When(@"I rename ""(.*)"" items")]
        public void WhenIRenameAnyItem(int numberOfItems)
        {
            var dir = Directory.GetCurrentDirectory();
            App.Logger.Info("Renaming any item");
            var renamedItems = App.Main.RenameAnyItems(numberOfItems);

            this.SetScenarioContextParameter("RenamedItemsNames", renamedItems);
        }

        [When(@"I select ""(.*)"" filter")]
        public void WhenISelectFilter(string filterName)
        {
            App.Logger.Info($"Change filter to {filterName}");
            App.Main.SetFilter(filterName);
        }


        #endregion

        #region Then

        [Then(@"I shouldn't see alert")]
        public void ThenIShouldnTSeeAlert()
        {
            string alertText = null;
            string errorMessage = null;
            try
            {
                alertText = App.Pages.Driver.SwitchTo().Alert().Text;
            }
            catch (NoAlertPresentException e)
            {
                errorMessage = e.Message;
            }

            Assert.That(!string.IsNullOrEmpty(errorMessage), "Alert shouldn't be displayed");
            Assert.That(string.IsNullOrEmpty(alertText));
        }

        [Then(@"I see item (.*) is created")]
        public void ThenISeeItemAlertIsCreated(string itemName)
        {
            App.Logger.Info($"Checking that to do item '{itemName}' exists");
            var items = App.Main.GetActiveToDoItemsNamesList().Where(e => e.Equals(itemName)).ToList();
            Assert.That(items.Count.Equals(1), $"Item with name {itemName} wasn't created");
        }

        /// <summary>
        /// Verify deleted item is not displayed any more.
        /// </summary>
        [Then(@"I do not see deleted item in the list any more")]
        [Then(@"I do not see deleted items in the list any more")]
        public void ThenIDoNotSeeDeletedItemInTheListAnyMore()
        {
            App.Logger.Info("Check that item(s) was/were removed");
            var removedNames = this.GetScenarioContextParameter<List<string>>("RemovedItemsNames");
            var removedNamedFormatted = ServiceMethods.ListToString(removedNames);

            App.Logger.Info($"Removed items to check: {removedNamedFormatted}");
            var items = App.Main.GetToDoItemsNamesList();
            var initialItemsList = this.GetScenarioContextParameter<List<string>>("Items");

            Assert.That(items.SequenceEqual(initialItemsList.Except(removedNames)), "Removed items shouldn't be displayed");
        }


        [Then(@"I see that items counter displays valid number")]
        public void ThenISeeThatItemsCounterDisplaysValidNumber()
        {
            var itemsNames = App.Main.GetActiveToDoItemsNamesList();
            App.Logger.Info($"Check that active items count is {itemsNames.Count}");
            var actualNumberOfItems = App.Main.GetNumberOfItems();
            Assert.That(itemsNames.Count.Equals(actualNumberOfItems), "Displayed items number is incorrect");
        }

        [Then(@"I see item in the completed state")]
        [Then(@"I see items in the completed state")]
        public void ThenISeeItemInTheCompletedState()
        {
            App.Logger.Info("Check that item(s) was/were completed");

            var completedNames = this.GetScenarioContextParameter<List<string>>("CompletedItemsNames");
            var completedNamedFormatted = ServiceMethods.ListToString(completedNames);

            App.Logger.Info($"Completed items to check: {completedNamedFormatted}");
            var items = App.Main.GetActiveToDoItemsNamesList();
            var initialItemsList = this.GetScenarioContextParameter<List<string>>("ActiveItems");

            Assert.That(items.SequenceEqual(initialItemsList.Except(completedNames)), "Items weren't completed");

            items = App.Main.GetToDoItemsNamesList();
            Assert.That(completedNames.All(e => items.Contains(e)), "Completed items are not displayed in the list");
        }

        [Then(@"I see all items in the completed state")]
        public void ThenISeeAllItemsInTheCompletedState()
        {
            App.Logger.Info("Check that all items are completed");

            var initialItemsList = this.GetScenarioContextParameter<List<string>>("Items");
            var activeItems = App.Main.GetActiveToDoItemsNamesList();
            var allItems = App.Main.GetToDoItemsNamesList();

            Assert.That(activeItems.Count.Equals(0), "There are should be no active items left");
            Assert.That(allItems.SequenceEqual(initialItemsList), "All items are still displayed");
        }

        [Then(@"I see that item with new name is displayed")]
        [Then(@"I see that items with new name are displayed")]
        public void ThenISeeThatItemWithNewNameIsDisplayed()
        {
            App.Logger.Info("Check that items(s) renamed");

            var renamedItems = this.GetScenarioContextParameter<List<Tuple<string, string>>>("RenamedItemsNames");
            var items = App.Main.GetToDoItemsNamesList();

            foreach (var item in renamedItems)
            {
                Assert.That(!items.Contains(item.Item1) && items.Contains(item.Item2), "Only new item should be displayed");
            }
        }

        [Then(@"I see items are displayed")]
        public void ThenISeeItemsAreDisplayed(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                var itemName = row["item name"];
                App.Logger.Info($"Checking that to do item '{itemName}' exists");
                var items = App.Main.GetActiveToDoItemsNamesList().Where(e => e.Equals(itemName)).ToList();
                Assert.That(items.Count.Equals(1), $"Item with name {itemName} wasn't created");
            }
        }

        [Then(@"I see created items are displayed")]
        public void ThenISeeCreatedItemsAreDisplayed()
        {
            var itemsNames = GetScenarioContextParameter<List<string>>("Items");
            foreach (var itemName in itemsNames)
            {
                App.Logger.Info($"Checking that to do item '{itemName}' exists");
                var items = App.Main.GetActiveToDoItemsNamesList().Where(e => e.Equals(itemName)).ToList();
                Assert.That(items.Count.Equals(1), $"Item with name {itemName} wasn't created");
            }

        }

        [Then(@"I see only active to do items")]
        public void ThenISeeOnlyActiveToDoItems()
        {
            App.Logger.Info("Check that only active items are displayed");
            var allItems = App.Main.GetToDoItemsNamesList();
            var activeItems = App.Main.GetActiveToDoItemsNamesList();
            Assert.AreEqual(allItems, activeItems, "Only active items are displayed");

            var initialActiveItems = GetScenarioContextParameter<List<string>>("ActiveItems");
            var completedItems = GetScenarioContextParameter<List<string>>("CompletedItemsNames");

            // consistency check
            Assert.That(ServiceMethods.CompareLists(allItems, initialActiveItems.Except(completedItems).ToList()));
        }

        [Then(@"I see only completed to do items")]
        public void ThenISeeOnlyCompletedToDoItems()
        {
            App.Logger.Info("Check that only completed items are displayed");

            var allItems = App.Main.GetToDoItemsNamesList();
            var completedItems = App.Main.GetCompletedToDoItemsNamesList();
            var activeItems = App.Main.GetActiveToDoItemsNamesList();
            Assert.That(activeItems.Count.Equals(0) && ServiceMethods.CompareLists(allItems, completedItems), "Only completed items are displayed");

            var initialCompletedItems = GetScenarioContextParameter<List<string>>("CompletedItemsNames");

            // consistency check
            Assert.That(ServiceMethods.CompareLists(allItems, initialCompletedItems));
        }


        #endregion
    }
}
