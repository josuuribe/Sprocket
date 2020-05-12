﻿using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Elements.Operates.Commands;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.System;
using System;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class Commands : IDisposable
    {
        private static WorflowEngineTest st = null;

        public Commands()
        {
            st = new WorflowEngineTest();
        }

        public void Dispose()
        {
            st = null;
        }

        [Trait("Commands", "FalseCommand")]
        [Fact]
        public void FalseCommand_False()
        {
            var p = new Person();
            var fc = new FalseCommand<Person>();

            var res = st.Match(fc, p);

            Assert.IsType<FalseCommand<Person>>(fc);
            Assert.False(res);
        }

        [Trait("Commands", "TrueCommand")]
        [Fact]
        public void TrueCommand_True()
        {
            var p = new Person();
            var op = new TrueCommand<Person>();

            var res = st.Match(op, p);

            Assert.IsType<TrueCommand<Person>>(op);
            Assert.True(res);
        }
    }
}
