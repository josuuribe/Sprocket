using RaraAvis.Sprocket.RuleEngine.Elements.Operates;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    public class AddAgeFunction : Function<Person, int, bool>
    {
        public AddAgeFunction()
        { }
        public AddAgeFunction(int parameter) : base(default(Person), parameter)
        { }
        public override bool Value(Person element)
        {
            element.Age += this.Parameters;
            return true;
        }
    }
}
