using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    class WrongCommand : Command<Person, bool>
    {
        public override bool Value(RuleElement<Person> element)
        {
            element.Element.Correct = false;
            return element.Element.Correct;
        }
    }
}
