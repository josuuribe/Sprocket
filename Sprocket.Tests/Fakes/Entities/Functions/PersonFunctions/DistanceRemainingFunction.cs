using RaraAvis.Sprocket.RuleEngine.Elements.Operates;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    public class DistanceRemainingFunction : Function<Person, int, int>
    {
        public DistanceRemainingFunction() : base() { }
        public DistanceRemainingFunction(Person p) : base(p) { }
        public DistanceRemainingFunction(Person p, int i) : base(p, i) { }
        public DistanceRemainingFunction(int i) : base(i) { }
        public override int Value(Person element)
        {
            return this.Parameters - element.DistanceTravelled;
        }
    }
}
