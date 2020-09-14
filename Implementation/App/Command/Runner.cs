using System;
using SurveyMod.Domain;
using SurveyMod.Domain.Entity;
using SurveyMod.Domain.Repository;
using SurveyMod.Implementation.App.Command.Handler;
using SurveyMod.Implementation.App.Command.Output;
using SurveyMod.Implementation.App.Factory;
using Vote = SurveyMod.Implementation.App.Command.Handler.Vote;

namespace SurveyMod.Implementation.App.Command
{
    public class Runner
    {
        private readonly ISurveyRepository _repository;
        private readonly Facade _facade;
        private readonly Player _player;
        private readonly IOutput _output;

        public Runner(ISurveyRepository repository, Facade facade, Player player, IOutput output)
        {
            _repository = repository;
            _facade = facade;
            _player = player;
            _output = output;
        }

        public void RunCommand(Entity.Command command)
        {
            try
            {
                RunHandlerByCommand(command);
            }
            catch (Exception exception)
            {
                _output.WriteLineForUser(exception.Message);
            }
        }

        private void RunHandlerByCommand(Entity.Command command)
        {
            var commandHandler = GetCommandHandler(command);

            commandHandler.Configure();

            if (IsHelpRequestedByUser(command))
            {
                PrintHelp(commandHandler);
                return;
            }

            commandHandler.Handle(command, _output);
        }

        private Base GetCommandHandler(Entity.Command command)
        {
            if (command.GetSubCommandName() == "create")
            {
                return GetCreateCommandHandler();
            } 
            
            if (command.GetSubCommandName() == "list")
            {
                return GetListCommandHandler();
            } 
            
            if (command.GetSubCommandName() == "vote")
            {
                return GetVoteCommandHandler();
            }

            throw new Exception("Handler not found for command");
        }
        
        private static bool IsHelpRequestedByUser(Entity.Command command)
        {
            var parameters = command.SplitInputBySpace();
            
            return parameters.Length == 3 && parameters[2] == "help";
        }

        private void PrintHelp(Base commandHandler)
        {
            _output.WriteLineForUser("---------------------------------------------");
            _output.WriteLineForUser(commandHandler.Description);
            commandHandler.Options.ForEach(option => _output.WriteLineForUser($"\t-{option.Name} : {option.Description}"));
            _output.WriteLineForUser("");
        }
        
        private Create GetCreateCommandHandler()
        {
            return new Create(
                _repository, 
                _facade,
                _player,
                new TextFactory()
            );
        }

        private List GetListCommandHandler()
        {
            return new List(
                _repository, 
                _facade
            );
        }

        private Vote GetVoteCommandHandler()
        {
            return new Vote(
                _repository, 
                _facade,
                _player
            );
        }
    }
}