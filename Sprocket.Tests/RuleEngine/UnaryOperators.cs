using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.UnaryOperators;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions;
using RaraAvis.Sprocket.Tests.Fakes.System;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class UnaryOperators : IDisposable
    {
        private WorflowEngineTest st = null;
        private Operator<Person> op = null;

        public UnaryOperators()
        {
            st = new WorflowEngineTest();
        }

        public void Dispose()
        {
            st = null;
        }

        [Trait("UnaryOperators", "Not")]
        [Fact]
        public void Not_Command()
        {
            var p = new Person();
            var rc = new RightCommand();
            op = !rc;

            var res = st.Match(op, p);

            Assert.IsType<Not<Person>>(op);
            Assert.False(res);
            Assert.True(p.Correct);
        }

        [Trait("UnaryOperators", "True")]
        [Fact]
        public void True_Command()
        {
            var p = new Person();
            var rc = new RightCommand();
            op = +rc;

            var res = st.Match(op, p);

            Assert.IsType<True<Person>>(op);
            Assert.True(res);
            Assert.True(p.Correct);
        }

        [Trait("UnaryOperators", "False")]
        [Fact]
        public void False_Command()
        {
            var p = new Person();
            var rc = new RightCommand();
            op = -rc;

            var res = st.Match(op, p);

            Assert.IsType<False<Person>>(op);
            Assert.False(res);
            Assert.True(p.Correct);
        }

        [Trait("UnaryOperators", "True")]
        [Fact]
        public void True_Operator()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            Operator<Person> op = +(dc < 10);

            var res = st.Match(op, p);

            Assert.IsType<True<Person>>(op);
            Assert.True(res);
        }

        [Trait("UnaryOperators", "False")]
        [Fact]
        public void False_Operator()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            op = -(dc < 10);

            var res = st.Match(op, p);

            Assert.IsType<False<Person>>(op);
            Assert.False(res);
        }

        [Trait("UnaryOperators", "Not")]
        [Fact]
        public void Not_Operator_True()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            op = !(dc > 10);

            var res = st.Match(op, p);

            Assert.IsType<Not<Person>>(op);
            Assert.True(res);
        }

        [Trait("UnaryOperators", "Not")]
        [Fact]
        public void Not_Operator_False()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            op = !(dc < 10);

            var res = st.Match(op, p);

            Assert.IsType<Not<Person>>(op);
            Assert.False(res);
        }

        [Trait("UnaryOperators", "Not")]
        [Fact]
        public void Not_Function_False()
        {
            var p = new Person();
            var snf = new SetNameFunction();
            op = !snf;

            var res = st.Match(op, p);

            Assert.IsType<Not<Person>>(op);
            Assert.False(res);
        }
    }
}
