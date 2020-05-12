using RaraAvis.Sprocket.RuleEngine.Elements.Operates;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Linq;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    [DataContract]
    class IsFamilyCommand : Command<Person, bool>
    {
        public Person Person { get; set; }
        public override bool Value(Person element)
        {
            return element.Family.Any(x => x.Id == Person.Id);
        }
    }
}
