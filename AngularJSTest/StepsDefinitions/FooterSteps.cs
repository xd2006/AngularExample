

namespace AngularJSTest.StepsDefinitions
{
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    /// <summary>
    /// The footer steps.
    /// </summary>
    [Scope(Feature = "ComponentTestsFooter")]
    [Binding]
    public class FooterSteps : StepsTemplate
    {
        [When(@"I click on the footer (.*)")]
        public void WhenIClickOnTheFooterLink(string linkText)
        {
            App.Logger.Info($"Clicking on link with text '{linkText}'");
            App.Main.ClickFooterLink(linkText);
        }

        [Then(@"I see the (.*) is opened")]
        public void ThenISeeThePageIsOpened(string pageUrl)
        {
            App.Logger.Info($"Checking that page '{pageUrl}' is opened");
            var url = App.Main.GetPageUrl();
            Assert.That(url.Contains(pageUrl));

        }


    }
}
