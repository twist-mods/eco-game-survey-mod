using System.IO;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using SurveyMod.Implementation.App.Adapter.Repository;
using SurveyMod.Implementation.App.Command;
using SurveyMod.Implementation.App.Command.Entity;
using SurveyMod.Domain;
using SurveyMod.Implementation.App.Command.Factory;
using SurveyMod.Implementation.EcoGame.Adapter.Command.Output;
using Player = SurveyMod.Domain.Entity.Player;

namespace Eco.Mods
{
    public class SurveyCommands : IChatCommandHandler
    {
        private static readonly string StoragePath = Directory.GetCurrentDirectory() + "\\Mods\\SurveyMod\\Storage\\";
        
        [ChatCommand("Survey manager")]
        public static void Survey(User user) { }

        [ChatSubCommand("Survey", "Start a survey", ChatAuthorizationLevel.User)]
        public static void Start(User user, string input)
        {
            CreateStorageDirectoryWhenNotExists();
            GetRunner(user).RunCommand(new Command($"/survey start {input}"));
        }

        [ChatSubCommand("Survey", "Stop a survey", ChatAuthorizationLevel.User)]
        public static void Stop(User user, string input)
        {
            CreateStorageDirectoryWhenNotExists();
            GetRunner(user).RunCommand(new Command($"/survey stop {input}"));
        }

        [ChatSubCommand("Survey", "List the surveys", ChatAuthorizationLevel.User)]
        public static void List(User user, string input)
        {
            CreateStorageDirectoryWhenNotExists();
            GetRunner(user).RunCommand(new Command($"/survey list {input}"));
        }

        [ChatSubCommand("Survey", "Vote for a survey", ChatAuthorizationLevel.User)]
        public static void Vote(User user, string input)
        {
            CreateStorageDirectoryWhenNotExists();
            GetRunner(user).RunCommand(new Command($"/survey vote {input}"));
        }

        private static void CreateStorageDirectoryWhenNotExists()
        {
            if (!Directory.Exists(StoragePath))
            {
                Directory.CreateDirectory(StoragePath);
            }
        }

        private static Runner GetRunner(User user)
        {
            var handlerFactory = new HandlerFactory(new JsonSurveyRepository(StoragePath + "surveys.json"),
                new Facade(), new Player(user.Id.ToString(), user.Name));

            return new Runner(
                handlerFactory, 
                new ChatClient(user));
        }
    }
}