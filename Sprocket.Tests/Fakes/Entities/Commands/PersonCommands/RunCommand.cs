using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    public class RunCommand : Command<Person, bool>
    {
        public override bool Value(RuleElement<Person> element)
        {
            element.Element.Run();
            return true;
        }
    }
}
