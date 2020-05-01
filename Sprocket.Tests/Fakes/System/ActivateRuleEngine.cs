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
        private readonly IRuleEngineService<Person> res = null;

        public RuleElement<Person> RuleElement { get; private set; }
        public ExecutionEngineResult ExecutionEngineResult { get; private set; }
        public List<Stage> Stages { get; set; }

        public ActivateRuleEngine()
        {
            res = new RuleEngineActivatorService<Person>().GetRuleEngine();
            Stages = new List<Stage>();
        }

        public void ClearAll()
        {
            this.Stages.Clear();
        }

        public Stage CreateStage(int id, string name, Operator<Person> op)
        {
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

        public Stage CreateFailedStageWrongAssemblyPath(int id, string name, Operator<Person> op)
        {
            ActivityAssembly aan = new ActivityAssembly();
            aan.AssemblyPath = Path.Combine(AppContext.BaseDirectory, "Wrong.dll");
            Stage stage = new Stage();            
            stage.Id = id;
            stage.Name = name;
            stage.ActivitiesAssemblyNames.Add(aan);
            stage.XMLStage = res.Serialize(op, stage);
            Stages.Add(stage);
            return stage;
        }

        public void Init(Person element)
        {
            this.RuleElement = res.Init(Stages, element);
            this.ExecutionEngineResult = res.ExecutionEngineResult;
        }
    }
}
