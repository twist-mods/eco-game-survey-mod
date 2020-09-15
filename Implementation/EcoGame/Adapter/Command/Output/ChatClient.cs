using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using Eco.Shared.Localization;
using Eco.Shared.Services;
using SurveyMod.Implementation.App.Command.Output;

namespace SurveyMod.Implementation.EcoGame.Adapter.Command.Output
{
    public class ChatClient: IOutput
    {
        private readonly User _user;

        public ChatClient(User user)
        {
            _user = user;
        }

        public void WriteLineForAll(string message)
        {
            ChatManager.ServerMessageToAll(Localizer.DoStr($"\r\n{message}"), DefaultChatTags.General);
        }

        public void WriteLineForUser(string message)
        {
            ChatManager.ServerMessageToPlayerLocStr(message, _user);
        }
    }
}