using RaraAvis.Sprocket.RuleEngine;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    public class SonsFunction : Operand<Person, Person>
    {
        public int Parameter { get; set; }

        public SonsFunction(int i)
        {
            this.Parameter = i;
        }
        public override Person Process(Person element)
        {
            if (element.Family.Count > 0)
                return element.Family[this.Parameter];
            else
                return null;
        }
    }
}
