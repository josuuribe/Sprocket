using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.System;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class Batchs
    {
        private static RunCommand rc = null;
        private static WalkCommand wc = null;
        private static RuleElement<Person> re = null;
        private static DistanceCommand dc = null;
        private static SerializeTest st = null;
        private static Operator<Person> op = null;
        private static Person p = null;
        private static FalseCommand fc = null;

        public Batchs()
        {
            rc = new RunCommand();
            wc = new WalkCommand();
            dc = new DistanceCommand();
            re = new RuleElement<Person>();
            st = new SerializeTest();
            fc = new FalseCommand();

            p = new Person();
            st.BeginSerialize();
        }

        public void Dispose()
        {
            
        }

        [Trait("RuleEngine", "Batch")]
        [Fact]
        public void AppendCommandToBatch()
        {
            op = (rc + wc) + (rc);

            var res = st.Match(op, p);

            Assert.True(p.DistanceTravelled == 5 && res, "'Plus' operator (+) is not true.");
        }

        [Trait("RuleEngine", "Batch")]
        [Fact]
        public void AppendBatchToCommand()
        {
            op = (rc) + (rc + wc);

            var res = st.Match(op, p);

            Assert.True(p.DistanceTravelled == 5 && res, "'Plus' operator (+) is not true.");
        }
    }
}
