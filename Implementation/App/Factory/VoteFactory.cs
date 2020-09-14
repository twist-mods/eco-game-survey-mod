using SurveyMod.Domain.Entity;

namespace SurveyMod.Implementation.App.Factory
{
    public class VoteFactory
    {
        public Vote CreateVote(string playerId, Text choice)
        {
            return new Vote(playerId, choice);
        }
    }
}