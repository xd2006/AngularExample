

namespace AngularJSTest.StepsDefinitions
{
    using System;

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
    }
}
