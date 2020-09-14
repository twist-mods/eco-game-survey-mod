using SurveyMod.Domain.Entity;

namespace SurveyMod.Implementation.App.Factory
{
    public class PlayerFactory
    {
        public Player CreatePlayer(string id)
        {
            return new Player(id);
        }
    }
}