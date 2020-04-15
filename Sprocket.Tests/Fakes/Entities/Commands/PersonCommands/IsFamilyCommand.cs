using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Linq;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    class IsFamilyCommand : BooleanCommand<Person>
    {
        public Person Person { get; set; }
        public override bool Value(RuleElement<Person> element)
        {
            return element.Element.Family.Any(x => x.Id == Person.Id);
        }
    }
}
