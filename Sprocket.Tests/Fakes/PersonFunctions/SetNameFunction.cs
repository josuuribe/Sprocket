using RaraAvis.Sprocket.RuleEngine;
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

        public override bool Process(Person element)
        {
            element.Name = this.Parameter;
            return true;
        }
    }
}
