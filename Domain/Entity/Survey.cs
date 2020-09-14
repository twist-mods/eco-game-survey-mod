using System.Collections.Generic;

namespace SurveyMod.Domain.Entity
{
    public enum SurveyStatus
    {
        OnGoing,
        Done
    }
    
    public class Survey
    {
        public string Id { get; set; }
        public Player Player { get; set; }
        public Text Question { get; set; }
        public List<Text> Choices { get; set;  }
        public List<Vote> Votes { get; set; }
        public SurveyStatus Status { get; set; } = SurveyStatus.OnGoing;
    }
}