namespace SurveyMod.Domain.UseCase.SaveSurvey
{
    public class Request
    {
        public Entity.Survey Survey { get; }

        public Request(Entity.Survey survey)
        {
            Survey = survey;
        }
    }
}
