using System.Collections.Generic;
using SurveyMod.Domain.Entity;

namespace SurveyMod.Domain.Repository
{
    public interface ISurveyRepository
    {
        Entity.Survey Create();
        Survey Save(Entity.Survey survey);
        Entity.Survey FindOne(string id);
        List<Entity.Survey> FindAll();
    }
}
