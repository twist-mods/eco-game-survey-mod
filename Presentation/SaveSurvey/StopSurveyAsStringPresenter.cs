using SurveyMod.Domain.UseCase.SaveSurvey;
using SurveyMod.Presentation.Shared;

namespace SurveyMod.Presentation.SaveSurvey
{
    public class StopSurveyAsStringPresenter : IPresenter
    {
        public string ViewModel { get; set; }
        
        public void Present(Response response)
        {
            ViewModel = "A survey has been ended.\r\n" +
                        $"{response.Survey.Question.Value} | " +
                        $"Results: {ChoicesWithResultsAsStringView.CreateView(response.Survey, true)}\r\n";
        }
    }
}