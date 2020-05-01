using RaraAvis.Sprocket.Parts.Elements.Commands.ExpressionOperators;
using RaraAvis.Sprocket.Parts.Elements.Functions;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    public class SetNameFunction : Function<Person, string, bool>
    {
        protected internal override bool Process(RuleElement<Person> element)
        {
            element.Element.Name = this.Parameters;
            return true;
        }
    }
}
