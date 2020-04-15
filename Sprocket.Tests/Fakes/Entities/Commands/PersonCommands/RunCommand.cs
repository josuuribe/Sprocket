using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    class RunCommand : BooleanCommand<Person>
    {
        public override bool Value(RuleElement<Person> element)
        {
            element.Element.Run();
            return true;
        }
    }
}
