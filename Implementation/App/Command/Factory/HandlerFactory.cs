using System;
using SurveyMod.Domain;
using SurveyMod.Domain.Entity;
using SurveyMod.Domain.Repository;
using SurveyMod.Implementation.App.Command.Handler;
using SurveyMod.Implementation.App.Factory;
using Vote = SurveyMod.Implementation.App.Command.Handler.Vote;

namespace SurveyMod.Implementation.App.Command.Factory
{
    
    public enum CommandHandler
    {
        Start,
        Stop,
        List,
        Vote
    }
    
    public class HandlerFactory
    {
        private readonly ISurveyRepository _repository;
        private readonly Facade _facade;
        private readonly Player _player;

        public HandlerFactory(ISurveyRepository repository, Facade facade, Player player)
        {
            _repository = repository;
            _facade = facade;
            _player = player;
        }
        
        public Base GetCommandHandler(string name)
        {
            switch (name)
            {
                case "start":
                    return GetStartCommandHandler();
                case "list":
                    return GetListCommandHandler();
                case "vote":
                    return GetVoteCommandHandler();
            }

            throw new Exception($"Handler \"{name}\" not found");
        }
        
        private Start GetStartCommandHandler()
        {
            return new Start(
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