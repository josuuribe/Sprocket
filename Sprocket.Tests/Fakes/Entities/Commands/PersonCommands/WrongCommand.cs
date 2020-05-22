using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    [DataContract]
    public class WrongCommand : Operand<Person, bool>
    {
        public WrongCommand() { }
        public override bool Process(Rule<Person> element)
        {
            element.Element.Correct = false;
            return element.Element.Correct;
        }
    }
}
