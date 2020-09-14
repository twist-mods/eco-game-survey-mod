using System.Collections.Generic;
using SurveyMod.Implementation.App.Command.Entity;
using SurveyMod.Implementation.App.Command.Output;

namespace SurveyMod.Implementation.App.Command.Handler
{
    public abstract class Base : IHandlableCommand
    {
        public string Description { get; set; }
        public List<Option> Options { get; } = new List<Option>();
        
        public abstract void Configure();

        public Base SetDescription(string description)
        {
            Description = description;

            return this;
        }

        public Base AddOption(string name, string description, OptionAvailability availability)
        {
            var option = new Option();

            option.Name = name;
            option.Description = description;
            option.Availability = availability;

            Options.Add(option);

            return this;
        }

        public abstract void Handle(Entity.Command command, IOutput output);
    }
}