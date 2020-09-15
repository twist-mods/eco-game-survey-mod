using SurveyMod.Domain;
using SurveyMod.Domain.Repository;
using SurveyMod.Implementation.App.Command.Entity;
using SurveyMod.Implementation.App.Command.Output;
using SurveyMod.Presentation.GetAllSurveys;

namespace SurveyMod.Implementation.App.Command.Handler
{
    public class List: Base
    {
        private readonly ISurveyRepository _repository;
        private readonly Facade _facade;

        public List(ISurveyRepository repository, Facade facade)
        {
            _repository = repository;
            _facade = facade;
        }

        public override void Configure()
        {
            SetDescription("List all the surveys")
                .AddOption("a", "List the active surveys", OptionAvailability.Optional)
                .AddOption("i", "List the ended surveys", OptionAvailability.Optional)
                .AddOption("r", "Display the results", OptionAvailability.Optional);
        }

        public override void Handle(Entity.Command command, IOutput output)
        {
            var presenter = new GetAllSurveysAsStringPresenter(
                "",
                OptionParser.HasOption("a", command),
                OptionParser.HasOption("i", command),
                OptionParser.HasOption("r", command));
            
            _facade.ToGetAllSurveys().CreateExecutor(_repository).Execute(presenter);

            output.WriteLineForAll(presenter.ViewModel);
        }
    }
}