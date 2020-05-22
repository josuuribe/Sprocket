using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    [DataContract]
    internal class RightCommand : Operand<Person, bool>
    {
        public RightCommand() : base() { }
        public RightCommand(Person p) : base(p) { }
        public override bool Process(Rule<Person> element)
        {
            element.Element.Correct = true;
            return element.Element.Correct;
        }
    }
}
