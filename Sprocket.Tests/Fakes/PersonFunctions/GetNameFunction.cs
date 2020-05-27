using RaraAvis.Sprocket.RuleEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    [DataContract]
    public class GetNameFunction : Operand<Person, string>
    {
        [DataMember]
        public Operand<Person, Person> Parameter { get; set; }

        public GetNameFunction(Operand<Person, Person> op) { }

        public override string Process(Person element)
        {
            Person p = this.Parameter.Process(element);
            return p.Name;
        }
    }
}
