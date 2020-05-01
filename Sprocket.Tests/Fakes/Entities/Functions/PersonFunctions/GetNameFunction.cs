using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Parts.Elements.Commands.ExpressionOperators;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    class GetNameFunction : Function<Person, Operate<Person, Person>, string>
    {
        public GetNameFunction(Person p) : base(p) { }
        public GetNameFunction(Person p, Operate<Person, Person> op) : base(p, op) { }

        protected internal override string Process(RuleElement<Person> element)
        {
            Person p = this.Parameters.Process(element);
            return p.Name;
        }
    }
}
