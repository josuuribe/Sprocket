using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    class GetNameCommand : Command<Person, string>
    {
        public GetNameCommand() : base() { }
        public GetNameCommand(Person p) : base(p) { }
        protected internal override string Process(RuleElement<Person> element)
        {
            return element.Element.Name;
        }
    }
}
