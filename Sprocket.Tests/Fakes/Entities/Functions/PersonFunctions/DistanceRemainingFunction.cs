using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    [DataContract]
    public class DistanceRemainingFunction : Operand<Person, int>
    {
        [DataMember]
        int Parameter { get; set; }
        public DistanceRemainingFunction(int i) {
            this.Parameter = i;
        }
        public override int Process(Rule<Person> element)
        {
            return this.Parameter - element.Element.DistanceTravelled;
        }
    }
}
