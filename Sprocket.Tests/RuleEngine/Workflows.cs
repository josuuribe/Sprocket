using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions;
using RaraAvis.Sprocket.Tests.Fakes.System;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class Workflows : IDisposable
    {
        private static SerializeTest st = null;

        public Workflows()
        {
            st = new SerializeTest();
            st.BeginSerialize();
        }

        public void Dispose()
        {
            st.EndSerialize();
        }

        [Trait("RuleEngine", "Workflows")]
        [Fact]
        public void OneStageWorkflow_Positive()
        {
            GetDistanceCommand dc = new GetDistanceCommand();
            Operator<Person> op = (dc < 10);
            Person p = new Person();

            var stage = st.ActivateRuleEngine.CreateStage(1, "Stage-1", op);
            var result = st.ExecuteWorkflow(p, stage);

            Assert.Equal(ExecutionEngineResult.OK, result.Item2);
        }

        [Trait("RuleEngine", "Workflows")]
        [Fact]
        public void OneStageWorkflow_Negative()
        {
            GetDistanceCommand dc = new GetDistanceCommand();
            Person p = new Person();

            Operator<Person> op = (dc > 10);
            var stage = st.ActivateRuleEngine.CreateStage(1, "Stage-1", op);
            var result = st.ExecuteWorkflow(p, stage);

            Assert.Equal(ExecutionEngineResult.KO, result.Item2);
        }

        [Trait("RuleEngine", "Workflows")]
        [Fact]
        public void OneStageWorkflow_Failed()
        {
            IsFamilyCommand ifc = new IsFamilyCommand();
            Person p = new Person();
            p.Family = null;

            Operator<Person> op = (ifc);

            var stage = st.ActivateRuleEngine.CreateStage(1, "Stage-1", op);
            var result = st.ExecuteWorkflow(p, stage);

            Assert.Equal(ExecutionEngineResult.ERROR, result.Item2);
        }

        [Trait("RuleEngine", "Workflows")]
        [Fact]
        public void OneStageWorkflow_WrongAssemblyPath()
        {
            IsFamilyCommand ifc = new IsFamilyCommand();
            Person p = new Person();
            p.Family = null;

            Operator<Person> op = (ifc);

            Assert.ThrowsAny<Exception>(() => st.ActivateRuleEngine.CreateFailedStageWrongAssemblyPath(1, "Stage-1", op));
        }

        [Trait("RuleEngine", "Workflows")]
        [Fact]
        public void OneStageWorkflow_CorrectStage()
        {
            var stage = st.ActivateRuleEngine.CreateStage(1, "Stage", null);

            Assert.Equal(1, stage.ActivitiesAssemblyNames.First().Version);
            Assert.Equal(Path.GetFileName(stage.ActivitiesAssemblyNames.First().AssemblyName), stage.ActivitiesAssemblyNames.First().AssemblyName);
            Assert.Equal(Path.Combine(AppContext.BaseDirectory, "RaraAvis.Sprocket.Tests.dll"), stage.ActivitiesAssemblyNames.First().AssemblyPath);
        }
    }
}
