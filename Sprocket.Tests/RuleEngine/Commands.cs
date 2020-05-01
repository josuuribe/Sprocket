using RaraAvis.Sprocket.Parts.Elements.Commands;
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
            TrueCommand<Person> tc = new TrueCommand<Person>();

            var ope = (tc >> 1);

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
            RightCommand rc = new RightCommand();
            var ope = (rc >> 1);

            var res = st.Match(ope, p);

            Assert.True(res);
            Assert.Equal(1, st.UserStatus);
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void RemoveFlag_Function()
        {
            RightCommand rc = new RightCommand();
            var ope = (rc << 1);

            var res = st.Match(ope, p);

            Assert.True(res);
            Assert.Equal(0, st.UserStatus);
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void CommandCast_String()
        {
            Person p = new Person() { Name = "Name" };
            GetNameCommand gnc = new GetNameCommand(p);
            
            string name = gnc;
            name = name.ToUpperInvariant();

            Assert.Equal(p.Name.ToUpperInvariant(), name);
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void CommandCast_SameElement()
        {
            Person p = new Person() { Name = "Name" };
            GetNameCommand gnc = new GetNameCommand(p);

            var element = gnc.Element;

            Assert.Equal(p, element);
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void CommandValue_SameCast()
        {
            Person p = new Person() { Name = "Name" };
            GetNameCommand gnc = new GetNameCommand(p);

            Assert.Equal(gnc, gnc.Value);
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void Command_Not()
        {
            RightCommand rc = new RightCommand();
            Person p = new Person();
            var op = !rc;

            var res = st.Match(op, p);

            Assert.False(res);
            Assert.True(p.Correct);
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void Command_True()
        {
            RightCommand rc = new RightCommand();
            Person p = new Person();
            var op = +rc;

            var res = st.Match(op, p);

            Assert.True(res);
            Assert.True(p.Correct);
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void Command_False()
        {
            RightCommand rc = new RightCommand();
            Person p = new Person();
            var op = -rc;

            var res = st.Match(op, p);

            Assert.False(res);
            Assert.True(p.Correct);
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void FalseCommand_False()
        {
            FalseCommand<Person> fc = new FalseCommand<Person>();
            Person p = new Person();

            var res = st.Match(fc, p);

            Assert.False(res);
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void TrueCommand_True()
        {
            TrueCommand<Person> fc = new TrueCommand<Person>();
            Person p = new Person();

            var res = st.Match(fc, p);

            Assert.True(res);
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void CastFalseCommand_False()
        {
            FalseCommand<Person> fc = new FalseCommand<Person>();
            Person p = new Person();

            var res = st.Match(fc, p);

            Assert.False(res);
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void CastTrueCommand_Bool()
        {
            Person p = new Person();
            TrueCommand<Person> fc = new TrueCommand<Person>(p);

            Assert.True(fc);
        }

        [Trait("RuleEngine", "System")]
        [Fact]
        public void CastFalseCommand_Bool()
        {
            Person p = new Person();
            FalseCommand<Person> fc = new FalseCommand<Person>(p);

            Assert.False(fc);
        }
    }
}
