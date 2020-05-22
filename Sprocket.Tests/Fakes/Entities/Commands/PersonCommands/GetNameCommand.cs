using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    class GetNameCommand : Operand<Person, string>
    {
        public GetNameCommand() : base() { }
        public GetNameCommand(Person p) : base(p) { }
        public override string Process(Rule<Person> element)
        {
            return element.Element.Name;
        }
    }
}
