using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Elements.Operates;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    [DataContract]
    internal class RightCommand : Command<Person, bool>
    {
        public RightCommand() : base() { }
        public RightCommand(Person p) : base(p) { }
        public override bool Value(Person element)
        {
            element.Correct = true;
            return element.Correct;
        }
    }
}
