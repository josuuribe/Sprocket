using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    [DataContract]
    class EatCommand : Command<Person, bool>
    {
        protected internal override bool Process(RuleElement<Person> element)
        {
            element.Element.Eat();
            return true;
        }
    }
}
