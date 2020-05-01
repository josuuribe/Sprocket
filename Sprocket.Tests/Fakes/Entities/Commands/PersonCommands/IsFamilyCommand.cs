using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Linq;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    [DataContract]
    class IsFamilyCommand : Command<Person, bool>
    {
        public Person Person { get; set; }
        protected internal override bool Process(RuleElement<Person> element)
        {
            return element.Element.Family.Any(x => x.Id == Person.Id);
        }
    }
}
