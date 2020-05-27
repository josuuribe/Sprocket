using RaraAvis.Sprocket.RuleEngine;
using RaraAvis.Sprocket.RuleEngine.Operators.IterationOperators;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions;
using RaraAvis.Sprocket.Tests.Fakes.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.SyworflowEngineTestem;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Linq.Expressions;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class IterationOperators : IClassFixture<WorkflowEngineTest>
    {
        private readonly WorkflowEngineTest workflowEngineTest = null;

        public IterationOperators(WorkflowEngineTest worflowEngineTest)
        {
            this.workflowEngineTest = worflowEngineTest;
        }

        [Trait("IterationOperators", "Loop")]
        [Fact]
        public void Loop_Operator_Operand()
        {
            var p = new Person() { worflowEngineTestatus = worflowEngineTestatus.WakeUp };
            Operator<Person> isc = new HasworflowEngineTestatus(worflowEngineTestatus.WakeUp);
            var sc = new SleepCommand();
            var op = (isc) * (sc);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<Loop<Person, worflowEngineTestatus>>(op);
            Assert.Equal(worflowEngineTestatus.Sleep, p.worflowEngineTestatus);
            Assert.True(res);
        }

        [Trait("IterationOperators", "Loop")]
        [Fact]
        public void Loop_Operator_BoolOperand()
        {
            var p = new Person();
            var ac = new GetAgeCommand();
            var aaf = new AddAgeFunction(1);
            var op = (ac < 10) * (aaf);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<Loop<Person, bool>>(op);
            Assert.Equal(10, p.Age);
            Assert.True(res);
        }

        [Trait("IterationOperators", "Loop")]
        [Fact]
        public void Loop_Operator_BooleanOperand()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var rc = new RunCommand();
            var wc = new WalkCommand();
            var op = (dc < 10) * (rc / wc);

            var res = workflowEngineTest.Match(+op, p);

            Assert.IsType<Loop<Person, bool>>(op);
            Assert.Equal(12, p.DistanceTravelled);
            Assert.True(res);
        }

        [Trait("IterationOperators", "Loop")]
        [Fact]
        public void Loop_Operator_Function()
        {
            var p = new Person();
            var gac = new GetAgeCommand();
            var aaf = new AddAgeFunction(1);
            var op = (gac < 10) * (aaf);

            var res = workflowEngineTest.Match(+op, p);

            Assert.IsType<Loop<Person, bool>>(op);
            Assert.Equal(10, p.Age);
            Assert.True(res);
        }

        [Trait("IterationOperators", "Loop")]
        [Fact]
        public void Loop_BooleanOperand_Operand()
        {
            var p = new Person() { Age = 10 };
            var haf = new HasAgeFunction(10);
            var aaf = new AddAgeFunction(1);
            var op = (haf) * (aaf);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<Loop<Person, bool>>(op);
            Assert.Equal(11, p.Age);
            Assert.True(res);
        }

        [Trait("IterationOperators", "Loop")]
        [Fact]
        public void Loop_Pointer_Pointer()
        {
            var p = new Person() { Age = 10 };
            Expression<Func<Rule<Person>, bool>> getAge = (rule) => rule.Target.Age < 10;
            Operator<Person> opAge = getAge;
            AddAgeFunction addAgeFunction = new AddAgeFunction(5);
            var op = (opAge) * (addAgeFunction);

            var res = workflowEngineTest.Match(+op, p);

            Assert.IsType<Loop<Person, bool>>(op);
            Assert.Equal(10, p.Age);
            Assert.True(res);
        }
    }
}
