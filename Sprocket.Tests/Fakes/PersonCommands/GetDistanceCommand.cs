using RaraAvis.Sprocket.RuleEngine;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.PersonCommands
{
    [DataContract]
    public class GetDistanceCommand : Operand<Person, int>
    {
        public override int Process(Person element)
        {
            return element.DistanceTravelled;
        }
    }
}
