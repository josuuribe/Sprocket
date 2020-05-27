using RaraAvis.Sprocket.RuleEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    [DataContract]
    public class DiworflowEngineTestanceRemainingFunction : Operand<Person, int>
    {
        [DataMember]
        int Parameter { get; set; }
        public DiworflowEngineTestanceRemainingFunction(int i)
        {
            this.Parameter = i;
        }
        public override int Process(Person element)
        {
            return this.Parameter - element.DistanceTravelled;
        }
    }
}
