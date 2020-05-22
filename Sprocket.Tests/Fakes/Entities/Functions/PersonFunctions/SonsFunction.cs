using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions
{
    public class SonsFunction : Operand<Person, Person>
    {
        public int Parameter { get; set; }

        public SonsFunction(int i)  {
            this.Parameter = i;
        }
        public override Person Process(Rule<Person> element)
        {
            if (element.Element.Family.Count > 0)
                return element.Element.Family[this.Parameter];
            else
                return null;
        }
    }
}
