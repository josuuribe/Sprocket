using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Services;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Workflows;
using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;
using System;
using System.Collections.Generic;
using System.IO;

namespace RaraAvis.Sprocket.Tests.Fakes.System
{
    public class ActivateRuleEngine
    {
        public RuleElement<Person> RuleElement { get; private set; }
        public ExecutionEngineResult ExecutionEngineResult { get; private set; }

        IRuleEngineService<Person> res = null;

        public List<Stage> Stages { get; set; }
        RuleEngineActivatorService<Person> reas;

        public ActivateRuleEngine()
        {
            res = new RuleEngineActivatorService<Person>().GetRuleEngine();
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

            stage.XMLStage = res.Serialize(op, stage);

            Stages.Add(stage);

            return stage;
        }

        public void CreateWorkflow()
        {
            FakeWorkflow workflow = new FakeWorkflow();
            workflow = new FakeWorkflow();
            //workflowPreprocess.Type = WorkflowType.PREPROCESS;
            workflow.Code = "Workflow Pre-Process ";
            this.Workflow = workflow;
        }

        //public void CreateStagePostPreprocess(int id, ExpressionOperator<Person> op)
        //{
        //    FakeWorkflow workflowPostprocess = new FakeWorkflow();
        //    workflowPostprocess = new FakeWorkflow();
        //    workflowPostprocess.Type = WorkflowType.POSTPROCESS;
        //    workflowPostprocess.Name = "Workflow Post-Process";
        //    this.Workflow = workflowPostprocess;
        //}

        public void Init(Person element)
        {
            this.RuleElement = res.Init(this.Workflow, Stages, element);
            this.ExecutionEngineResult = res.ExecutionEngineResult;
        }

        public U Execute<U>(Command<Person, U> operate, Person p)
        {
            return res.Execute<U>(operate, p);
        }
    }
}
