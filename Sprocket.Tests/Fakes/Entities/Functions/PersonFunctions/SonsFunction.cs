using RaraAvis.Sprocket.Parts.Elements.Commands.ExpressionOperators;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    class SonsFunction : Function<Person, int, Person>
    {
        public SonsFunction() : base() { }
        public SonsFunction(Person p, int i) : base(p, i) { }
        protected internal override Person Process(RuleElement<Person> element)
        {
            if (element.Element.Family.Count > 0)
                return element.Element.Family[this.Parameters];
            else
                return null;
        }
    }
}
