using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    [DataContract]
    public class GetAgeCommand : Operand<Person, int>
    {
        public GetAgeCommand() { }
        public GetAgeCommand(Person p) : base(p) { }
        public override int Process(Rule<Person> element)
        {
            return element.Element.Age;
        }
    }
}
