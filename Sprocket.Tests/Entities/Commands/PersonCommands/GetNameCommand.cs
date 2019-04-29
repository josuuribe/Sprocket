using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaraAvis.Sprocket.Tests.Entities.Commands.PersonCommands
{
    public class GetNameCommand : Command<Person, string>
    {
        public override string Value(RuleElement<Person> element)
        {
            return element.Element.Name;
        }
    }
}
