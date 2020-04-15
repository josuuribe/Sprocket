using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    class RightCommand : Command<Person, bool>
    {
        public override bool Value(RuleElement<Person> element)
        {
            element.Element.Correct = true;
            return element.Element.Correct;
        }
    }
}
