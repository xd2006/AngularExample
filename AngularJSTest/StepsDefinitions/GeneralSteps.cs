

namespace AngularJSTest.StepsDefinitions
{
    using System;
    using System.Linq;

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
        /// Remove any item and write it's name to context variable 'ItemName'.
        /// </summary>
        [When(@"I remove any item")]
        public void WhenIRemoveAnyItem()
        {
            App.Logger.Info("Removing any item");
            var name = App.Main.RemoveAnyItem();
            this.SetScenarioContextParameter("ItemName", name);
        }

        [Then(@"I do not see deleted item in the list any more")]
        public void ThenIDoNotSeeDeletedItemInTheListAnyMore()
        {
            App.Logger.Info("Check that item was removed");
            string name = this.GetScenarioContextParameter<string>("ItemName");
            var items = App.Pages.HomePage.ToDosWidget.GetToDos().Select(e => e.Name);
            Assert.That(!items.Contains(name));
        }
    }
}
