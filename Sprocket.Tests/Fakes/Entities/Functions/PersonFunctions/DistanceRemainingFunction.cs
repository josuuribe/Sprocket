using RaraAvis.Sprocket.Parts.Elements.Commands.ExpressionOperators;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    class DistanceRemainingFunction : Function<Person, int, int>
    {
        public DistanceRemainingFunction(Person p) : base(p) { }
        public DistanceRemainingFunction(Person p, int i) : base(p, i) { }
        protected internal override int Process(RuleElement<Person> element)
        {
            return this.Parameters - element.Element.DistanceTravelled;
        }
    }
}
