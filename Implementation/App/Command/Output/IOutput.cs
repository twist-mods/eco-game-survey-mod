namespace SurveyMod.Implementation.App.Command.Output
{
    public interface IOutput
    {
        public void WriteLineForAll(string message);
        public void WriteLineForUser(string message);
    }
}