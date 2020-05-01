using RaraAvis.Sprocket.Parts.Elements.Commands.ExpressionOperators;
using RaraAvis.Sprocket.Parts.Elements.Functions;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    internal class HasAgeFunction : Function<Person, int, bool>
    {
        public HasAgeFunction(int parameter) : base(default(Person), parameter)
        { }

        protected internal override bool Process(RuleElement<Person> element)
        {
            return element.Element.Age == this.Parameters;
        }
    }
}
