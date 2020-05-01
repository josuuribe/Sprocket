using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    class SleepCommand : Command<Person, Status>
    {
        protected internal override Status Process(RuleElement<Person> element)
        {
            element.Element.Sleep();
            return element.Element.Status;
        }
    }
}
