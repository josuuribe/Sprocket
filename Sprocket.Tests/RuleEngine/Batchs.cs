using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Parts.Elements.Functions.Kernel;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions;
using RaraAvis.Sprocket.Tests.Fakes.System;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Collections.Generic;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class Batchs : IDisposable
    {
        private static RunCommand rc = null;
        private static WalkCommand wc = null;
        private static RuleElement<Person> re = null;
        private static GetDistanceCommand dc = null;
        private static SerializeTest st = null;
        private static Operator<Person> op = null;
        private static Person p = null;

        public Batchs()
        {
            rc = new RunCommand();
            wc = new WalkCommand();
            dc = new GetDistanceCommand();
            re = new RuleElement<Person>();
            st = new SerializeTest();

            p = new Person();
            st.BeginSerialize();
        }

        public void Dispose()
        {
            st.EndSerialize();
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
            RunCommand rc = new RunCommand();
            WalkCommand wc = new WalkCommand();

            Operator<Person> op = (rc) + (rc + wc);

            var res = st.Match(op, p);

            Assert.True(p.DistanceTravelled == 5);
            Assert.True(res);
        }

        [Trait("RuleEngine", "Batch")]
        [Fact]
        public void Batch_Function_Function_True()
        {
            SetNameFunction sn = new SetNameFunction();
            sn.Parameters = "name";
            SetSurnameFunction ss = new SetSurnameFunction();
            ss.Parameters = "surname";

            op = sn + ss;

            var res = st.Match(op, p);

            Assert.Equal("name", p.Name);
            Assert.Equal("surname", p.Surname);
            Assert.True(res);
        }

        [Trait("RuleEngine", "Batch")]
        [Fact]
        public void Batch_Batch_Function_True()
        {
            AddAgeFunction ag1 = new AddAgeFunction();
            ag1.Parameters = 1;
            AddAgeFunction ag2 = new AddAgeFunction();
            ag2.Parameters = 2;
            AddAgeFunction ag3 = new AddAgeFunction();
            ag3.Parameters = 3;

            Batch<Person> b1 = ag1 + ag2;
            var op3 = b1 + ag3;

            var res = st.Match(op3, p);

            Assert.True(res);
            Assert.Equal(6, p.Age);
        }

        [Trait("RuleEngine", "Batch")]
        [Fact]
        public void Batch_Function_Batch_True()
        {
            AddAgeFunction ag1 = new AddAgeFunction();
            ag1.Parameters = 1;
            AddAgeFunction ag2 = new AddAgeFunction();
            ag2.Parameters = 2;
            AddAgeFunction ag3 = new AddAgeFunction();
            ag3.Parameters = 3;

            Batch<Person> b1 = ag1 + ag2;
            var op3 = ag3 + b1;

            var res = st.Match(op3, p);

            Assert.True(res);
            Assert.Equal(6, p.Age);
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void Batch_Throws_Exception_False()
        {
            SonsFunction sf1 = new SonsFunction();
            sf1.Parameters = -1;
            SonsFunction sf2 = new SonsFunction();
            sf2.Parameters = -1;
            p.Family = new List<Person>()
            {
                new Person()
                {
                     Name="Test"
                }
            };

            var ope = (sf1 + sf2);

            var res = st.Match(ope, p);

            Assert.False(res);
        }
    }
}
