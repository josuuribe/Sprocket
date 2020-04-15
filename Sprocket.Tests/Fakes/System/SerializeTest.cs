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
        ActivateRuleEngine are;
        RuleElement<Person> re = null;

        public void BeginSerialize()
        {
            re = new RuleElement<Person>();
            are = new ActivateRuleEngine();
        }

        public void EndSerialize()
        {
            are.ClearAll();
        }

        public int UserStatus
        {
            get
            {
                return re.UserStatus;
            }
        }


        public Stage CreateStage(int id, string name, Operator<Person> p)
        {
            return are.CreateStage(id, name, p);
        }

        public (RuleElement<Person> ruleElement, ExecutionEngineResult) ExecuteWorkflow(Person p, params Stage[] stages)
        {
            are.CreateWorkflow();
            are.Stages = stages.ToList();
            are.Init(p);
            return (are.RuleElement, are.ExecutionEngineResult);
        }

        public bool Match(Operator<Person> op, Person p)
        {
            re.Element = p;
            return op.Match(re);
        }

        public Stage CreateStage(string nameStage, Operator<Person> op, Person p)
        {
            return are.CreateStage(1, nameStage, op);
        }

        public U Execute<U>(Command<Person, U> operate, Person p)
        {
            return are.Execute(operate, p);
        }
    }
}
