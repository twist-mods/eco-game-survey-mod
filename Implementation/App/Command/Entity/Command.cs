namespace SurveyMod.Implementation.App.Command.Entity
{
    public class Command
    {
        public string Input { get; }
        
        public Command(string input)
        {
            Input = input;
        }

        public string GetCommandName()
        {
            return SplitInputBySpace()[0];
        }

        public string GetSubCommandName()
        {
            return SplitInputBySpace()[1];
        }
        
        public string[] SplitInputBySpace()
        {
            return Input.Substring(1).Split(' ');
        } 
    }
}