using RaraAvis.Sprocket.RuleEngine;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.PersonCommands
{
    [DataContract]
    public class GetAgeCommand : Operand<Person, int>
    {
        public GetAgeCommand() { }
        public GetAgeCommand(Person p) : base(p) { }
        public override int Process(Person element)
        {
            return element.Age;
        }
    }
}
