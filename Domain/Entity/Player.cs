namespace SurveyMod.Domain.Entity
{
    public class Player
    {
        public string Id { get; set; }

        public Player(string id)
        {
            Id = id;
        }
    }
}