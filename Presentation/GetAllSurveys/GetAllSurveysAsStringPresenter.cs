using System;
using SurveyMod.Domain.Entity;
using SurveyMod.Domain.UseCase.GetAllSurveys;
using SurveyMod.Presentation.Shared;

namespace SurveyMod.Presentation.GetAllSurveys
{
    public class GetAllSurveysAsStringPresenter: IPresenter
    {
        public string ViewModel { get; set; }

        private readonly bool _areActiveShown;
        private readonly bool _areEndedShown;
        private readonly bool _areResultsShown;

        public GetAllSurveysAsStringPresenter(string viewModel = "", bool areActiveShown = false, bool areEndedShown = false, bool areResultsShown = false)
        {
            ViewModel = viewModel;
            _areActiveShown = areActiveShown;
            _areEndedShown = areEndedShown;
            _areResultsShown = areResultsShown;
        }

        public void Present(Response response)
        {
            response.Surveys.ForEach(survey =>
            {
                if (
                    (_areActiveShown && survey.Status == SurveyStatus.Done) 
                    || (_areEndedShown && survey.Status == SurveyStatus.OnGoing)
                ) {
                    return;
                }

                ViewModel += $"- ID: {survey.Id} | Question: {survey.Question.Value} " + 
                             $"({ChoicesWithResultsAsStringView.CreateView(survey, _areResultsShown)})\r\n";
            });
        }

        private string CreateViewForChoices(Survey survey)
        {
            var viewModel = "";
            
            survey.Choices.ForEach(choice =>
            {
                var choiceToOutput = $"{choice.Value}, ";

                if (_areResultsShown)
                {
                    choiceToOutput = $"{choice.Value} {CalculatePercentageOfVotesForChoice(survey, choice)}%, ";
                }

                viewModel += choiceToOutput;
            });

            return viewModel.Substring(0, viewModel.Length - 2);
        }

        private static decimal CalculatePercentageOfVotesForChoice(Survey survey, Text choice)
        {
            var counter = 0;

            survey.Votes.ForEach(vote =>
            {
                if (vote.Choice.Value == choice.Value)
                {
                    counter++;
                }
            });

            return counter > 0 ? Math.Round((decimal) ((counter / survey.Votes.Count) * 100), 2) : counter;
        }
    }
}