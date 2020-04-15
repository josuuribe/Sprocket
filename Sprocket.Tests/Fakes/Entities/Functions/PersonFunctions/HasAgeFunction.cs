using RaraAvis.Sprocket.Parts.Elements.Functions;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    internal class HasAgeFunction : BooleanFunction<Person, int>
    {
        public override bool Value(RuleElement<Person> element)
        {
            return element.Element.Age == this.Parameters;
        }
    }
}
