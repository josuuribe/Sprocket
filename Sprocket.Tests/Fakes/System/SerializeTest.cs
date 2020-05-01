using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Workflows;
using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;
using System.Linq;

namespace RaraAvis.Sprocket.Tests.Fakes.System
{
    public class SerializeTest
    {
        public ActivateRuleEngine ActivateRuleEngine { get; private set; }
        private RuleElement<Person> re = null;

        public void BeginSerialize()
        {
            re = new RuleElement<Person>();
            ActivateRuleEngine = new ActivateRuleEngine();
        }

        public void EndSerialize()
        {
            ActivateRuleEngine.ClearAll();
        }

        public int UserStatus
        {
            get
            {
                return re.UserStatus;
            }
        }

        public (RuleElement<Person> ruleElement, ExecutionEngineResult) ExecuteWorkflow(Person p, params Stage[] stages)
        {
            ActivateRuleEngine.Stages = stages.ToList();
            ActivateRuleEngine.Init(p);
            return (ActivateRuleEngine.RuleElement, ActivateRuleEngine.ExecutionEngineResult);
        }

        public bool Match(Operator<Person> op, Person p)
        {
            re.Element = p;
            return op.Match(re);
        }
    }
}
