using RaraAvis.Sprocket.RuleEngine.Elements.Operates;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    public class SetNameFunction : Function<Person, string, bool>
    {
        public override bool Value(Person element)
        {
            element.Name = this.Parameters;
            return true;
        }
    }
}
