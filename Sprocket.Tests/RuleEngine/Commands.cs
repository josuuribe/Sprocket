using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions;
using RaraAvis.Sprocket.Tests.Fakes.System;
using System;
using System.Collections.Generic;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class Commands : IDisposable
    {
        private static SerializeTest st = null;
        private static Person p = null;


        public Commands()
        {
            st = new SerializeTest();
            p = new Person();
            st.BeginSerialize();
        }

        public void Dispose()
        {
            st.EndSerialize();
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void Execute()
        {
            GetNameCommand gnc = new GetNameCommand();
            p.Name = "Name";

            var ope = (gnc);

            var res = st.Execute<string>(ope, p);

            Assert.Equal("Name", res);
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void NotEquals_Command_True()
        {
            SleepCommand sc = new SleepCommand();

            var ope = (sc != Status.WakeUp);

            var res = st.Match(ope, p);

            Assert.Equal(Status.Sleep, p.Status);
            Assert.True(res);
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void NotEquals_Command_False()
        {
            SleepCommand sc = new SleepCommand();

            var ope = (sc != Status.Sleep);

            var res = st.Match(ope, p);

            Assert.Equal(Status.Sleep, p.Status);
            Assert.False(res);
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void Equals_Command_True()
        {
            SleepCommand sc = new SleepCommand();

            var ope = (sc == Status.Sleep);

            var res = st.Match(ope, p);

            Assert.Equal(Status.Sleep, p.Status);
            Assert.True(res);
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void Equals_Command_False()
        {
            SleepCommand sc = new SleepCommand();

            var ope = (sc == Status.WakeUp);

            var res = st.Match(ope, p);

            Assert.Equal(Status.Sleep, p.Status);
            Assert.False(res);
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void AddFlagCommand()
        {
            IsFamilyCommand ifc = new IsFamilyCommand();

            var ope = (ifc >> 1);

            var res = st.Match(ope, p);

            Assert.True(res);
            Assert.Equal(1, st.UserStatus);
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void RemoveFlagCommand()
        {
            IsFamilyCommand ifc = new IsFamilyCommand();

            var ope = ((ifc >> 1) + (ifc << 1));

            var res = st.Match(ope, p);

            Assert.True(res);
            Assert.Equal(0, st.UserStatus);
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void AddFlag_Function()
        {
            SonsFunction sf = new SonsFunction();
            p.Family = new List<Person>();

            var ope = (sf >> 1);

            var res = st.Match(ope, p);

            Assert.True(res);
            Assert.Equal(1, st.UserStatus);
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void RemoveFlag_Function()
        {
            SonsFunction sf = new SonsFunction();
            p.Family = new List<Person>();

            var ope = (sf << 1);

            var res = st.Match(ope, p);

            Assert.True(res);
            Assert.Equal(0, st.UserStatus);
        }
    }
}
