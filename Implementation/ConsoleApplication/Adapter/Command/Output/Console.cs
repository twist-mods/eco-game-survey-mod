using SurveyMod.Implementation.App.Command.Output;
using SystemConsole=System.Console;

namespace SurveyMod.Implementation.ConsoleApplication.Adapter.Command.Output
{
    public class Console: IOutput
    {
        public void WriteLineForAll(string message)
        {
            SystemConsole.WriteLine(message);
        }

        public void WriteLineForUser(string message)
        {
            SystemConsole.WriteLine(message);
        }
    }
}