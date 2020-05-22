using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    [DataContract]
    public class SetNameFunction : Operand<Person, bool>
    {
        [DataMember]
        string Parameter { get; set; }

        public SetNameFunction(string name)
        {
            this.Parameter = name;
        }

        public override bool Process(Rule<Person> element)
        {
            element.Element.Name = this.Parameter;
            return true;
        }
    }
}
