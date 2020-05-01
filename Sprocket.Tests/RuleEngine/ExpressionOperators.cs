using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions;
using RaraAvis.Sprocket.Tests.Fakes.System;
using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;
using System;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{

    public class ExpressionOperators : IDisposable
    {
        private static Person p = null;
        private static GetDistanceCommand dc = null;
        private static Operator<Person> op = null;
        private static SerializeTest st = null;


        public ExpressionOperators()
        {
            dc = new GetDistanceCommand();
            st = new SerializeTest();
            p = new Person();
            st.BeginSerialize();
        }

        public void Dispose()
        {
            st.EndSerialize();
        }

        [Trait("RuleEngine", "ArithmeticOperators")]
        [Fact]
        public void NumericGreaterThanTrue()
        {
            p.DistanceTravelled = 10;

            op = (dc > 0);

            var res = st.Match(op, p);

            Assert.True(res, "'>' is false");
        }

        [Trait("RuleEngine", "ArithmeticOperators")]
        [Fact]
        public void NumericGreaterThanFalse()
        {
            p.DistanceTravelled = 10;

            op = (dc > 10);
            var res = st.Match(op, p);

            Assert.False(res, "'>' is true");
        }

        [Trait("RuleEngine", "ArithmeticOperators")]
        [Fact]
        public void NumericGreaterThanOrEqualsTrue()
        {
            p.DistanceTravelled = 10;

            op = (dc >= 10);
            var res = st.Match(op, p);

            Assert.True(res, "'>=' is false");
        }

        [Trait("RuleEngine", "ArithmeticOperators")]
        [Fact]
        public void NumericGreaterThanOrEqualsFalse()
        {
            p.DistanceTravelled = 10;

            op = (dc > 11);
            var res = st.Match(op, p);

            Assert.False(res, "'>=' is true");
        }

        [Trait("RuleEngine", "ArithmeticOperators")]
        [Fact]
        public void NumericLessThanTrue()
        {
            p.DistanceTravelled = 10;

            op = (dc < 20);
            var res = st.Match(op, p);

            Assert.True(res, "'<' is false");
        }

        [Trait("RuleEngine", "ArithmeticOperators")]
        [Fact]
        public void NumericLessThanFalse()
        {
            p.DistanceTravelled = 10;

            op = (dc < 10);
            var res = st.Match(op, p);

            Assert.False(res, "'>' is true");
        }

        [Trait("RuleEngine", "ArithmeticOperators")]
        [Fact]
        public void NumericLessThanOrEqualsTrue()
        {
            p.DistanceTravelled = 10;

            op = (dc <= 10);
            var res = st.Match(op, p);

            Assert.True(res, "'<=' is false");
        }

        [Trait("RuleEngine", "ArithmeticOperators")]
        [Fact]
        public void NumericLessThanOrEqualsFalse()
        {
            p.DistanceTravelled = 10;

            op = (dc <= 9);
            var res = st.Match(op, p);

            Assert.False(res, "'<=' is true");
        }

        [Trait("RulEngine", "Wrapps")]
        [Fact]
        public void WrapBoolCommandTrue()
        {
            Person son = new Person();
            p.Family.Add(son);
            son.Id = Guid.NewGuid();
            IsFamilyCommand isFamily = new IsFamilyCommand();
            isFamily.Person = son;

            op = (isFamily);
            var res = st.Match(op, p);

            Assert.True(res, "'WrapBool' is false");
        }

        [Trait("RuleEngine", "Wrapps")]
        [Fact]
        public void WrapBoolCommandFalse()
        {
            Person son = new Person();
            son.Id = Guid.NewGuid();
            p.Id = Guid.NewGuid();
            p.Family.Add(son);
            IsFamilyCommand isFamily = new IsFamilyCommand();
            isFamily.Person = p;

            op = (isFamily);
            var res = st.Match(op, p);

            Assert.False(res, "'WrapBool' is false");
        }

        [Trait("RuleEngine", "Functions")]
        [Fact]
        public void Function_Equals_True()
        {
            Person p = new Person();
            Person son1 = new Person();
            Person son2 = new Person();
            son2.Name = "Son2Name";
            p.Family.Add(son1);
            p.Family.Add(son2);

            SonsFunction sc = new SonsFunction();
            GetNameFunction gn = new GetNameFunction(p);

            op = (gn - (sc - 1) == son2.Name);
            var res = st.Match(op, p);

            Assert.True(res, "'Nested Call' is true");
        }

        [Trait("RuleEngine", "Functions")]
        [Fact]
        public void Function_NotEquals_True()
        {
            Person p = new Person();
            Person son1 = new Person();
            Person son2 = new Person();
            son2.Name = "Son2";
            son2.Name = "Son2Name";
            p.Family.Add(son1);
            p.Family.Add(son2);

            SonsFunction sc = new SonsFunction();
            GetNameFunction gn = new GetNameFunction(p);

            op = (gn - (sc - 1) != "Get:");
            var res = st.Match(op, p);

            Assert.True(res, "'Nested Call' is true");
        }

        [Trait("RuleEngine", "ArithmeticOperators")]
        [Fact]
        public void NumericEqualsTrue()
        {
            p.DistanceTravelled = 10;

            op = (dc == 10);

            var res = st.Match(op, p);

            Assert.True(res);
        }

        [Trait("RuleEngine", "ArithmeticOperators")]
        [Fact]
        public void NumericEqualsFalse()
        {
            p.DistanceTravelled = 10;

            op = (dc == 1);

            var res = st.Match(op, p);

            Assert.False(res);
        }

        [Trait("RuleEngine", "ArithmeticOperators")]
        [Fact]
        public void NumericNotEqualsTrue()
        {
            p.DistanceTravelled = 10;

            op = (dc != 5);

            var res = st.Match(op, p);

            Assert.True(res);
        }

        [Trait("RuleEngine", "ArithmeticOperators")]
        [Fact]
        public void NumericNotEqualsFalse()
        {
            p.DistanceTravelled = 10;

            op = (dc != 10);

            var res = st.Match(op, p);

            Assert.False(res);
        }
    }
}