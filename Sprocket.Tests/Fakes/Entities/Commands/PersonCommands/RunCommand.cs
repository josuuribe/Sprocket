using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    class RunCommand : Command<Person, bool>
    {
        protected internal override bool Process(RuleElement<Person> element)
        {
            element.Element.Run();
            return true;
        }
    }
}
