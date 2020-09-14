using SurveyMod.Domain.Repository;

namespace SurveyMod.Domain.UseCase.SaveSurvey
{
    public class Factory
    {
        public Executor CreateExecutor(ISurveyRepository surveyRepository)
        {
            return new Executor(surveyRepository);
        }

        public Request CreateRequest(Entity.Survey survey)
        {
            return new Request(survey);
        }
    }
}
