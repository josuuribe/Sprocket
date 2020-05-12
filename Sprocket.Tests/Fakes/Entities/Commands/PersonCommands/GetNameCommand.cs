using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Elements.Operates;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    class GetNameCommand : Command<Person, string>
    {
        public GetNameCommand() : base() { }
        public GetNameCommand(Person p) : base(p) { }
        public override string Value(Person element)
        {
            return element.Name;
        }
    }
}
