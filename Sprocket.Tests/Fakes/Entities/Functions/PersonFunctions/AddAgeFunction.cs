using RaraAvis.Sprocket.Parts.Elements.Functions;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    public class AddAgeFunction : BooleanFunction<Person, int>
    {
        public override bool Value(RuleElement<Person> element)
        {
            element.Element.Age += this.Parameters;
            return true;
        }
    }
}
