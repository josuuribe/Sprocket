using RaraAvis.Sprocket.RuleEngine;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.PersonCommands
{
    [DataContract]
    public class WrongCommand : Operand<Person, bool>
    {
        public WrongCommand() { }
        public override bool Process(Person element)
        {
            element.Correct = false;
            return element.Correct;
        }
    }
}
