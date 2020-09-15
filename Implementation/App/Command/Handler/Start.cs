using System;
using System.Collections.Generic;
using System.Linq;
using SurveyMod.Domain;
using SurveyMod.Domain.Entity;
using SurveyMod.Domain.Repository;
using SurveyMod.Implementation.App.Command.Entity;
using SurveyMod.Implementation.App.Command.Output;
using SurveyMod.Implementation.App.Factory;
using SurveyMod.Presentation.SaveSurvey;

namespace SurveyMod.Implementation.App.Command.Handler
{
    public class Start: Base
    {
        private readonly ISurveyRepository _repository;
        private readonly Facade _facade;
        private Player _player;
        private TextFactory _textFactory;

        public Start(ISurveyRepository repository, Facade facade, Player player, TextFactory textFactory)
        {
            _repository = repository;
            _facade = facade;
            _player = player;
            _textFactory = textFactory;
        }

        public override void Configure()
        {
            SetDescription("Create a survey")
                .AddOption("q", "The question to survey", OptionAvailability.Required)
                .AddOption("c", "A choice (minimum 2 choices required)", OptionAvailability.Required);
        }

        public override void Handle(Entity.Command command, IOutput output)
        {
            CheckCommandParametersValidity(command);
            
            var survey = CreateSurveyOnInputBasis(command);
            var request = _facade.ToSaveSurveyFactory().CreateRequest(survey);
            var presenter = new StartSurveyAsStringPresenter();
                
            _facade.ToSaveSurveyFactory()
                .CreateExecutor(_repository)
                .Execute(request, presenter);

            output.WriteLineForAll(presenter.ViewModel);
        }

        private static void CheckCommandParametersValidity(Entity.Command command)
        {
            if (OptionParser.CountOptions("q", command) != 1)
            {
                throw new Exception("You must specify a question");
            }

            if (OptionParser.CountOptions("c", command) < 2)
            {
                throw new Exception("You must specify at least 2 choices");
            }
        }

        private Survey CreateSurveyOnInputBasis(Entity.Command command)
        {
            var survey = _repository.Create();
            
            survey.Player = _player;
            survey.Question = _textFactory.CreateText(GetQuestionOption(command));
            survey.Choices = GetChoices(command).Select(choice => _textFactory.CreateText(choice)).ToList();
            survey.Votes = new List<Domain.Entity.Vote>();
            
            return survey;
        }

        private static string GetQuestionOption(Entity.Command command)
        {
            return OptionParser.ParseOption("q", command)[0];
        }

        private static IEnumerable<string> GetChoices(Entity.Command command)
        {
            return OptionParser.ParseOption("c", command);
        }
    }
}