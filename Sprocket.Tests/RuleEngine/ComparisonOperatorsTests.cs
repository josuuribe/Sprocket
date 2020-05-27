using RaraAvis.Sprocket.RuleEngine;
using RaraAvis.Sprocket.RuleEngine.Operators.ComparisonOperators;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions;
using RaraAvis.Sprocket.Tests.Fakes.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.SyworflowEngineTestem;
using System;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class ComparisonOperatorsTests : IClassFixture<WorkflowEngineTest>
    {
        private readonly WorkflowEngineTest workflowEngineTest = null;

        public ComparisonOperatorsTests(WorkflowEngineTest worflowEngineTest)
        {
            this.workflowEngineTest = worflowEngineTest;
        }

        [Trait("ComparisonOperators", "GreaterThan")]
        [Fact]
        public void GreaterThan_Operand_Int_True()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc > 0);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<GreaterThan<Person, IComparable>>(op);
            Assert.True(res);
        }

        [Trait("ComparisonOperators", "GreaterThan")]
        [Fact]
        public void GreaterThan_Operand_Int_False()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc > 10);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<GreaterThan<Person, IComparable>>(op);
            Assert.False(res);
        }

        [Trait("ComparisonOperators", "GreaterThanOrEquals")]
        [Fact]
        public void GreaterThanOrEquals_Operand_Int_True()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc >= 10);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<GreaterThanOrEquals<Person, IComparable>>(op);
            Assert.True(res);
        }

        [Trait("ComparisonOperators", "GreaterThanOrEquals")]
        [Fact]
        public void GreaterThanOrEquals_Operand_Int_False()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc >= 11);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<GreaterThanOrEquals<Person, IComparable>>(op);
            Assert.False(res);
        }

        [Trait("ComparisonOperators", "Lessthan")]
        [Fact]
        public void Lessthan_Operand_Int_True()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc < 20);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<LessThan<Person, IComparable>>(op);
            Assert.True(res);
        }

        [Trait("ComparisonOperators", "Lessthan")]
        [Fact]
        public void LessThan_Operand_Int_False()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc < 10);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<LessThan<Person, IComparable>>(op);
            Assert.False(res);
        }

        [Trait("ComparisonOperators", "LessThanOrEquals")]
        [Fact]
        public void LessThanOrEquals_Operand_Int_True()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc <= 10);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<LessThanOrEquals<Person, IComparable>>(op);
            Assert.True(res);
        }

        [Trait("ComparisonOperators", "LessThanOrEquals")]
        [Fact]
        public void LessThanOrEquals_Operand_Int_False()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc <= 9);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<LessThanOrEquals<Person, IComparable>>(op);
            Assert.False(res);
        }

        [Trait("ComparisonOperators", "Equals")]
        [Fact]
        public void Equals_Operand_Int_True()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc == 10);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<Equals<Person, int>>(op);
            Assert.True(res);
        }

        [Trait("ComparisonOperators", "Equals")]
        [Fact]
        public void Equals_Operand_Int_False()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc == 1);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<Equals<Person, int>>(op);
            Assert.False(res);
        }

        [Trait("ComparisonOperators", "NotEquals")]
        [Fact]
        public void NotEquals_Operand_Int_True()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc != 5);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<NotEquals<Person, int>>(op);
            Assert.True(res);
        }

        [Trait("ComparisonOperators", "NotEquals")]
        [Fact]
        public void NotEquals_Operand_Int_False()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc != 10);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<NotEquals<Person, int>>(op);
            Assert.False(res);
        }

        [Trait("ComparisonOperators", "NotEquals")]
        [Fact]
        public void NotEquals_Operand_Value_True()
        {
            var p = new Person();
            var sc = new SleepCommand();
            var op = (sc != worflowEngineTestatus.WakeUp);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<NotEquals<Person, worflowEngineTestatus>>(op);
            Assert.Equal(worflowEngineTestatus.Sleep, p.worflowEngineTestatus);
            Assert.True(res);
        }

        [Trait("ComparisonOperators", "NotEquals")]
        [Fact]
        public void NotEquals_Operand_Value_False()
        {
            var p = new Person();
            var sc = new SleepCommand();
            var op = (sc != worflowEngineTestatus.Sleep);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<NotEquals<Person, worflowEngineTestatus>>(op);
            Assert.Equal(worflowEngineTestatus.Sleep, p.worflowEngineTestatus);
            Assert.False(res);
        }

        [Trait("ComparisonOperators", "Equals")]
        [Fact]
        public void Equals_Operand_Value_True()
        {
            var p = new Person();
            var sc = new SleepCommand();
            var op = (sc == worflowEngineTestatus.Sleep);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<Equals<Person, worflowEngineTestatus>>(op);
            Assert.Equal(worflowEngineTestatus.Sleep, p.worflowEngineTestatus);
            Assert.True(res);
        }

        [Trait("ComparisonOperators", "Equals")]
        [Fact]
        public void Equals_Operand_Value_False()
        {
            var p = new Person();
            var sc = new SleepCommand();
            var op = (sc == worflowEngineTestatus.WakeUp);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<Equals<Person, worflowEngineTestatus>>(op);
            Assert.Equal(worflowEngineTestatus.Sleep, p.worflowEngineTestatus);
            Assert.False(res);
        }

        [Trait("ComparisonOperators", "Equals")]
        [Fact]
        public void Equals_Function_Int_True()
        {
            var p = new Person() { DistanceTravelled = 20 };
            var drc = new DiworflowEngineTestanceRemainingFunction(30);
            Operator<Person> op = (drc) == 10;

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<Equals<Person, int>>(op);
            Assert.True(res);
        }

        [Trait("ComparisonOperators", "Equals")]
        [Fact]
        public void Equals_Function_Int_False()
        {
            var p = new Person() { DistanceTravelled = 20 };
            var drc = new DiworflowEngineTestanceRemainingFunction(30);
            Operator<Person> op = (drc) == 0;

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<Equals<Person, int>>(op);
            Assert.False(res);
        }

        [Trait("ComparisonOperators", "NotEquals")]
        [Fact]
        public void NotEquals_Function_Int_True()
        {
            var p = new Person() { DistanceTravelled = 20 };
            var drc = new DiworflowEngineTestanceRemainingFunction(30);
            Operator<Person> op = (drc) != 0;

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<NotEquals<Person, int>>(op);
            Assert.True(res);
        }

        [Trait("ComparisonOperators", "NotEquals")]
        [Fact]
        public void NotEquals_Function_Int_False()
        {
            var p = new Person() { DistanceTravelled = 20 };
            var drc = new DiworflowEngineTestanceRemainingFunction(30);
            var op = (drc) != 10;

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<NotEquals<Person, int>>(op);
            Assert.False(res);
        }
    }
}
