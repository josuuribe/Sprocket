using RaraAvis.Sprocket.Parts.Elements.Commands.ExpressionOperators;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    public class DistanceRemainingFunction : Function<Person, int, int>
    {
        public override int Execute(RuleElement<Person> element)
        {
            return this.Parameters - element.Element.DistanceTravelled;
        }
    }
}
