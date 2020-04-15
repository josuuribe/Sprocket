using RaraAvis.Sprocket.Parts.Elements.Commands.ExpressionOperators;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    class SonsFunction : Function<Person, int, Person>
    {
        public override Person Value(RuleElement<Person> element)
        {
            if (element.Element.Family.Count > 0)
                return element.Element.Family[this.Parameters];
            else
                return null;
        }
    }
}
