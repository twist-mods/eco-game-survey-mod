using System;
using SurveyMod.Domain.Entity;

namespace SurveyMod.Presentation.Shared
{
    public static class ChoicesWithResultsAsStringView
    {
        public static string CreateView(Survey survey, bool areResultsShown)
        {
            var viewModel = "";
            
            survey.Choices.ForEach(choice =>
            {
                var choiceToOutput = $"{choice.Value}, ";

                if (areResultsShown)
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