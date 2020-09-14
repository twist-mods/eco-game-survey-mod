using System.Collections.Generic;

namespace SurveyMod.Domain.Repository
{
    public interface ISurveyRepository
    {
        Entity.Survey Create();
        void Save(Entity.Survey survey);
        Entity.Survey FindOne(string id);
        List<Entity.Survey> FindAll();
    }
}
