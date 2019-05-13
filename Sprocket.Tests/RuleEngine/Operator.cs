using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.System;
using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class Operator
    {
        private static DistanceCommand dc = null;
        private static Person fakeElement = null;
        private static SerializeTest st = null;
        private static Operator<Person> op = null;
        private static Person p = null;


        public Operator()
        {
            dc = new DistanceCommand();
            st = new SerializeTest();
            p = new Person();
            st.BeginSerialize();
        }

        [Fact]
        public void AndAlso()
        {
            op = (dc < 10) & (dc < 5);

            var result = st.Match(op, p);

            Assert.True(result, "'&&' is false.");
        }

        [Fact]
        public void And()
        {
            op = (dc < 10) && (dc < 5);

            var result = st.Match(op, p);

            Assert.True(result, "'&&' is false.");
        }

        [Fact]
        public void AddResult()
        {
            op = (dc < 10) >> (int)Feature.ASIAN;

            var stage = st.CreateStage(1, "Stage-1", op);
            var result = st.ExecuteWorkflow(p, stage);

            Assert.True(((Feature)result.ruleElement.UserStatus & Feature.ASIAN) == Feature.ASIAN, "Invalid status");
            Assert.Equal(ExecutionEngineResult.OK, result.Item2);
        }

        [Fact]
        public void RemoveResult()
        {
            op = (dc < 10) << (int)Feature.ASIAN;

            var stage = st.CreateStage(1, "Stage-1", op);
            var result = st.ExecuteWorkflow(p, stage);

            Assert.Equal(0, result.ruleElement.UserStatus);
            Assert.Equal(ExecutionEngineResult.OK, result.Item2);
        }
    }
}
