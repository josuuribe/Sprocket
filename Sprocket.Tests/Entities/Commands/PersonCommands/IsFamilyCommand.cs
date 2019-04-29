using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RaraAvis.Sprocket.Tests.Entities.Commands.PersonCommands
{
    public class IsFamilyCommand : BooleanCommand<Person>
    {
        public Person Person { get; set; }
        public override bool Value(RuleElement<Person> element)
        {
            return element.Element.Family.Any(x => x.Id == Person.Id);
        }
    }
}
