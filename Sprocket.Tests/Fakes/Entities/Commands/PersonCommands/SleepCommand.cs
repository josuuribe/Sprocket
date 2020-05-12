using RaraAvis.Sprocket.RuleEngine.Elements.Operates;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    public class SleepCommand : Command<Person, Status>
    {
        public override Status Value(Person element)
        {
            element.Sleep();
            return element.Status;
        }
    }
}
