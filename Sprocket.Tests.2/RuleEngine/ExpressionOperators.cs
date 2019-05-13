using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Parts.Elements.Functions.Kernel;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;
using System;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    [TestClass]
    public class ExpressionOperators
    {
        private static Person p = null;
        private static RuleElement<Person> re = null;
        private static DistanceCommand dc = null;
        private static Operator<Person> op = null;
        private static SerializeTest st = null;

        [ClassInitialize]
        public static void Init(TestContext tc)
        {
            dc = new DistanceCommand();
            st = new SerializeTest();
        }

        [TestInitialize]
        public void InitTest()
        {
            p = new Person();
            st.BeginSerialize();
        }

        [TestCleanup]
        public void EndTest()
        {
            st.EndSerialize();
        }

        [TestCategory("ArithmeticOperators")]
        [TestMethod]
        public void NumericGreaterThanIsTrue()
        {
            p.DistanceTravelled = 10;

            op = (dc > 0);

            var res = st.Match(op, p);

            Assert.IsTrue(res, "'>' is false");
        }

        [TestCategory("ArithmeticOperators")]
        [TestMethod]
        public void NumericGreaterThanIsFalse()
        {
            p.DistanceTravelled = 10;

            op = (dc > 10);
            var res = st.Match(op, p);

            Assert.IsFalse(res, "'>' is true");
        }

        [TestCategory("ArithmeticOperators")]
        [TestMethod]
        public void NumericGreaterThanOrEqualsIsTrue()
        {
            p.DistanceTravelled = 10;

            op = (dc >= 10);
            var res = st.Match(op, p);

            Assert.IsTrue(res, "'>=' is false");
        }

        [TestCategory("ArithmeticOperators")]
        [TestMethod]
        public void NumericGreaterThanOrEqualsIsFalse()
        {
            p.DistanceTravelled = 10;

            op = (dc > 11);
            var res = st.Match(op, p);

            Assert.IsFalse(res, "'>=' is true");
        }

        [TestCategory("ArithmeticOperators")]
        [TestMethod]
        public void NumericLessThanIsTrue()
        {
            p.DistanceTravelled = 10;

            op = (dc < 20);
            var res = st.Match(op, p);

            Assert.IsTrue(res, "'<' is false");
        }

        [TestCategory("ArithmeticOperators")]
        [TestMethod]
        public void NumericLessThanIsFalse()
        {
            p.DistanceTravelled = 10;

            op = (dc < 10);
            var res = st.Match(op, p);

            Assert.IsFalse(res, "'>' is true");
        }

        [TestCategory("ArithmeticOperators")]
        [TestMethod]
        public void NumericLessThanOrEqualsIsTrue()
        {
            p.DistanceTravelled = 10;

            op = (dc <= 10);
            var res = st.Match(op, p);

            Assert.IsTrue(res, "'<=' is false");
        }

        [TestCategory("ArithmeticOperators")]
        [TestMethod]
        public void NumericLessThanOrEqualsIsFalse()
        {
            p.DistanceTravelled = 10;

            op = (dc <= 9);
            var res = st.Match(op, p);

            Assert.IsFalse(res, "'<=' is true");
        }

        [TestCategory("Wrapps")]
        [TestMethod]
        public void WrapBoolCommandIsTrue()
        {
            Person son = new Person();
            p.Family.Add(son);
            son.Id = Guid.NewGuid();
            IsFamilyCommand isFamily = new IsFamilyCommand();
            isFamily.Person = son;

            op = (isFamily);
            var res = st.Match(op, p);

            Assert.IsTrue(res, "'WrapBool' is false");
        }

        [TestCategory("Wrapps")]
        [TestMethod]
        public void WrapBoolCommandIsFalse()
        {
            Person son = new Person();
            son.Id = Guid.NewGuid();
            p.Id = Guid.NewGuid();
            p.Family.Add(son);
            IsFamilyCommand isFamily = new IsFamilyCommand();
            isFamily.Person = p;

            op = (isFamily);
            var res = st.Match(op, p);

            Assert.IsFalse(res, "'WrapBool' is false");
        }

        [TestCategory("Casts")]
        [TestMethod]
        public void CastBoolCommandAsBooleanOperateIsTrue()
        {
            EatCommand ec = new EatCommand();

            op = (ec) & (ec);
            var res = st.Match(op, p);

            Assert.IsTrue(res, "'Cast bool command' is false");
        }

        [TestCategory("Casts")]
        [TestMethod]
        public void CastBoolCommandAsBooleanOperateIsFalse()
        {
            EatCommand ec = new EatCommand();

            var op = (ec) & !(ec);

            var res = st.Match(op, p);

            Assert.IsFalse(res, "'Cast bool command' is true");
        }

        [TestCategory("Functions")]
        [TestMethod]
        public void NestedCallFunction()
        {
            Person son1 = new Person();
            Person son2 = new Person();
            son2.Name = "Son2Name";
            p.Family.Add(son1);
            p.Family.Add(son2);

            SonsFunction sc = new SonsFunction();
            GetNameFunction gn = new GetNameFunction();

            op = (gn - (sc - 1) == "Get:" + son2.Name);
            var res = st.Match(op, p);

            Assert.IsTrue(res, "'Nested Call' is true");
        }
    }
}