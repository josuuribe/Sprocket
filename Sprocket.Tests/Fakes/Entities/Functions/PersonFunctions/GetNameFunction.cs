using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Elements.Operates;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    [DataContract]
    public class GetNameFunction : Function<Person, Operand<Person, Person>, string>
    {
        public GetNameFunction(Person p) : base(p) { }
        public GetNameFunction(Person p, Operand<Person, Person> op) : base(p, op) { }

        public override string Value(Person element)
        {
            Person p = this.Parameters.Value(element);
            return p.Name;
        }
    }
}
