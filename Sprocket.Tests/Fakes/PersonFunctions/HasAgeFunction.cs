using RaraAvis.Sprocket.RuleEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    [DataContract]
    public class HasAgeFunction : Operand<Person, bool>
    {
        [DataMember]
        public int Parameter { get; set; }

        public HasAgeFunction() : base()
        {

        }
        public HasAgeFunction(int parameter)
        {
            this.Parameter = parameter;
        }

        public override bool Process(Person element)
        {
            return element.Age == this.Parameter;
        }
    }
}
