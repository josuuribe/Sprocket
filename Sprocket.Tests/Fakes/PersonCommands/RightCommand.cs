using RaraAvis.Sprocket.RuleEngine;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.PersonCommands
{
    [DataContract]
    internal class RightCommand : Operand<Person, bool>
    {
        public RightCommand() : base() { }
        public RightCommand(Person p) : base(p) { }
        public override bool Process(Person element)
        {
            element.Correct = true;
            return element.Correct;
        }
    }
}
