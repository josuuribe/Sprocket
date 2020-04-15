using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.System;
using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;
using System;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class Operator : IDisposable
    {
        private static GetDistanceCommand dc = null;
        private static Person fakeElement = null;
        private static SerializeTest st = null;
        private static Operator<Person> op = null;
        private static Person p = null;


        public Operator()
        {
            dc = new GetDistanceCommand();
            st = new SerializeTest();
            p = new Person();
            st.BeginSerialize();
        }

        public void Dispose()
        {
            st.EndSerialize();
        }

        [Fact]
        public void AddResult()
        {
            op = (dc < 10) >> (int)Feature.Asian;

            var stage = st.CreateStage(1, "Stage-1", op);
            var result = st.ExecuteWorkflow(p, stage);

            Assert.True(((Feature)result.ruleElement.UserStatus & Feature.Asian) == Feature.Asian, "Invalid status");
            Assert.Equal(ExecutionEngineResult.OK, result.Item2);
        }

        [Fact]
        public void RemoveResult()
        {
            op = (dc < 10) << (int)Feature.Asian;

            var stage = st.CreateStage(1, "Stage-1", op);
            var result = st.ExecuteWorkflow(p, stage);

            Assert.Equal(0, result.ruleElement.UserStatus);
            Assert.Equal(ExecutionEngineResult.OK, result.Item2);
        }
    }
}
