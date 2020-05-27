using RaraAvis.Sprocket.RuleEngine;
using RaraAvis.Sprocket.Tests.Fakes.Entities;

namespace RaraAvis.Sprocket.Tests.Fakes.PersonCommands
{
    class GetNameCommand : Operand<Person, string>
    {
        public GetNameCommand() : base() { }
        public GetNameCommand(Person p) : base(p) { }
        public override string Process(Person element)
        {
            return element.Name;
        }
    }
}
