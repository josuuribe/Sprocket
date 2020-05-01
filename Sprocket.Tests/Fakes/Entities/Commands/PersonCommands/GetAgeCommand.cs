using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    class GetAgeCommand : ExpressionCommand<Person, int>
    {
        public GetAgeCommand() : base() { }
        public GetAgeCommand(Person p) : base(p) { }
        protected internal override int Process(RuleElement<Person> element)
        {
            return element.Element.Age;
        }
    }
}
