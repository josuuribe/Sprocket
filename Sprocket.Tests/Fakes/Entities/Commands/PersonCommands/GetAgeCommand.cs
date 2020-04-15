using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    class GetAgeCommand : ExpressionCommand<Person, int>
    {
        public override int Value(RuleElement<Person> element)
        {
            return element.Element.Age;
        }
    }
}
