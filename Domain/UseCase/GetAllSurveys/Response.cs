using System.Collections.Generic;

namespace SurveyMod.Domain.UseCase.GetAllSurveys
{
    public class Response
    {
        public List<Entity.Survey> Surveys { get; }

        public Response(List<Entity.Survey> surveys)
        {
            Surveys = surveys;
        }
    }
}
