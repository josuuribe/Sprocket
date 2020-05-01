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
    public class SystemFlow : IDisposable
    {
        private static GetDistanceCommand rc = null;
        private static SetNameFunction snf = null;
        private static SerializeTest st = null;
        private static Operator<Person> op = null;
        private static Person p = null;

        public SystemFlow()
        {
            rc = new GetDistanceCommand();
            snf = new SetNameFunction();
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
        public void Operator_JMP()
        {
            var s1 = ((rc < 10) ^ 3);
            var s2 = (snf - "Name-2");
            var s3 = (snf - "Name-3");

            var stage1 = st.ActivateRuleEngine.CreateStage(1, "Stage-1", s1);
            var stage2 = st.ActivateRuleEngine.CreateStage(2, "Stage-2", s2);
            var stage3 = st.ActivateRuleEngine.CreateStage(3, "Stage-3", s3);

            var res = st.ExecuteWorkflow(p, stage1, stage2, stage3);

            Assert.Equal("Name-3", res.ruleElement.Element.Name);
        }

        [Trait("RuleEngine", "Commands")]
        [Fact]
        public void Operate_JMP()
        {
            RightCommand rc = new RightCommand();
            var s1 = rc ^ 3;
            var s2 = (snf - "Name-2");
            var s3 = (snf - "Name-3");

            var stage1 = st.ActivateRuleEngine.CreateStage(1, "Stage-1", s1);
            var stage2 = st.ActivateRuleEngine.CreateStage(2, "Stage-2", s2);
            var stage3 = st.ActivateRuleEngine.CreateStage(3, "Stage-3", s3);

            var res = st.ExecuteWorkflow(p, stage1, stage2, stage3);

            Assert.Equal("Name-3", res.ruleElement.Element.Name);
        }

        [Trait("RuleEngine", "Commands")]
        [Fact]
        public void Break()
        {
            op = ~(snf - "new");

            var stage = st.ActivateRuleEngine.CreateStage(1, "Stage-1", op);
            var result = st.ExecuteWorkflow(p, stage);

            Assert.Equal("new", result.ruleElement.Element.Name);
            Assert.Equal(ExecutionEngineResult.EXIT, result.Item2);
        }

        [Trait("RulEngine", "Commands")]
        [Fact]
        public void Break_ExpressionOperator_True()
        {
            GetDistanceCommand dc = new GetDistanceCommand();

            op = ~(dc < 5);

            var stage = st.ActivateRuleEngine.CreateStage(1, "Stage-1", op);
            var result = st.ExecuteWorkflow(p, stage);

            Assert.Equal(0, p.DistanceTravelled);
            Assert.Equal(ExecutionEngineResult.EXIT, result.Item2);
        }

        [Trait("RulEngine", "Commands")]
        [Fact]
        public void Break_ExpressionOperator_False()
        {
            GetDistanceCommand dc = new GetDistanceCommand();

            op = ~(dc > 5);

            var stage = st.ActivateRuleEngine.CreateStage(1, "Stage-1", op);
            var result = st.ExecuteWorkflow(p, stage);

            Assert.Equal(0, p.DistanceTravelled);
            Assert.Equal(ExecutionEngineResult.KO, result.Item2);
        }
    }
}
