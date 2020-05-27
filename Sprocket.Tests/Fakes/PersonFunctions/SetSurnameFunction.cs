using RaraAvis.Sprocket.RuleEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    [DataContract]
    public class SetSurnameFunction : Operand<Person, bool>
    {
        [DataMember]
        string Parameter { get; set; }

        public SetSurnameFunction(string name)
        {
            this.Parameter = name;
        }

        public override bool Process(Person element)
        {
            element.Surname = this.Parameter;
            return true;
        }
    }
}
