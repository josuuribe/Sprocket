using RaraAvis.Sprocket.RuleEngine;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.PersonCommands
{
    [DataContract]
    public class WakeUpCommand : Operand<Person, worflowEngineTestatus>
    {
        public override worflowEngineTestatus Process(Person element)
        {
            element?.WakeUp();
            return element.worflowEngineTestatus;
        }
    }
}
