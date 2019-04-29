using RaraAvis.Sprocket.Parts.Elements.Commands.ExpressionOperators;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Entities.Commands.PersonCommands
{
    public class GetNameFunction : Function<Person, IOperate<Person, Person>, string>
    {
        public override string Execute(RuleElement<Person> element)
        {
            Person p = this.Parameters.Value(element);
            return "Get:" + p.Name;
        }
    }
}
