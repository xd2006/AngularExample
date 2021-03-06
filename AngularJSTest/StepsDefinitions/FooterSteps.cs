﻿
namespace AngularJSTest.StepsDefinitions
{
    using AngularJSTest.Service;

    using NUnit.Framework;
    using TechTalk.SpecFlow;

    /// <summary>
    /// The footer steps.
    /// </summary>
    [Scope(Feature = "ComponentTestsFooter")]
    [Binding]
    public class FooterSteps : StepsTemplate
    {
        #region When

        [When(@"I click on the footer (.*)")]
        public void WhenIClickOnTheFooterLink(string linkText)
        {
            App.Logger.Info($"Clicking on link with text '{linkText}'");
            App.Main.ClickFooterLink(linkText);
        }

        #endregion

        #region Then

        [Then(@"I see the (.*) is opened")]
        public void ThenISeeThePageIsOpened(string pageUrl)
        {
            App.Logger.Info($"Checking that page '{pageUrl}' is opened");
            App.Pages.Driver.WaitForPageReady();
            var url = App.Main.GetPageUrl();
            Assert.That(url.Contains(pageUrl));
        }

        #endregion
    }
}
