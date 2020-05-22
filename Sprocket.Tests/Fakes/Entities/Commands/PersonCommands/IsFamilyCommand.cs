using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Linq;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    [DataContract]
    class IsFamilyCommand : Operand<Person, bool>
    {
        public Person Person { get; set; }
        public override bool Process(Rule<Person> element)
        {
            return element.Element.Family.Any(x => x.Id == Person.Id);
        }
    }
}
