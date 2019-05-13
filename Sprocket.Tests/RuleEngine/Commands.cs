using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.System;
using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class Commands
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

        
        public Commands()
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



        [Trait("RuleEngine", "Command")]
        [Fact]
        public void Execute()
        {
            GetNameCommand gnc = new GetNameCommand();
            p.Name = "Name";

            var ope = (gnc);

            var res = st.Execute<string>(ope, p);

            Assert.Equal("Name", res);
        }
    }
}
