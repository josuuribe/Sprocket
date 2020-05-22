﻿using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    [DataContract]
    class EatCommand : Operand<Person, bool>
    {
        public override bool Process(Rule<Person> element)
        {
            element.Element.Eat();
            return true;
        }
    }
}
