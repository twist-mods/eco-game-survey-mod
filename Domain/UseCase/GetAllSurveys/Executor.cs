using SurveyMod.Domain.Repository;

namespace SurveyMod.Domain.UseCase.GetAllSurveys
{
    public class Executor
    {
        private readonly ISurveyRepository _surveyRepository;

        public Executor(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public void Execute(IPresenter presenter)
        {
            var response = new Response(_surveyRepository.FindAll());

            presenter.Present(response);
        }
    }
}
