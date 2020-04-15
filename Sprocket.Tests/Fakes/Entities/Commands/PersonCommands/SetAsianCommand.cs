using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    class SetAsianCommand : Command<Person, bool>
    {
        public override bool Value(RuleElement<Person> element)
        {
            element.UserStatus = (int)Feature.Asian;
            return true;
        }
    }
}
