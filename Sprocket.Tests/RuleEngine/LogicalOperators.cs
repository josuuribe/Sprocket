using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Parts.Elements.Functions.Kernel;
using RaraAvis.Sprocket.Tests.Entities;
using RaraAvis.Sprocket.Tests.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    [TestClass]
    public class LogicalOperators
    {
        private static RunCommand rc = null;
        private static WalkCommand wc = null;
        private static RuleElement<Person> re = null;
        private static DistanceCommand dc = null;
        private static SerializeTest st = null;
        private static Operator<Person> op = null;
        private static Person p = null;
        private static FalseCommand fc = null;

        [ClassInitialize]
        public static void Init(TestContext tc)
        {
            rc = new RunCommand();
            wc = new WalkCommand();
            dc = new DistanceCommand();
            re = new RuleElement<Person>();
            st = new SerializeTest();
            fc = new FalseCommand();
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

        [TestCategory("LogicalOperators")]
        [TestMethod]
        public void AndAlsoAIsTrue()
        {
            op = (rc + wc) & (rc + wc);

            var res = st.ExecuteWorkflow(op, p);

            Assert.IsTrue(p.DistanceTravelled == 6 && res.resultMatch, "'And' operator (&) is not true.");
            Assert.AreEqual(res.executionEngineResult, ExecutionEngineResult.COMPLETED);
        }

        [TestCategory("LogicalOperators")]
        [TestMethod]
        public void AndAlsoIsFalse()
        {
            op = (rc + wc) & !(rc + wc);

            var res = st.ExecuteWorkflow(op, p);

            Assert.IsTrue(p.DistanceTravelled == 6 && !res.resultMatch, "'And' operator (&) is not false.");
            Assert.AreEqual(res.executionEngineResult, ExecutionEngineResult.FAILED);
        }

        [TestCategory("LogicalOperators")]
        [TestMethod]
        public void AndIsTrue()
        {
            op = (rc + wc) && (rc + wc);

            var res = st.ExecuteWorkflow(op, p);

            Assert.IsTrue(p.DistanceTravelled == 6 && res.resultMatch, "'And' operator (&&) is false.");
            Assert.AreEqual(res.executionEngineResult, ExecutionEngineResult.COMPLETED);
        }

        [TestCategory("LogicalOperators")]
        [TestMethod]
        public void AndIsFalse()
        {
            op = !(rc + wc) && (rc + wc);

            var res = st.ExecuteWorkflow(op, p);

            Assert.IsTrue(p.DistanceTravelled == 3 && !res.resultMatch, "'And' operator (&&) is true.");
            Assert.AreEqual(res.executionEngineResult, ExecutionEngineResult.FAILED);
        }

        [TestCategory("LogicalOperators")]
        [TestMethod]
        public void OrElseIsTrue()
        {
            op = (rc + wc) | (rc + wc);

            var res = st.ExecuteWorkflow(op, p);

            Assert.IsTrue(p.DistanceTravelled == 6 && res.resultMatch, "'Or' operator (|) is true.");
            Assert.AreEqual(res.executionEngineResult, ExecutionEngineResult.COMPLETED);
        }

        [TestCategory("LogicalOperators")]
        [TestMethod]
        public void OrIsTrue()
        {
            op = (rc + wc) || (rc + wc);

            var res = st.ExecuteWorkflow(op, p);

            Assert.IsTrue(p.DistanceTravelled == 3 && res.resultMatch, "'Or' operator (||) is true.");
            Assert.AreEqual(res.executionEngineResult, ExecutionEngineResult.COMPLETED);
        }

        [TestCategory("LogicalOperators")]
        [TestMethod]
        public void OrIsFalse()
        {
            op = !(rc + wc) || !(rc + wc);

            var res = st.ExecuteWorkflow(op, p);

            Assert.IsTrue(p.DistanceTravelled == 6 && !res.resultMatch, "'Or' operator (||) is false.");
            Assert.AreEqual(res.executionEngineResult, ExecutionEngineResult.FAILED);
        }

        [TestCategory("LogicalOperators")]
        [TestMethod]
        public void IfThenElseTrue()
        {
            op = (true) % ((rc + wc) / fc);

            var res = st.ExecuteWorkflow(op, p);

            Assert.IsTrue(p.DistanceTravelled == 3, "Incorrect distance travelled.");
            Assert.IsTrue(res.resultMatch, "'IfThenElse' operator %(x)+(y-z) is false.");
            Assert.AreEqual(res.executionEngineResult, ExecutionEngineResult.COMPLETED);
        }

        [TestCategory("LogicalOperators")]
        [TestMethod]
        public void IfThenElseFalse()
        {
            op = (false) % ((rc + wc) / (rc + wc));

            var res = st.ExecuteWorkflow(op, p);

            Assert.IsTrue(p.DistanceTravelled == 3, "Incorrect distance travelled.");
            Assert.IsFalse(res.resultMatch, "'IfThenElse' operator %(x)+(y-z) is true.");
            Assert.AreEqual(res.executionEngineResult, ExecutionEngineResult.FAILED);
        }

        [TestCategory("LogicalOperators")]
        [TestMethod]
        public void IfThenTrue()
        {
            op = (true) % ((rc + wc));

            var res = st.ExecuteWorkflow(op, p);

            Assert.IsTrue(p.DistanceTravelled == 3, "Incorrect distance travelled.");
            Assert.IsTrue(res.resultMatch, "'IfThen' operator %(x)+(y) is false.");
            Assert.AreEqual(res.executionEngineResult, ExecutionEngineResult.COMPLETED);
        }

        [TestCategory("LogicalOperators")]
        [TestMethod]
        public void IfThenFalse()
        {
            op = (false) % ((rc + wc));

            var res = st.ExecuteWorkflow(op, p);

            Assert.IsTrue(p.DistanceTravelled == 0, "Incorrect distance travelled.");
            Assert.IsTrue(!res.resultMatch, "'IfThen' operator (x)%(y) is true.");
            Assert.AreEqual(res.executionEngineResult, ExecutionEngineResult.FAILED);
        }

        [TestCategory("LogicalOperators")]
        [TestMethod]
        public void LoopIsTrue()
        {
            op = (dc < 10) * ((rc + wc));

            var res = st.ExecuteWorkflow(op, p);

            Assert.IsTrue(p.DistanceTravelled == 12, "Incorrect distance travelled.");
            Assert.IsTrue(res.resultMatch, "'Loop' operator %(x)*(y) is false.");
            Assert.AreEqual(res.executionEngineResult, ExecutionEngineResult.COMPLETED);
        }
    }
}
