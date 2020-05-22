using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
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

        public override bool Process(Rule<Person> element)
        {
            return element.Element.Age == this.Parameter;
        }
    }
}
