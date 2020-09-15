using SurveyMod.Domain.UseCase.SaveSurvey;

namespace SurveyMod.Presentation.SaveSurvey
{
    public class EmptyStringPresenter : IPresenter
    {
        public string ViewModel { get; set; }
        
        public void Present(Response response)
        {
            ViewModel = "";
        }
    }
}