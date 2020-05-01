using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    [DataContract]
    internal class RightCommand : Command<Person, bool>
    {
        public RightCommand() : base() { }
        public RightCommand(Person p) : base(p) { }
        protected internal override bool Process(RuleElement<Person> element)
        {
            element.Element.Correct = true;
            return element.Element.Correct;
        }
    }
}
