using SurveyMod.Domain.UseCase.SaveSurvey;

namespace SurveyMod.Presentation.SaveSurvey
{
    public class StartSurveyAsStringPresenter : IPresenter
    {
        public string ViewModel { get; set; }
        
        public void Present(Response response)
        {
            var survey = response.Survey;
            ViewModel = $"A new survey has been created : \"{survey.Question.Value}\"\r\n" +
                        $"You can vote now (/survey vote -i {survey.Id} -s).\r\n";
        }
    }
}