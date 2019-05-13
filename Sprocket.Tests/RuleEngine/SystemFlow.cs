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
    public class SystemFlow
    {
        private static DistanceCommand dc = null;
        private static RunCommand rc = null;
        private static WalkCommand wc = null;
        private static Person fakeElement = null;
        private static GetNameFunction gnf = null;
        private static SetNameFunction snf = null;
        private static SerializeTest st = null;
        private static Operator<Person> op = null;
        private static Person p = null;

        public SystemFlow()
        {
            dc = new DistanceCommand();
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

        [Trait("RuleEngine", "Commands")]
        [Fact]
        public void JMP()
        {
            var s1 = ((dc < 10) ^ "Stage-3");

            var s2 = (snf - "Name-2");

            var s3 = (snf - "Name-3");


            var stage1 = st.CreateStage(1, "Stage-1", s1);
            var stage2 = st.CreateStage(2, "Stage-2", s2);
            var stage3 = st.CreateStage(3, "Stage-3", s3);

            var res = st.ExecuteWorkflow(p, stage1, stage2, stage3);

            Assert.Equal("Name-3", res.ruleElement.Element.Name);
        }

        [Trait("RuleEngine", "Commands")]
        [Fact]
        public void Break()
        {
            op = (dc < 10) % ~(snf - "new");

            var stage = st.CreateStage(1, "Stage-1", op);
            var result = st.ExecuteWorkflow(p, stage);

            Assert.Equal("new", result.ruleElement.Element.Name);
            Assert.Equal(ExecutionEngineResult.EXIT, result.Item2);
        }

        [Trait("RuleEngine", "Commands")]
        [Fact]
        public void AddResult()
        {
            SetAsianCommand sac = new SetAsianCommand();

            op = (sac) >> (int)Feature.ASIAN;

            var stage = st.CreateStage(1, "Stage-1", op);
            var result = st.ExecuteWorkflow(p, stage);

            Assert.True(((Feature)result.ruleElement.UserStatus & Feature.ASIAN) == Feature.ASIAN, "Invalid status");
            Assert.Equal(ExecutionEngineResult.OK, result.Item2);
        }

        [Trait("RuleEngine", "Commands")]
        [Fact]
        public void RemoveResult()
        {
            SetAsianCommand sac = new SetAsianCommand();
            //Batch<Person> batchAsian = new Batch<Person>();
            //batchAsian.Add(sac);

            op = (sac) << (int)Feature.ASIAN;

            var stage = st.CreateStage(1, "Stage-1", op);
            var res = st.ExecuteWorkflow(p, stage);

            Assert.Equal(0, res.ruleElement.UserStatus);
            Assert.Equal(ExecutionEngineResult.OK, res.Item2);
        }
    }
}
