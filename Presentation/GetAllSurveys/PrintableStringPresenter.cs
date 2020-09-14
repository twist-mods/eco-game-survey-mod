using SurveyMod.Domain.UseCase.GetAllSurveys;

namespace SurveyMod.Presentation.GetAllSurveys
{
    public class PrintableStringPresenter: IPresenter
    {
        public string ViewModel { get; set; } = "";
        
        public void Present(Response response)
        {
            response.Surveys.ForEach(survey =>
            {
                ViewModel += $"- ID: {survey.Id} | Question: {survey.Question.Value} (";
                survey.Choices.ForEach(choice => ViewModel += $"{choice.Value}, ");
                ViewModel = ViewModel.Substring(0, ViewModel.Length - 2) + ")\r\n";
            });
        }
    }
}