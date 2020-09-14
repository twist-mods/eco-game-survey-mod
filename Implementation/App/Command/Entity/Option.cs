namespace SurveyMod.Implementation.App.Command.Entity
{
    public enum OptionAvailability
    {
        Required,
        Optional
    }
    
    public class Option
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public OptionAvailability Availability { get; set; }
    }
}