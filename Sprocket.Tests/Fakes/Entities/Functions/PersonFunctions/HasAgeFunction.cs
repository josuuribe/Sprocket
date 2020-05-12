using RaraAvis.Sprocket.RuleEngine.Elements.Operates;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    public class HasAgeFunction : Function<Person, int, bool>
    {
        public HasAgeFunction() : base()
        {

        }
        public HasAgeFunction(int parameter) : base(default(Person), parameter)
        { }

        public override bool Value(Person element)
        {
            return element.Age == this.Parameters;
        }
    }
}
