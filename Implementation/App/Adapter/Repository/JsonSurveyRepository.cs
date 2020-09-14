using SurveyMod.Domain.Repository;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

 namespace SurveyMod.Implementation.App.Adapter.Repository
{
    public class JsonSurveyRepository : ISurveyRepository
    {
        private readonly string _filepath;

        public JsonSurveyRepository(string filepath)
        {
            _filepath = filepath;
        }

        public List<Domain.Entity.Survey> FindAll()
        {
            if (!File.Exists(_filepath))
            {
                return new List<Domain.Entity.Survey>();
            }
            
            return JsonConvert.DeserializeObject<List<Domain.Entity.Survey>>(File.ReadAllText(_filepath));
        }

        public Domain.Entity.Survey FindOne(string id)
        {
            return FindAll().Find(currentSurvey => id == currentSurvey.Id);
        }

        public Domain.Entity.Survey Create()
        {
            return new Domain.Entity.Survey();
        }

        public void Save(Domain.Entity.Survey survey)
        {
            var surveys = FindAll();
            var index = surveys.FindIndex(currentSurvey => survey.Id == currentSurvey.Id);

            if (index > -1)
            {
                surveys[index] = survey;
            }
            else
            {
                surveys.Add(survey);
            }
            
            File.WriteAllText(_filepath, JsonConvert.SerializeObject(surveys));
        }
    }
}
