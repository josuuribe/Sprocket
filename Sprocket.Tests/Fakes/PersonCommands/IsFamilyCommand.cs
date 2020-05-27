using RaraAvis.Sprocket.RuleEngine;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using System.Linq;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.PersonCommands
{
    [DataContract]
    internal class IsFamilyCommand : Operand<Person, bool>
    {
        public Person Person { get; set; }
        public override bool Process(Person element)
        {
            return element.Family.Any(x => x.Id == Person.Id);
        }
    }
}
