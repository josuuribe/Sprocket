using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    [TestClass]
    public class Functions
    {
        private static Person p = null;
        private static RuleElement<Person> re = null;
        private static DistanceRemainingFunction drf = null;
        private static Operator<Person> op = null;
        private static SerializeTest st = null;

        [ClassInitialize]
        public static void Init(TestContext tc)
        {
            drf = new DistanceRemainingFunction();
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


        [TestCategory("Functions")]
        [TestMethod]
        public void GreatherThanTrue()
        {
            //op = a > 10;

            //op = (+(drf));
            //var res = st.ExecuteWorkflow(op, p);

            //Assert.IsTrue(res.resultMatch, "'>=' is false");
            //Assert.AreEqual(res.executionEngineResult, ExecutionEngineResult.COMPLETED);
        }
    }
}
