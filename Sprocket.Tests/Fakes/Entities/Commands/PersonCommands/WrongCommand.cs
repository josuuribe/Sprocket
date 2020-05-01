using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    internal class WrongCommand : Command<Person, bool>
    {
        public WrongCommand() : base() { }
        public WrongCommand(Person p) : base(p) { }
        protected internal override bool Process(RuleElement<Person> element)
        {
            element.Element.Correct = false;
            return element.Element.Correct;
        }
    }
}
