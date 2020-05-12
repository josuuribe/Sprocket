using RaraAvis.Sprocket.RuleEngine.Elements.Operates;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    public class SonsFunction : Function<Person, int, Person>
    {
        public SonsFunction() : base() { }
        public SonsFunction(Person p, int i) : base(p, i) { }
        public SonsFunction(int i) : base(i) { }
        public override Person Value(Person element)
        {
            if (element.Family.Count > 0)
                return element.Family[this.Parameters];
            else
                return null;
        }
    }
}
