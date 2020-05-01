using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    public class GetDistanceCommand : ExpressionCommand<Person, int>
    {
        protected internal override int Process(RuleElement<Person> element)
        {
            return element.Element.DistanceTravelled;
        }
    }
}
