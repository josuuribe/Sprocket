using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions;
using RaraAvis.Sprocket.Tests.Fakes.System;
using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;
using System;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class Workflows : IDisposable
    {
        private static GetDistanceCommand dc = null;
        private static RunCommand rc = null;
        private static WalkCommand wc = null;
        private static Person fakeElement = null;
        private static GetNameFunction gnf = null;
        private static SetNameFunction snf = null;
        private static SerializeTest st = null;
        private static Operator<Person> op = null;
        private static Person p = null;

        public Workflows()
        {
            dc = new GetDistanceCommand();
            rc = new RunCommand();
            wc = new WalkCommand();
            gnf = new GetNameFunction();
            snf = new SetNameFunction();
            fakeElement = new Person();
            st = new SerializeTest();
            p = new Person();
            st.BeginSerialize();
        }

        public void Dispose()
        {
            st.EndSerialize();
        }

        [Trait("RuleEngine", "Workflows")]
        [Fact]
        public void OneStageWorkflowExecuted()
        {
            op = (dc < 10);

            var stage = st.CreateStage(1, "Stage-1", op);
            var result = st.ExecuteWorkflow(p, stage);

            Assert.Equal(ExecutionEngineResult.OK, result.Item2);
        }

        [Trait("RuleEngine", "Workflows")]
        [Fact]
        public void OneStageWorkflowFailed()
        {
            op = (dc > 10);

            var stage = st.CreateStage(1, "Stage-1", op);
            var result = st.ExecuteWorkflow(p, stage);

            Assert.Equal(ExecutionEngineResult.KO, result.Item2);
        }
    }
}
