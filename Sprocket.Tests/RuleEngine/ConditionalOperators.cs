using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.ConditionalOperators;
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
    public class ConditionalOperators
    {
        private WorflowEngineTest st = null;
        private Operator<Person> op = null;

        public ConditionalOperators()
        {
            st = new WorflowEngineTest();
        }

        public void Dispose()
        {
            st = null;
        }

        [Trait("ConditionalOperators", "IfThenElse")]
        [Fact]
        public void If_bool_Then_ExpressionOperator_Else_ExpressionOperator_True()
        {
            var p = new Person();
            var rc = new RunCommand();
            var wc = new WalkCommand();
            op = (true) + (rc - wc);

            var res = st.Match(op, p);

            Assert.IsType<IfThenElse<Person>>(op);
            Assert.Equal(2, p.DistanceTravelled);
            Assert.True(res);
        }

        [Trait("ConditionalOperators", "IfThenElse")]
        [Fact]
        public void If_bool_Then_ExpressionOperator_Else_ExpressionOperator_False()
        {
            var p = new Person();
            var rc = new RunCommand();
            var wc = new WalkCommand();
            op = (false) + (rc - wc);

            var res = st.Match(op, p);

            Assert.IsType<IfThenElse<Person>>(op);
            Assert.Equal(1, p.DistanceTravelled);
            Assert.False(res);
        }

        [Trait("ConditionalOperators", "IfThenElse")]
        [Fact]
        public void If_ExpressionOperator_Then_ExpressionOperator_Else_ExpressionOperator_True()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var rc = new RunCommand();
            var wc = new WalkCommand();
            op = (dc < 10) + (rc - wc);

            var res = st.Match(op, p);

            Assert.IsType<IfThenElse<Person>>(op);
            Assert.Equal(2, p.DistanceTravelled);
            Assert.True(res);
        }

        [Trait("ConditionalOperators", "IfThenElse")]
        [Fact]
        public void If_ExpressionOperator_Then_ExpressionOperator_Else_ExpressionOperator_False()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var rc = new RunCommand();
            var wc = new WalkCommand();
            op = (dc > 10) + (rc - wc);

            var res = st.Match(op, p);

            Assert.IsType<IfThenElse<Person>>(op);
            Assert.Equal(1, p.DistanceTravelled);
            Assert.False(res);
        }

        [Trait("ConditionalOperators", "IfThen")]
        [Fact]
        public void If_Operator_Then_Command_True()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var sc = new SleepCommand();
            op = (dc < 10) + (sc);

            var res = st.Match(op, p);

            Assert.IsType<IfThen<Person>>(op);
            Assert.Equal(Status.Sleep, p.Status);
            Assert.True(res);
        }

        [Trait("ConditionalOperators", "IfThen")]
        [Fact]
        public void If_Operator_Then_Command_False()
        {
            var p = new Person();
            p.WakeUp();
            var dc = new GetDistanceCommand();
            var sc = new SleepCommand();
            op = (dc > 10) + (sc);

            var res = st.Match(op, p);

            Assert.IsType<IfThen<Person>>(op);
            Assert.Equal(Status.WakeUp, p.Status);
            Assert.False(res);
        }

        [Trait("ConditionalOperators", "IfThen")]
        [Fact]
        public void If_Operator_Then_Function_True()
        {
            var p = new Person();
            var gac = new GetAgeCommand();
            var aaf = new AddAgeFunction();
            op = (gac <= 0) + (aaf - 1);

            var res = st.Match(op, p);

            Assert.IsType<IfThen<Person>>(op);
            Assert.Equal(1, p.Age);
            Assert.True(res);
        }

        [Trait("ConditionalOperators", "IfThen")]
        [Fact]
        public void If_Operator_Then_Function_False()
        {
            var p = new Person();
            var gac = new GetAgeCommand();
            var aaf = new AddAgeFunction();
            op = (gac > 10) + (aaf);

            var res = st.Match(op, p);

            Assert.IsType<IfThen<Person>>(op);
            Assert.Equal(0, p.Age);
            Assert.False(res);
        }

        [Trait("ConditionalOperators", "IfThen")]
        [Fact]
        public void If_Bool_Then_Command_True()
        {
            var p = new Person();
            p.WakeUp();
            var sc = new SleepCommand();
            op = (true) + (sc);

            var res = st.Match(op, p);

            Assert.IsType<IfThen<Person>>(op);
            Assert.Equal(Status.Sleep, p.Status);
            Assert.True(res);
        }

        [Trait("ConditionalOperators", "IfThen")]
        [Fact]
        public void If_Bool_Then_Command_False()
        {
            var p = new Person();
            p.WakeUp();
            var sc = new SleepCommand();
            op = (false) + (sc);

            var res = st.Match(op, p);

            Assert.IsType<IfThen<Person>>(op);
            Assert.Equal(Status.WakeUp, p.Status);
            Assert.False(res);
        }

        [Trait("ConditionalOperators", "IfThen")]
        [Fact]
        public void If_Bool_Then_BooleanFunction_True()
        {
            var p = new Person();
            var aaf = new AddAgeFunction(10);
            op = (true) + (aaf);

            var res = st.Match(op, p);

            Assert.IsType<IfThen<Person>>(op);
            Assert.Equal(10, p.Age);
            Assert.True(res);
        }

        [Trait("ConditionalOperators", "IfThen")]
        [Fact]
        public void If_Bool_Then_BooleanFunction_False()
        {
            var p = new Person();
            var aaf = new AddAgeFunction();
            aaf.Parameters = 10;
            op = (false) + (aaf);

            var res = st.Match(op, p);

            Assert.IsType<IfThen<Person>>(op);
            Assert.Equal(0, p.Age);
            Assert.False(res);
        }

    }
}
