

namespace AngularJSTest.StepsDefinitions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AngularJSTest.Service;

    using NUnit.Framework;

    using OpenQA.Selenium;

    using TechTalk.SpecFlow;

    [Binding]
    public class GeneralSteps : StepsTemplate
    {
        [When(@"I add new to do item (.*)")]
        public void WhenIAddNewToDoItemAlert(string itemName)
        {
            App.Logger.Info($"Adding new to do item {itemName}");
            App.Main.AddNewToDoItem(itemName); 
        }

        [Then(@"I shouldn't see alert")]
        public void ThenIShouldnTSeeAlert()
        {
            string alertText = null;
            try
            {
                alertText = App.Pages.Driver.SwitchTo().Alert().Text;
            }
            catch (NoAlertPresentException e)
            {
                Assert.That(!string.IsNullOrEmpty(e.Message));
            }

            Assert.That(string.IsNullOrEmpty(alertText));            
        }

        [Then(@"I see item (.*) is created")]
        public void ThenISeeItemAlertIsCreated(string itemName)
        {
            App.Logger.Info($"Checking that to do item '{itemName}' exists");
            var item = App.Main.GetToDoItem(itemName);
            Assert.That(item.Name.Equals(itemName));
        }

        [Given(@"""(.*)"" to do items are created")]
        public void GivenSeveralToDoAreCreated(int numberOfItems)
        {
            App.Logger.Info($"Create {numberOfItems} to do items is needed");
            App.Main.CreateItemsIfNeeded(numberOfItems);
        }

        /// <summary>
        /// Remove any items and write their name to context variable 'RemovedItemsNames'.
        /// Writes initial items list to context - 'Items'
        /// </summary>
        [When(@"I remove any item")]
        public void WhenIRemoveAnyItem()
        {
            App.Logger.Info("Removing any item(s)");
            var itemsNames = App.Main.GetToDoItemsNamesList();
            var removedItems = App.Main.RemoveAnyItems();
            this.SetScenarioContextParameter("RemovedItemsNames", removedItems);
            this.SetScenarioContextParameter("Items", itemsNames);
        }

        /// <summary>
        /// Verify deleted item is not displayed any more.
        /// </summary>
        [Then(@"I do not see deleted item in the list any more")]
        public void ThenIDoNotSeeDeletedItemInTheListAnyMore()
        {
            App.Logger.Info("Check that item(s) was/were removed");
            var removedNames = this.GetScenarioContextParameter<List<string>>("RemovedItemsNames");
            var removedNamedFormatted = ServiceMethods.ListToString(removedNames);

            App.Logger.Info($"Removed items to check: {removedNamedFormatted}");
            var items = App.Main.GetToDoItemsNamesList();
            var initialItemsList = this.GetScenarioContextParameter<List<string>>("Items");

            Assert.That(items.SequenceEqual(initialItemsList.Except(removedNames)));
        }
    }
}
