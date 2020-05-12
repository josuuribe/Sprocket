using RaraAvis.Sprocket.RuleEngine.Elements.Operates;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    public class SetSurnameFunction : Function<Person, string, bool>
    {
        public override bool Value(Person element)
        {
            element.Surname = this.Parameters;
            return true;
        }
    }
}
