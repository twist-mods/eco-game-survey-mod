namespace SurveyMod.Domain.UseCase.SaveSurvey
{
    public class Response
    {
        public Entity.Survey Survey { get; }

        public Response(Entity.Survey survey)
        {
            Survey = survey;
        }
    }
}
