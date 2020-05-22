using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Elements.Casts;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.IterationOperators;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions;
using RaraAvis.Sprocket.Tests.Fakes.System;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Linq.Expressions;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class IterationOperators
    {
        private WorflowEngineTest st = null;

        public IterationOperators()
        {
            st = new WorflowEngineTest();
        }

        public void Dispose()
        {
            st = null;
        }

        [Trait("IterationOperators", "Loop")]
        [Fact]
        public void Loop_Operator_Operand()
        {
            var p = new Person() { Status = Status.WakeUp };
            Operator<Person> isc = new HasStatus(Status.WakeUp);
            var sc = new SleepCommand();
            var op = (isc) * (sc);

            var res = st.Match(op, p);

            Assert.IsType<Loop<Person, Status>>(op);
            Assert.Equal(Status.Sleep, p.Status);
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

            var res = st.Match(op, p);

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

            var res = st.Match(+op, p);

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

            var res = st.Match(+op, p);

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

            var res = st.Match(op, p);

            Assert.IsType<Loop<Person, bool>>(op);
            Assert.Equal(11, p.Age);
            Assert.True(res);
        }

        //[Trait("IterationOperators", "Loop")]
        //[Fact]
        //public void Loop_Bool_Function_True()
        //{
        //    var p = new Person();
        //    var aaf = new AddAgeFunction(1);
        //    var op = (false) * (aaf);

        //    var res = st.Match(op, p);

        //    Assert.IsType<Loop<Person>>(op);
        //    Assert.Equal(0, p.Age);
        //    Assert.True(res);
        //}

        [Trait("IterationOperators", "Loop")]
        [Fact]
        public void Loop_Pointer_Pointer()
        {
            var p = new Person() { Age = 10 };
            Expression<Func<Rule<Person>, bool>> getAge = (rule) => rule.Element.Age < 10;
            Operator<Person> opAge = getAge;
            AddAgeFunction addAgeFunction = new AddAgeFunction(5);
            var op = (opAge) * (addAgeFunction);

            var res = st.Match(+op, p);

            Assert.IsType<Loop<Person, bool>>(op);
            Assert.Equal(10, p.Age);
            Assert.True(res);
        }
    }
}
