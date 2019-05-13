using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Parts.Elements.Functions.Kernel;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.RuleEngine;
using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;

namespace RaraAvis.Sprocket.Tests.System
{
    [TestClass]
    public class Workflows
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


        [ClassInitialize]
        public static void Init(TestContext tc)
        {
            dc = new DistanceCommand();
            rc = new RunCommand();
            wc = new WalkCommand();
            gnf = new GetNameFunction();
            snf = new SetNameFunction();
            fakeElement = new Person();
            st = new SerializeTest();
        }

        [TestInitialize]
        public void InitTest()
        {
            p = new Person();
            st.BeginSerialize();
        }

        [TestCleanup]
        public void EndTest()
        {
            st.EndSerialize();
        }

        [TestMethod]
        public void OneStageWorkflowExecuted()
        {
            op = (dc < 10);

            var stage = st.CreateStage(1, "Stage-1", op);
            var result = st.ExecuteWorkflow(p, stage);

            Assert.AreEqual(result.Item2, ExecutionEngineResult.OK);
        }

        [TestMethod]
        public void OneStageWorkflowFailed()
        {
            op = (dc > 10);

            var stage = st.CreateStage(1, "Stage-1", op);
            var result = st.ExecuteWorkflow(p, stage);

            Assert.AreEqual(result.Item2, ExecutionEngineResult.KO);
        }

        [TestMethod]
        public void JMP()
        {
            var s1 = ((dc < 10) ^ "Stage-3");

            var s2 = (snf - "Name-2");

            var s3 = (snf - "Name-3");


            var stage1 = st.CreateStage(1, "Stage-1", s1);
            var stage2 = st.CreateStage(2, "Stage-2", s2);
            var stage3 = st.CreateStage(3, "Stage-3", s3);

            var res = st.ExecuteWorkflow(p, stage1, stage2, stage3);

            Assert.AreEqual(res.ruleElement.Element.Name, "Name-3");
        }

        [TestMethod]
        public void Break()
        {
            op = (dc < 10) % ~(snf - "new");

            var stage = st.CreateStage(1, "Stage-1", op);
            var result = st.ExecuteWorkflow(p, stage);

            Assert.AreEqual(result.ruleElement.Element.Name, "new", "Invalid name");
            Assert.AreEqual(result.Item2, ExecutionEngineResult.EXIT);
        }

        [TestMethod]
        public void AddResult()
        {
            op = (dc < 10) >> (int)Feature.ASIAN;

            var stage = st.CreateStage(1, "Stage-1", op);
            var result = st.ExecuteWorkflow(p, stage);

            Assert.IsTrue(((Feature)result.ruleElement.UserStatus & Feature.ASIAN) == Feature.ASIAN, "Invalid status");
            Assert.AreEqual(result.Item2, ExecutionEngineResult.OK);
        }

        [TestMethod]
        public void RemoveResult()
        {
            SetAsianCommand sac = new SetAsianCommand();
            //Batch<Person> batchAsian = new Batch<Person>();
            //batchAsian.Add(sac);

            op = (sac) << (int)Feature.ASIAN;

            var stage = st.CreateStage(1, "Stage-1", op);
            var res = st.ExecuteWorkflow(p, stage);

            Assert.AreEqual(res.ruleElement.UserStatus, 0, "Invalid status");
            Assert.AreEqual(res.Item2, ExecutionEngineResult.OK);
        }

        [TestMethod]
        public void Execute()
        {
            GetNameCommand gnc = new GetNameCommand();
            p.Name = "Name";

            var ope = (gnc);

            var res = st.Execute<string>(ope, p);

            Assert.AreEqual(res, "Name");
        }
    }
}
