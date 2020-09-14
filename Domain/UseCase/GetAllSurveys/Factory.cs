using System.Collections.Generic;
using SurveyMod.Domain.Repository;

namespace SurveyMod.Domain.UseCase.GetAllSurveys
{
    public class Factory
    {
        public Executor CreateExecutor(ISurveyRepository surveyRepository)
        {
            return new Executor(surveyRepository);
        }

        public Response CreateResponse(List<Entity.Survey> surveys)
        {
            return new Response(surveys);
        }
    }
}
