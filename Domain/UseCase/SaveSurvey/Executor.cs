using SurveyMod.Domain.Repository;

namespace SurveyMod.Domain.UseCase.SaveSurvey
{
    public class Executor
    {
        private readonly ISurveyRepository _surveyRepository;

        public Executor(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public void Execute(Request request)
        {
            _surveyRepository.Save(request.Survey);
        }
    }
}
