using System;
using System.Collections.Generic;
using System.Linq;
using SurveyMod.Domain;
using SurveyMod.Domain.Entity;
using SurveyMod.Domain.Repository;
using SurveyMod.Implementation.App.Command.Entity;
using SurveyMod.Implementation.App.Command.Output;
using SurveyMod.Implementation.App.Factory;

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
            var parameters = command.SplitInputBySpace();
            
            CheckCommandParametersValidity(parameters);

            var survey = CreateSurveyOnInputBasis(command);
            var request = _facade.ToSaveSurveyFactory().CreateRequest(survey);

            _facade.ToSaveSurveyFactory()
                .CreateExecutor(_repository)
                .Execute(request);

            output.WriteLineForAll("A new survey has been created.");
            output.WriteLineForAll($"{survey.Question.Value}. Please vote now (/survey vote -i {survey.Id} -s)!");
        }

        private Survey CreateSurveyOnInputBasis(Entity.Command command)
        {
            var survey = _repository.Create();
            
            survey.Id = ShortGuid.NewShortGuid().ToString().Substring(0, 5).ToLower();
            survey.Player = _player;
            survey.Question = _textFactory.CreateText(GetQuestionOption(command));
            survey.Choices = GetChoices(command).Select(choice => _textFactory.CreateText(choice)).ToList();
            survey.Votes = new List<Domain.Entity.Vote>();
            
            return survey;
        }

        private static IEnumerable<string> GetChoices(Entity.Command command)
        {
            var choiceOptions = OptionParser.ParseOption("c", command);

            if (choiceOptions.Count < 2)
            {
                throw new Exception("A minimum of 2 choices are required");
            }

            return choiceOptions;
        }

        private static string GetQuestionOption(Entity.Command command)
        {
            var questionOption = OptionParser.ParseOption("q", command);

            if (questionOption.Count != 1)
            {
                throw new Exception("A question is mandatory to create a SurveyMod.");
            }

            return questionOption[0];
        }

        private static void CheckCommandParametersValidity(string[] commands)
        {
            if (commands.Length < 3)
            {
                throw new Exception("-q parameter is mandatory");
            }

            if (commands.Length < 4)
            {
                throw new Exception("You must specify at least 2 choices using -c parameter");
            }
        }
    }
}