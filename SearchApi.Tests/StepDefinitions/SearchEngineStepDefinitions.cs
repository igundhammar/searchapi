using System;
using Reqnroll;

namespace SearchApi.Tests.StepDefinitions
{
    [Binding]
    public class SearchEngineStepDefinitions
    {
        [Given("the following search words:")]
        public void GivenTheFollowingSearchWords(DataTable dataTable)
        {
            throw new PendingStepException();
        }

        [Given("the following engines are available:")]
        public void GivenTheFollowingEnginesAreAvailable(DataTable dataTable)
        {
            throw new PendingStepException();
        }

        [When("I perform a search")]
        public void WhenIPerformASearch()
        {
            throw new PendingStepException();
        }

        [Then("I should receive a result from each engine")]
        public void ThenIShouldReceiveAResultFromEachEngine()
        {
            throw new PendingStepException();
        }

        [Then("each result should contain total and per-word counts")]
        public void ThenEachResultShouldContainTotalAndPer_WordCounts()
        {
            throw new PendingStepException();
        }
    }
}
