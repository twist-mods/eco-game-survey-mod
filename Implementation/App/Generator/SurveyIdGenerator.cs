using System;
using System.Collections.Generic;
using SurveyMod.Domain.Entity;

namespace SurveyMod.Implementation.App.Generator
{
    public class SurveyIdGenerator
    {
        public static string GenerateUniqueId(List<Survey> currentSurveys)
        {
            var id = "";
                
            do
            {
                id = ShortGuid.NewShortGuid().ToString().Substring(0, 5).ToLower();
            } while (currentSurveys.FindIndex(survey => survey.Id == id) > -1);

            return id;
        }
    }
}