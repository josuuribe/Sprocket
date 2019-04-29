using RaraAvis.Sprocket.Parts.Elements.Functions;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Entities.Commands.PersonCommands
{
    public class SetNameFunction : BooleanFunction<Person, string>
    {
        public override bool Execute(RuleElement<Person> element)
        {
            element.Element.Name = this.Parameters;
            return true;
        }
    }
}
