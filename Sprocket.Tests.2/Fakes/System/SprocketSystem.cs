using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaraAvis.Sprocket.Parts.Elements.Functions.Kernel;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Workflows;

namespace RaraAvis.Sprocket.Tests.Fakes.System
{
    [TestClass]
    public class SprocketSystem
    {
        private static ActivateRuleEngine are = null;
        private static RunCommand rc = null;
        private static WalkCommand wc = null;
        private static Person p = null;

        [ClassInitialize]
        public static void Init(TestContext tc)
        {
            are = new ActivateRuleEngine();
            rc = new RunCommand();
            wc = new WalkCommand();
            p = new Person();
        }

        [TestMethod]
        public void PreWorkflow()
        {
            RuleElement<Person> re = new RuleElement<Person>();
            re.UserStatus = 99;
            re.Element = new Person();


            //var d = ~(b);
            //are.CreateStagePreprocess(0, b);
            //are.Init(p);

            //Assert.IsTrue(String.Compare(fakeElement.Name, cn.Name) == 0);
        }
    }
}
