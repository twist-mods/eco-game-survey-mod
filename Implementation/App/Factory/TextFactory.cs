using SurveyMod.Domain.Entity;

namespace SurveyMod.Implementation.App.Factory
{
    public class TextFactory
    {
        public Text CreateText(string value)
        {
            return new Text(value);
        }
    }
}