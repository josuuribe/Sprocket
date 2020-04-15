using RaraAvis.Sprocket.Parts.Elements.Functions;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    public class SetSurnameFunction : BooleanFunction<Person, string>
    {
        public override bool Value(RuleElement<Person> element)
        {
            element.Element.Surname = this.Parameters;
            return true;
        }
    }
}
