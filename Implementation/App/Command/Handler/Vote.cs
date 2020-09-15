using System;
using SurveyMod.Domain;
using SurveyMod.Domain.Entity;
using SurveyMod.Domain.Repository;
using SurveyMod.Implementation.App.Command.Entity;
using SurveyMod.Implementation.App.Command.Output;
using SurveyMod.Presentation.SaveSurvey;

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
                .AddOption("s", "Show the choices for the selected survey", OptionAvailability.Optional);
        }

        public override void Handle(Entity.Command command, IOutput output)
        {
            CheckSurveyIdIsGiven(command);
            
            var id = OptionParser.ParseOption("i", command)[0];
            var survey = _repository.FindOne(id);

            CheckSurveyIsStillActive(survey);
            
            if (OptionParser.HasOption("s", command))
            {
                output.WriteLineForUser("---------------------------------------------");
                output.WriteLineForUser($"The available choices for the question \"{survey.Question.Value}\" :");
                survey.Choices.ForEach(currentChoice => output.WriteLineForUser($"\t- {currentChoice.Value}"));
                output.WriteLineForUser("");
                return;
            }
            
            CheckChoiceHasBeenGiven(command);

            var choice = survey.Choices
                .Find(currentChoice => currentChoice.Value == OptionParser.ParseOption("c", command)[0]);

            CheckChoiceIsNotNull(choice);
            CheckPlayerHasNotVotedYet(survey);

            survey.Votes.Add(new Domain.Entity.Vote(_player.Id, choice));
            
            var request = _facade.ToSaveSurveyFactory().CreateRequest(survey);

            _facade.ToSaveSurveyFactory()
                .CreateExecutor(_repository)
                .Execute(request, new EmptyStringPresenter());
            
            output.WriteLineForAll($"{_player.Name} has voted for \"{survey.Question.Value}\"");
        }

        private static void CheckChoiceIsNotNull(Text choice)
        {
            if (choice == null)
            {
                throw new Exception("Your choice is not one the available choices. Use -s to show the choices.");
            }
        }

        private static void CheckSurveyIdIsGiven(Entity.Command command)
        {
            if (!OptionParser.HasOption("i", command))
            {
                throw new Exception("The survey ID must be defined using the option -i. You can find the ID by using the command \"/survey list -a\"");
            }
        }

        private static void CheckSurveyIsStillActive(Survey survey)
        {
            if (survey.Status == SurveyStatus.Done)
            {
                throw new Exception("This survey has already ended.");
            }
        }

        private void CheckPlayerHasNotVotedYet(Survey survey)
        {
            var playerVote = survey.Votes.Find(vote => vote.PlayerId == _player.Id);

            if (playerVote != null)
            {
                throw new Exception("You've already voted for this survey");
            }
        }

        private static void CheckChoiceHasBeenGiven(Entity.Command command)
        {
            if (!OptionParser.HasOption("c", command))
            {
                throw new Exception("The choice must be defined using the option -c");
            }
        }
    }
}