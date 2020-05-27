using RaraAvis.Sprocket.RuleEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    [DataContract]
    public class AddAgeFunction : Operand<Person, bool>
    {
        [DataMember]
        public int Parameter { get; set; }
        public AddAgeFunction()
        { }
        public AddAgeFunction(int parameter)
        {
            this.Parameter = parameter;
        }
        public override bool Process(Person element)
        {
            element.Age += this.Parameter;
            return true;
        }
    }
}
