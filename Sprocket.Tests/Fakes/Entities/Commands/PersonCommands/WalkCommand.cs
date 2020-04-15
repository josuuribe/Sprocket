using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    class WalkCommand : BooleanCommand<Person>
    {
        public override bool Value(RuleElement<Person> element)
        {
            element.Element.Walk();
            return true;
        }
    }
}
