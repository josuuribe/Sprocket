using RaraAvis.Sprocket.RuleEngine;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.PersonCommands
{
    [DataContract]
    public class WalkCommand : Operand<Person, bool>
    {
        public override bool Process(Person element)
        {
            element.Walk();
            return true;
        }
    }
}
