using RaraAvis.Sprocket.RuleEngine;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.PersonCommands
{
    [DataContract]
    public class SleepCommand : Operand<Person, worflowEngineTestatus>
    {
        public override worflowEngineTestatus Process(Person element)
        {
            element.Sleep();
            return element.worflowEngineTestatus;
        }
    }
}
