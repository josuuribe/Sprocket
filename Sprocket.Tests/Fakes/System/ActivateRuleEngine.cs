using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using RaraAvis.Sprocket.WorkflowEngine.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace RaraAvis.Sprocket.Tests.Fakes.System
{
    public class ActivateRuleEngine
    {
        private readonly IRuleEngineService<Person> res = null;

        public ActivateRuleEngine()
        {
            res = RuleEngineActivatorService<Person>.GetRuleEngine();
        }

        public Rule<Person> RunEngine(Operator<Person> op, Person p)
        {
            //ActivityAssembly aan = new ActivityAssembly();
            //aan.AssemblyPath = Path.Combine(AppContext.BaseDirectory, "RaraAvis.Sprocket.Tests.dll");
            //Stage stage = new Stage();
            //stage.Id = id;
            //stage.ActivitiesAssemblyNames.Add(aan);
            ////stage.XMLStage = res.Serialize(op, stage);
            //Stages.Add(stage);

            return res.Init(op, p);
        }

        //public Stage CreateFailedStageWrongAssemblyPath(Guid id, Operator<Person> op)
        //{
        //    ActivityAssembly aan = new ActivityAssembly();
        //    aan.AssemblyPath = Path.Combine(AppContext.BaseDirectory, "Wrong.dll");
        //    Stage stage = new Stage();
        //    stage.Id = id;
        //    stage.ActivitiesAssemblyNames.Add(aan);
        //    //stage.XMLStage = res.Serialize(op, stage);
        //    Stages.Add(stage);
        //    return stage;
        //}

        //public Rule<Person> Init(Person element)
        //{
        //    return res.Init(Stages, element);
        //}
    }
}
