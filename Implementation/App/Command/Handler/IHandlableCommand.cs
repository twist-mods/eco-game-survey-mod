using SurveyMod.Implementation.App.Command.Output;

namespace SurveyMod.Implementation.App.Command.Handler
{
    public interface IHandlableCommand
    {
        void Handle(Entity.Command command, IOutput output);
    }
}