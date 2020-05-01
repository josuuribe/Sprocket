﻿using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    class SetAsianCommand : Command<Person, bool>
    {
        public SetAsianCommand() : base() { }
        public SetAsianCommand(Person p) : base(p) { }
        protected internal override bool Process(RuleElement<Person> element)
        {
            element.UserStatus = (int)Feature.Asian;
            return true;
        }
    }
}
