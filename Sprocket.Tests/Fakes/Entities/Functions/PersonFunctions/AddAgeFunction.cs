using RaraAvis.Sprocket.Parts.Elements.Commands.ExpressionOperators;
using RaraAvis.Sprocket.Parts.Elements.Functions;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    public class AddAgeFunction : Function<Person, int, bool>
    {
        public AddAgeFunction()
        { }
        public AddAgeFunction(int parameter) : base(default(Person), parameter)
        { }
        protected internal override bool Process(RuleElement<Person> element)
        {
            element.Element.Age += this.Parameters;
            return true;
        }
    }
}
