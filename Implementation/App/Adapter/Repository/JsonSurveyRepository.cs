using SurveyMod.Domain.Repository;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SurveyMod.Domain.Entity;
using SurveyMod.Implementation.App.Generator;

namespace SurveyMod.Implementation.App.Adapter.Repository
{
    public class JsonSurveyRepository : ISurveyRepository
    {
        private readonly string _filepath;

        public JsonSurveyRepository(string filepath)
        {
            _filepath = filepath;
        }

        public List<Survey> FindAll()
        {
            if (!File.Exists(_filepath))
            {
                return new List<Survey>();
            }
            
            return JsonConvert.DeserializeObject<List<Survey>>(File.ReadAllText(_filepath));
        }

        public Survey FindOne(string id)
        {
            return FindAll().Find(currentSurvey => id == currentSurvey.Id);
        }

        public Survey Create()
        {
            return new Survey();
        }

        public Survey Save(Survey survey)
        {
            var surveys = FindAll();

            File.WriteAllText(
                _filepath, 
                JsonConvert.SerializeObject(AddOrUpdateSurveyInTheList(survey, surveys))
            );

            return survey;
        }

        private static List<Survey> AddOrUpdateSurveyInTheList(Survey survey, List<Survey> surveys)
        {
            var index = surveys.FindIndex(currentSurvey => survey.Id == currentSurvey.Id);
            
            if (index > -1)
            {
                surveys[index] = survey;
            }
            else
            {
                survey.Id = SurveyIdGenerator.GenerateUniqueId(surveys);
                surveys.Add(survey);
            }

            return surveys;
        }
    }
}
