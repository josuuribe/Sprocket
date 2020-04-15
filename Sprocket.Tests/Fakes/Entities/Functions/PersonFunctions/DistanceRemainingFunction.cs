using RaraAvis.Sprocket.Parts.Elements.Commands.ExpressionOperators;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    class DistanceRemainingFunction : Function<Person, int, int>
    {
        public override int Value(RuleElement<Person> element)
        {
            return this.Parameters - element.Element.DistanceTravelled;
        }
    }
}
