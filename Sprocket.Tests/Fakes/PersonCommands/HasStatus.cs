using RaraAvis.Sprocket.RuleEngine;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.PersonCommands
{
    [DataContract]
    class HasworflowEngineTestatus : Operand<Person, bool>
    {
        [DataMember]
        public worflowEngineTestatus worflowEngineTestatus { get; set; }

        public HasworflowEngineTestatus(worflowEngineTestatus worflowEngineTestatus)
        {
            this.worflowEngineTestatus = worflowEngineTestatus;
        }

        public override bool Process(Person element)
        {
            return element.worflowEngineTestatus == worflowEngineTestatus;
        }
    }
}
