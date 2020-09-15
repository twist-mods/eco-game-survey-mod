using System;
using SurveyMod.Domain;
using SurveyMod.Domain.Entity;
using SurveyMod.Domain.Repository;
using SurveyMod.Implementation.App.Command.Entity;
using SurveyMod.Implementation.App.Command.Output;
using SurveyMod.Presentation.SaveSurvey;

namespace SurveyMod.Implementation.App.Command.Handler
{
    public class Stop: Base
    {
        private readonly ISurveyRepository _repository;
        private readonly Facade _facade;
        private Player _player;
 
        public Stop(ISurveyRepository repository, Facade facade, Player player)
        {
            _repository = repository;
            _facade = facade;
            _player = player;
        }

        public override void Configure()
        {
            SetDescription("Create a survey")
                .AddOption("i", "The survey ID", OptionAvailability.Optional);
        }

        public override void Handle(Entity.Command command, IOutput output)
        {
            CheckSurveyIdIsGiven(command);

            var survey = _repository.FindOne(GetSurveyId(command));

            CheckValidity(survey);

            survey.Status = SurveyStatus.Done;
            
            var request = _facade.ToSaveSurveyFactory().CreateRequest(survey);
            var presenter = new StopSurveyAsStringPresenter();

            _facade.ToSaveSurveyFactory()
                .CreateExecutor(_repository)
                .Execute(request, presenter);

            output.WriteLineForAll(presenter.ViewModel);
        }

        private void CheckValidity(Survey survey)
        {
            if (survey.Player.Id != _player.Id)
            {
                throw new Exception("You cannot end a survey you don't own!\r\n");
            }

            if (survey.Status == SurveyStatus.Done)
            {
                throw new Exception("This survey has already ended.\r\n");
            }
        }

        private static void CheckSurveyIdIsGiven(Entity.Command command)
        {
            if (OptionParser.CountOptions("i", command) != 1)
            {
                throw new Exception("You must specify the ID");
            }
        }

        private static string GetSurveyId(Entity.Command command)
        {
            return OptionParser.ParseOption("i", command)[0];
        }
    }
}