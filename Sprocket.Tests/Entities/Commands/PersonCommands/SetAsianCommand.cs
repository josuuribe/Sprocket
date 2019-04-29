using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaraAvis.Sprocket.Tests.Entities.Commands.PersonCommands
{
    public class SetAsianCommand : Command<Person, bool>
    {
        public override bool Value(RuleElement<Person> element)
        {
            element.UserStatus = (int)Feature.ASIAN;
            return true;
        }
    }
}
