using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    [DataContract]
    public class WakeUpCommand : Operand<Person, Status>
    {
        public override Status Process(Rule<Person> element)
        {
            element.Element.WakeUp();
            return element.Element.Status;
        }
    }
}
