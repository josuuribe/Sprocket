using RaraAvis.Sprocket.RuleEngine.Elements.Operates;
using RaraAvis.Sprocket.RuleEngine.Elements.Operates.Commands;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    public class GetDistanceCommand : Command<Person, int>
    {
        public override int Value(Person element)
        {
            return element.DistanceTravelled;
        }
    }
}
