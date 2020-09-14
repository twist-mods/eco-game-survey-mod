namespace SurveyMod.Domain.UseCase.GetAllSurveys
{
    public class Request
    {
        public string Id { get; }

        public Request(string id)
        {
            this.Id = id;
        }
    }
}
