using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Parts.Elements.Operators;
using RaraAvis.Sprocket.Tests.Entities;
using RaraAvis.Sprocket.Tests.System;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Workflows;
using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{    
    public class SerializeTest
    {
        ActivateRuleEngine are;
        RuleElement<Person> re = null;

        public void BeginSerialize()
        {
            re = new RuleElement<Person>();
            are = new ActivateRuleEngine();
            are.CreateWorkflowPreprocess();
        }

        public void EndSerialize()
        {
            are.ClearAll();
        }

        public Stage CreateStage(int id, string name, Operator<Person> p)
        {
            return are.CreateStage(id, name, p);
        }

        public (RuleElement<Person> ruleElement, ExecutionEngineResult) ExecutePreWorkflow(Person p, params Stage[] stages)
        {
            Person p2 = (Person)p.Clone();
            re.Element = p;
            are.CreateWorkflowPreprocess();
            are.Stages = stages.ToList();
            are.Init(p2);
            return (are.RuleElement, are.ExecutionEngineResult);
        }

        public (bool resultMatch, ExecutionEngineResult executionEngineResult, RuleElement<Person> ruleElement) ExecuteWorkflow(Operator<Person> op, Person p)
        {
            Person p2 = (Person)p.Clone();
            re.Element = p;
            bool resultMatch = op.Match(re);
            are.CreateStage(1, "Test", op);
            are.Init(p2);
            return (resultMatch, are.ExecutionEngineResult, are.RuleElement);
        }

        public U Execute<U>(Command<Person,U> operate, Person p)
        {
            return are.Execute(operate, p);
        }
    }
}
