using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    class SleepCommand : Command<Person, Status>
    {
        public override Status Value(RuleElement<Person> element)
        {
            element.Element.Sleep();
            return element.Element.Status;
        }
    }
}
