using RaraAvis.Sprocket.RuleEngine.Elements.Operates;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    public class WalkCommand : Command<Person, bool>
    {
        public override bool Value(Person element)
        {
            element.Walk();
            return true;
        }
    }
}
