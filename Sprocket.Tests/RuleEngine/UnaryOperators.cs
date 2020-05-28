using RaraAvis.Sprocket.RuleEngine;
using RaraAvis.Sprocket.RuleEngine.Operators.UnaryOperators;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions;
using RaraAvis.Sprocket.Tests.Fakes.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.SyworflowEngineTestem;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class UnaryOperators : IClassFixture<WorkflowEngineTest>
    {
        private readonly WorkflowEngineTest workflowEngineTest = null;

        public UnaryOperators(WorkflowEngineTest worflowEngineTest)
        {
            this.workflowEngineTest = worflowEngineTest;
        }

        [Trait("UnaryOperators", "True")]
        [Fact]
        public void Operand_Operator()
        {
            var p = new Person();
            var wc = new WalkCommand();
            Operator<Person> op = +(wc);

            var res = op.Process(p);

            Assert.IsType<True<Person>>(op);
            Assert.True(res);
            Assert.Equal(1, p.DistanceTravelled);
        }

        [Trait("UnaryOperators", "Not")]
        [Fact]
        public void Not_Command()
        {
            var p = new Person();
            Operator<Person> rc = new RightCommand();
            var op = !rc;

            var res = workflowEngineTest.Match(op, p);

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
            var op = +rc;

            var res = workflowEngineTest.Match(op, p);

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
            var op = -rc;

            var res = workflowEngineTest.Match(op, p);

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

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<True<Person>>(op);
            Assert.True(res);
        }

        [Trait("UnaryOperators", "False")]
        [Fact]
        public void False_Operator()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var op = -(dc < 10);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<False<Person>>(op);
            Assert.False(res);
        }

        [Trait("UnaryOperators", "Not")]
        [Fact]
        public void Not_Operator_True()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var op = !(dc > 10);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<Not<Person>>(op);
            Assert.True(res);
        }

        [Trait("UnaryOperators", "Not")]
        [Fact]
        public void Not_Operator_False()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var op = !(dc < 10);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<Not<Person>>(op);
            Assert.False(res);
        }

        [Trait("UnaryOperators", "Not")]
        [Fact]
        public void Not_Function_False()
        {
            var p = new Person();
            Operator<Person> snf = new SetNameFunction("new name");
            var op = !snf;

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<Not<Person>>(op);
            Assert.Equal("new name", p.Name);
            Assert.False(res);
        }
    }
}
