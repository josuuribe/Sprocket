using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    [DataContract]
    public class GetNameFunction : Operand<Person, string>
    {
        [DataMember]
        public Operand<Person, Person> Parameter { get; set; }

        public GetNameFunction(Operand<Person, Person> op) { }

        public override string Process(Rule<Person> element)
        {
            Person p = this.Parameter.Process(element);
            return p.Name;
        }
    }
}
