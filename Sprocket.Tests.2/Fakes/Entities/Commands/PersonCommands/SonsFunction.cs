using RaraAvis.Sprocket.Parts.Elements.Commands.ExpressionOperators;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    public class SonsFunction : Function<Person, int, Person>
    {
        public override Person Execute(RuleElement<Person> element)
        {
            return element.Element.Family[this.Parameters];
        }
    }
}
