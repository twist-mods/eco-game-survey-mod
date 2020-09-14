using System;
using SurveyMod.Domain;
using SurveyMod.Domain.Entity;
using SurveyMod.Domain.Repository;
using SurveyMod.Implementation.App.Command.Entity;
using SurveyMod.Implementation.App.Command.Output;

namespace SurveyMod.Implementation.App.Command.Handler
{
    public class Vote: Base
    {
        private readonly ISurveyRepository _repository;
        private readonly Facade _facade;
        private readonly Player _player;

        public Vote(ISurveyRepository repository, Facade facade, Player player)
        {
            _repository = repository;
            _facade = facade;
            _player = player;
        }

        public override void Configure()
        {
            SetDescription("Vote for a survey")
                .AddOption("i", "the ID of the survey", OptionAvailability.Optional)
                .AddOption("c", "Your choice", OptionAvailability.Optional)
                .AddOption("s", "Show the choices for the selected survey (using with \"true\")", OptionAvailability.Optional);
        }

        public override void Handle(Entity.Command command, IOutput output)
        {
            if (!OptionParser.HasOption("i", command))
            {
                throw new Exception("The ID must be defined using the option -i");
            }
            
            var id = OptionParser.ParseOption("i", command)[0];
            var survey = _repository.FindOne(id);
            
            if (OptionParser.HasOption("s", command))
            {
                output.WriteLineForAll("---------------------------------------------");
                output.WriteLineForAll($"The available choices for the question \"{survey.Question.Value}\" :");
                survey.Choices.ForEach(currentChoice => output.WriteLineForAll($"\t- {currentChoice.Value}"));
                output.WriteLineForAll("");
                return;
            }
            
            if (!OptionParser.HasOption("c", command))
            {
                throw new Exception("The choice must be defined using the option -c");
            }

            var choice = survey.Choices
                .Find(currentChoice => currentChoice.Value == OptionParser.ParseOption("c", command)[0]);

            if (choice == null)
            {
                return;
            }
            
            survey.Votes.Add(new Domain.Entity.Vote(_player.Id, choice));
            
            var request = _facade.ToSaveSurveyFactory().CreateRequest(survey);

            _facade.ToSaveSurveyFactory()
                .CreateExecutor(_repository)
                .Execute(request);
        }
    }
}