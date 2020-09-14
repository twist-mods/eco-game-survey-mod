using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SurveyMod.Implementation.App.Command
{
    public class OptionParser
    {
        public static List<string> ParseOption(string optionName, Entity.Command command)
        {
            var regex = new Regex(@"(-"+ optionName +@" [a-zA-Z0-9?_\: ]+)");
            var matches = regex.Matches(command.Input);

            return (from Match match in matches select match.Value.Substring(optionName.Length + 1).Trim()).ToList();
        }

        public static bool HasOption(string optionName, Entity.Command command)
        {
            var regex = new Regex(@"(-"+ optionName +@" [a-zA-Z0-9?_\: ]+)");
            var matches = regex.Matches(command.Input);

            return matches.Count > 0;
        }
    }
}