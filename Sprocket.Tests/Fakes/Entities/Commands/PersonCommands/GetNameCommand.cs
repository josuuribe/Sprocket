using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    class GetNameCommand : Command<Person, string>
    {
        public override string Value(RuleElement<Person> element)
        {
            return element.Element.Name;
        }
    }
}
