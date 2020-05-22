using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
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
        public override bool Process(Rule<Person> element)
        {
            element.Element.Age += this.Parameter;
            return true;
        }
    }
}
