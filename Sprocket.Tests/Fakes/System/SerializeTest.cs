using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using RaraAvis.Sprocket.WorkflowEngine.Services;
using System.Linq;

namespace RaraAvis.Sprocket.Tests.Fakes.System
{
    public class WorflowEngineTest
    {
        private readonly IRuleEngineService<Person> ruleEngineService;
        private IOperator<Person> op;

        private string serialized;

        public WorflowEngineTest()
        {
            ruleEngineService = RuleEngineActivatorService<Person>.GetRuleEngine();
        }

        public Rule<Person> Start(Operator<Person> @operator, Person p)
        {
            serialized = ruleEngineService.Serialize(@operator);
            this.op = ruleEngineService.Deserialize(serialized);
            return ruleEngineService.Init(op, p);
        }

        public bool Match(Operator<Person> @operator, Person p)
        {
            serialized = ruleEngineService.Serialize(@operator);
            this.op = ruleEngineService.Deserialize(serialized);
            Rule<Person> rule = new Rule<Person>(p);
            return (op as Operator<Person>).Operate(rule);
        }
    }
}
