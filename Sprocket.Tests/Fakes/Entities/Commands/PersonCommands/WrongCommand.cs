using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Elements.Operates;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    [DataContract]
    public class WrongCommand : Command<Person, bool>
    {
        public WrongCommand() : base() { }
        public WrongCommand(Person p) : base(p) { }
        public override bool Value(Person element)
        {
            element.Correct = false;
            return element.Correct;
        }
    }
}
