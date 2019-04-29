﻿using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.Tests.Entities;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Workflows;

namespace RaraAvis.Sprocket.Tests.Entities.Commands.PersonCommands
{
    public class WalkCommand : BooleanCommand<Person>
    {
        public override bool Value(RuleElement<Person> element)
        {
            element.Element.Walk();
            return true;
        }
    }
}
