using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Elements.Casts;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.ComparisonOperators;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions;
using RaraAvis.Sprocket.Tests.Fakes.System;
using System;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class ComparisonOperators
    {
        private WorflowEngineTest st = null;

        public ComparisonOperators()
        {
            st = new WorflowEngineTest();
        }

        public void Dispose()
        {
            st = null;
        }

        [Trait("ComparisonOperators", "GreaterThan")]
        [Fact]
        public void GreaterThan_Command_Int_True()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc > 0);

            var res = st.Match(op, p);

            Assert.IsType<GreaterThan<Person, IComparable>>(op);
            Assert.True(res);
        }

        [Trait("ComparisonOperators", "GreaterThan")]
        [Fact]
        public void GreaterThan_Command_Int_False()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc > 10);

            var res = st.Match(op, p);

            Assert.IsType<GreaterThan<Person, IComparable>>(op);
            Assert.False(res);
        }

        [Trait("ComparisonOperators", "GreaterThanOrEquals")]
        [Fact]
        public void GreaterThanOrEquals_Command_Int_True()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc >= 10);

            var res = st.Match(op, p);

            Assert.IsType<GreaterThanOrEquals<Person, IComparable>>(op);
            Assert.True(res);
        }

        [Trait("ComparisonOperators", "GreaterThanOrEquals")]
        [Fact]
        public void GreaterThanOrEquals_Command_Int_False()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc >= 11);

            var res = st.Match(op, p);

            Assert.IsType<GreaterThanOrEquals<Person, IComparable>>(op);
            Assert.False(res);
        }

        [Trait("ComparisonOperators", "LessThan")]
        [Fact]
        public void LessThan_Command_Int_True()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc < 20);

            var res = st.Match(op, p);

            Assert.IsType<LessThan<Person, IComparable>>(op);
            Assert.True(res);
        }

        [Trait("ComparisonOperators", "LessThan")]
        [Fact]
        public void LessThan_Command_Int_False()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc < 10);

            var res = st.Match(op, p);

            Assert.IsType<LessThan<Person, IComparable>>(op);
            Assert.False(res);
        }

        [Trait("ComparisonOperators", "LessThanOrEquals")]
        [Fact]
        public void LessThanOrEquals_Command_Int_True()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc <= 10);

            var res = st.Match(op, p);

            Assert.IsType<LessThanOrEquals<Person, IComparable>>(op);
            Assert.True(res);
        }

        [Trait("ComparisonOperators", "LessThanOrEquals")]
        [Fact]
        public void LessThanOrEquals_Command_Int_False()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc <= 9);

            var res = st.Match(op, p);

            Assert.IsType<LessThanOrEquals<Person, IComparable>>(op);
            Assert.False(res);
        }

        [Trait("ComparisonOperators", "Equals")]
        [Fact]
        public void Equals_Command_Int_True()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc == 10);

            var res = st.Match(op, p);

            Assert.IsType<Equals<Person, int>>(op);
            Assert.True(res);
        }

        [Trait("ComparisonOperators", "Equals")]
        [Fact]
        public void Equals_Command_Int_False()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc == 1);

            var res = st.Match(op, p);

            Assert.IsType<Equals<Person, int>>(op);
            Assert.False(res);
        }

        [Trait("ComparisonOperators", "NotEquals")]
        [Fact]
        public void NotEquals_Command_Int_True()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc != 5);

            var res = st.Match(op, p);

            Assert.IsType<NotEquals<Person, int>>(op);
            Assert.True(res);
        }

        [Trait("ComparisonOperators", "NotEquals")]
        [Fact]
        public void NotEquals_Command_Int_False()
        {
            var p = new Person() { DistanceTravelled = 10 };
            var dc = new GetDistanceCommand();
            var op = (dc != 10);

            var res = st.Match(op, p);

            Assert.IsType<NotEquals<Person, int>>(op);
            Assert.False(res);
        }

        [Trait("ComparisonOperators", "NotEquals")]
        [Fact]
        public void NotEquals_Command_Value_True()
        {
            var p = new Person();
            var sc = new SleepCommand();
            var op = (sc != Status.WakeUp);

            var res = st.Match(op, p);

            Assert.IsType<NotEquals<Person, Status>>(op);
            Assert.Equal(Status.Sleep, p.Status);
            Assert.True(res);
        }

        [Trait("ComparisonOperators", "NotEquals")]
        [Fact]
        public void NotEquals_Command_Value_False()
        {
            var p = new Person();
            var sc = new SleepCommand();
            var op = (sc != Status.Sleep);

            var res = st.Match(op, p);

            Assert.IsType<NotEquals<Person, Status>>(op);
            Assert.Equal(Status.Sleep, p.Status);
            Assert.False(res);
        }

        [Trait("ComparisonOperators", "Equals")]
        [Fact]
        public void Equals_Command_Value_True()
        {
            var p = new Person();
            var sc = new SleepCommand();
            var op = (sc == Status.Sleep);

            var res = st.Match(op, p);

            Assert.IsType<Equals<Person, Status>>(op);
            Assert.Equal(Status.Sleep, p.Status);
            Assert.True(res);
        }

        [Trait("ComparisonOperators", "Equals")]
        [Fact]
        public void Equals_Command_Value_False()
        {
            var p = new Person();
            var sc = new SleepCommand();
            var op = (sc == Status.WakeUp);

            var res = st.Match(op, p);

            Assert.IsType<Equals<Person, Status>>(op);
            Assert.Equal(Status.Sleep, p.Status);
            Assert.False(res);
        }

        [Trait("ComparisonOperators", "Equals")]
        [Fact]
        public void Equals_Function_Int_True()
        {
            var p = new Person() { DistanceTravelled = 20 };
            var drc = new DistanceRemainingFunction(30);
            Operator<Person> op = (drc) == 10;

            var res = st.Match(op, p);

            Assert.IsType<Equals<Person, int>>(op);
            Assert.True(res);
        }

        [Trait("ComparisonOperators", "Equals")]
        [Fact]
        public void Equals_Function_Int_False()
        {
            var p = new Person() { DistanceTravelled = 20 };
            var drc = new DistanceRemainingFunction(30);
            Operator<Person> op = (drc) == 0;

            var res = st.Match(op, p);

            Assert.IsType<Equals<Person, int>>(op);
            Assert.False(res);
        }

        [Trait("ComparisonOperators", "Equals")]
        [Fact]
        public void NotEquals_Function_Int_True()
        {
            var p = new Person() { DistanceTravelled = 20 };
            var drc = new DistanceRemainingFunction(30);
            Operator<Person> op = (drc) != 0;

            var res = st.Match(op, p);

            Assert.IsType<NotEquals<Person, int>>(op);
            Assert.True(res);
        }

        [Trait("ComparisonOperators", "Equals")]
        [Fact]
        public void NotEquals_Function_Int_False()
        {
            var p = new Person() { DistanceTravelled = 20 };
            var drc = new DistanceRemainingFunction(30);
            var op = (drc) != 10;

            var res = st.Match(op, p);

            Assert.IsType<NotEquals<Person, int>>(op);
            Assert.False(res);
        }
    }
}
