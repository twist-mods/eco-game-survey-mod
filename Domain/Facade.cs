using SaveSurveyFactory = SurveyMod.Domain.UseCase.SaveSurvey.Factory;
using GetAllSurveysFactory = SurveyMod.Domain.UseCase.GetAllSurveys.Factory;

namespace SurveyMod.Domain
{
    public class Facade
    {
        public SaveSurveyFactory ToSaveSurveyFactory()
        {
            return new SaveSurveyFactory();
        }
        public GetAllSurveysFactory ToGetAllSurveys()
        {
            return new GetAllSurveysFactory();
        }
    }
}
