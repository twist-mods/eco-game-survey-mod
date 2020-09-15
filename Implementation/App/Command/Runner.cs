using System;
using SurveyMod.Domain;
using SurveyMod.Domain.Entity;
using SurveyMod.Domain.Repository;
using SurveyMod.Implementation.App.Command.Factory;
using SurveyMod.Implementation.App.Command.Handler;
using SurveyMod.Implementation.App.Command.Output;
using SurveyMod.Implementation.App.Factory;
using Vote = SurveyMod.Implementation.App.Command.Handler.Vote;

namespace SurveyMod.Implementation.App.Command
{
    public class Runner
    {
        private readonly HandlerFactory _handlerFactory;
        private readonly IOutput _output;

        public Runner(HandlerFactory handlerFactory, IOutput output)
        {
            _handlerFactory = handlerFactory;
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
                _output.WriteLineForUser($"Error: {exception.Message}\r\n");
            }
        }

        private void RunHandlerByCommand(Entity.Command command)
        {
            var commandHandler = _handlerFactory.GetCommandHandler(command.GetSubCommandName());

            commandHandler.Configure();

            if (IsHelpRequestedByUser(command))
            {
                PrintHelp(commandHandler);
                return;
            }

            commandHandler.Handle(command, _output);
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
    }
}