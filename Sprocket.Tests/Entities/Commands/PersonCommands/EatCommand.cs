using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Entities.Commands.PersonCommands
{
    public class EatCommand : BooleanCommand<Person>
    {
        public override bool Value(RuleElement<Person> element)
        {
            element.Element.Eat();
            return true;
        }
    }
}
