using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Parts.Elements.Operators;
using RaraAvis.Sprocket.Services;
using RaraAvis.Sprocket.Tests.Entities;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Workflows;
using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RaraAvis.Sprocket.Tests.System
{
    public class ActivateRuleEngine
    {
        public RuleElement<Person> RuleElement { get; private set; }
        public ExecutionEngineResult ExecutionEngineResult { get; private set; }

        IRuleEngineService<Person> re = null;

        public List<Stage> Stages { get; set; }
        RuleEngineActivatorService<Person> res;

        public ActivateRuleEngine()
        {
            res = new RuleEngineActivatorService<Person>();
            re = res.GetRuleEngine(Path.Combine(Path.GetDirectoryName(typeof(ActivateRuleEngine).GetTypeInfo().Assembly.Location), "RaraAvis.Sprocket.dll"));
            Stages = new List<Stage>();
        }

        public FakeWorkflow Workflow
        {
            get; private set;
        }

        public void ClearAll()
        {
            this.Workflow = null;
            this.Stages.Clear();
        }

        public Stage CreateStage(int id, string name, Operator<Person> op)
        {
            //Workflow = wf;

            //ActivityAssembly aan1 = new ActivityAssembly();
            //aan1.AssemblyPath = Path.Combine(AppContext.BaseDirectory, "RaraAvis.Sprocket.dll");

            ActivityAssembly aan = new ActivityAssembly();
            aan.AssemblyPath = Path.Combine(AppContext.BaseDirectory, "RaraAvis.Sprocket.Tests.dll");

            Stage stage = new Stage();
            stage.Id = id;
            stage.Name = name;

            stage.ActivitiesAssemblyNames.Add(aan);

            stage.XMLStage = re.Serialize(op, stage);

            Stages.Add(stage);

            return stage;
        }

        public void CreateWorkflowPreprocess()
        {
            FakeWorkflow workflowPreprocess = new FakeWorkflow();
            workflowPreprocess = new FakeWorkflow();
            workflowPreprocess.Type = WorkflowType.PREPROCESS;
            workflowPreprocess.Name = "Workflow Pre-Process ";
            this.Workflow = workflowPreprocess;
        }

        public void CreateStagePostPreprocess(int id, ExpressionOperator<Person> op)
        {
            FakeWorkflow workflowPostprocess = new FakeWorkflow();
            workflowPostprocess = new FakeWorkflow();
            workflowPostprocess.Type = WorkflowType.POSTPROCESS;
            workflowPostprocess.Name = "Workflow Post-Process";
            this.Workflow = workflowPostprocess;
        }

        public void Init(Person element)
        {
            re.Init(this.Workflow, Stages, element);

            RuleElement = re.WorkflowResults.Last();
            ExecutionEngineResult = re.ExecutionEngineResult;
        }

        public U Execute<U>(Command<Person, U> operate, Person p)
        {
            return re.Execute<U>(operate, p);
        }
    }
}
