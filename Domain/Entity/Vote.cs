namespace SurveyMod.Domain.Entity
{
    public class Vote
    {
        public string PlayerId { get; }
        public Text Choice { get; set; }

        public Vote(string playerId, Text choice)
        {
            PlayerId = playerId;
            Choice = choice;
        }
    }
}