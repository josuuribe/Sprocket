using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.ExpressionOperators.BinaryOperators;
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
    public class BinaryOperators
    {
        private WorflowEngineTest st = null;
        private Operator<Person> op = null;

        public BinaryOperators()
        {
            st = new WorflowEngineTest();
        }

        [Trait("BinaryOperators", "And")]
        [Fact]
        public void And_Operator_Operator_True()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var rc = new RunCommand();
            var op1 = (dc < 10) + (rc);
            var op2 = (dc < 10) + (rc);
            op = op1 && op2;

            var res = st.Match(op, p);

            Assert.IsType<And<Person>>(op);
            Assert.Equal(4, p.DistanceTravelled);
            Assert.True(res);
        }

        [Trait("BinaryOperators", "And")]
        [Fact]
        public void And_Operator_Operator_False()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var op1 = (dc < 10);
            var op2 = (dc > 10);
            op = op1 && op2;

            var res = st.Match(op, p);

            Assert.IsType<And<Person>>(op);
            Assert.False(res);
        }

        [Trait("BinaryOperators", "AndAlso")]
        [Fact]
        public void AndAlso_Operator_Operator_True()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var rc = new RunCommand();
            var op1 = (dc < 10) + (rc);
            var op2 = (dc < 10) + (rc);
            op = op1 & op2;

            var res = st.Match(op, p);

            Assert.IsType<AndAlso<Person>>(op);
            Assert.Equal(4, p.DistanceTravelled);
            Assert.True(res);
        }

        [Trait("BinaryOperators", "AndAlso")]
        [Fact]
        public void AndAlso_Operator_Operator_False()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var rc = new RunCommand();
            var op1 = (dc < 10) + (rc);
            var op2 = (dc > 10) + (rc);
            op = op1 & op2;

            var res = st.Match(op, p);

            Assert.IsType<AndAlso<Person>>(op);
            Assert.Equal(2, p.DistanceTravelled);
            Assert.False(res);
        }

        [Trait("BinaryOperators", "OrElse")]
        [Fact]
        public void OrElse_Operator_Bool_True()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var rc = new RunCommand();
            op = (dc < 10) + (rc);
            op = op | false;

            var res = st.Match(op, p);

            Assert.IsType<OrElse<Person>>(op);
            Assert.Equal(2, p.DistanceTravelled);
            Assert.True(res);
        }

        [Trait("BinaryOperators", "OrElse")]
        [Fact]
        public void OrElse_Operator_Bool_False()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var rc = new RunCommand();
            var op1 = (dc > 10) + (rc);
            op = op1 | false;

            var res = st.Match(op, p);

            Assert.IsType<OrElse<Person>>(op);
            Assert.Equal(0, p.DistanceTravelled);
            Assert.False(res);
        }

        [Trait("BinaryOperators", "AndAlso")]
        [Fact]
        public void AndAlso_Operator_Bool_True()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var rc = new RunCommand();
            var op1 = (dc < 10) & (rc);
            op = op1 & true;

            var res = st.Match(op, p);

            Assert.IsType<AndAlso<Person>>(op);
            Assert.Equal(2, p.DistanceTravelled);
            Assert.True(res);
        }

        [Trait("BinaryOperators", "AndAlso")]
        [Fact]
        public void AndAlso_Operator_Bool_False()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var rc = new RunCommand();
            var op1 = (dc < 10) & (rc);
            op = op1 & false;

            var res = st.Match(op, p);

            Assert.IsType<AndAlso<Person>>(op);
            Assert.Equal(2, p.DistanceTravelled);
            Assert.False(res);
        }

        [Trait("BinaryOperators", "AndAlso")]
        [Fact]
        public void AndAlso_Bool_Operator_True()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var rc = new RunCommand();
            var op1 = (dc < 10) & (rc);
            op = true & op1;

            var res = st.Match(op, p);

            Assert.IsType<AndAlso<Person>>(op);
            Assert.Equal(2, p.DistanceTravelled);
            Assert.True(res);
        }

        [Trait("BinaryOperators", "AndAlso")]
        [Fact]
        public void AndAlso_Bool_Operator_False()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var rc = new RunCommand();
            var op1 = (dc < 10) & (rc);
            op = false & op1;

            var res = st.Match(op, p);

            Assert.IsType<AndAlso<Person>>(op);
            Assert.Equal(2, p.DistanceTravelled);
            Assert.False(res);
        }

        [Trait("BinaryOperators", "AndAlso")]
        [Fact]
        public void AndAlso_Function_Function_True()
        {
            var p = new Person() { Age = 10 };
            HasAgeFunction haf = new HasAgeFunction(10);
            AddAgeFunction aaf = new AddAgeFunction(20);
            op = (+haf) & (+aaf);

            var res = st.Match(op, p);

            Assert.IsType<AndAlso<Person>>(op);
            Assert.Equal(30, p.Age);
            Assert.True(res);
        }

        [Trait("BinaryOperators", "AndAlso")]
        [Fact]
        public void AndAlso_Function_Function_False()
        {
            Person p = new Person();
            HasAgeFunction haf = new HasAgeFunction(10);
            AddAgeFunction aaf = new AddAgeFunction(20);
            op = (-haf) & (-aaf);

            var res = st.Match(op, p);

            Assert.IsType<AndAlso<Person>>(op);
            Assert.Equal(20, p.Age);
            Assert.False(res);
        }

        [Trait("BinaryOperators", "OrElse")]
        [Fact]
        public void OrElse_Bool_Operator_False()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var rc = new RunCommand();
            var op1 = (dc > 10) + (rc);
            op = false | op1;

            var res = st.Match(op, p);

            Assert.IsType<OrElse<Person>>(op);
            Assert.Equal(0, p.DistanceTravelled);
            Assert.False(res);
        }

        [Trait("BinaryOperators", "OrElse")]
        [Fact]
        public void OrElse_Operator_Operator_True()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var rc = new RunCommand();
            var op1 = (dc < 10) + (rc);
            var op2 = (dc < 10) + (rc);
            op = op1 | op2;

            var res = st.Match(op, p);

            Assert.IsType<OrElse<Person>>(op);
            Assert.Equal(4, p.DistanceTravelled);
            Assert.True(res);
        }

        [Trait("BinaryOperators", "OrElse")]
        [Fact]
        public void OrElse_Operator_Operator_False()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var rc = new RunCommand();
            var op1 = (dc > 10) + (rc);
            var op2 = (dc > 10) + (rc);
            op = op1 | op2;

            var res = st.Match(op, p);

            Assert.IsType<OrElse<Person>>(op);
            Assert.Equal(0, p.DistanceTravelled);
            Assert.False(res);
        }

        [Trait("BinaryOperators", "OrElse")]
        [Fact]
        public void OrElse_BooleanFunction_BooleanFunction_True()
        {
            var p = new Person();
            var aaf1 = new AddAgeFunction(10);
            var haf1 = new HasAgeFunction(10);
            op = (+haf1) | (+aaf1);

            var res = st.Match(op, p);

            Assert.IsType<OrElse<Person>>(op);
            Assert.Equal(10, p.Age);
            Assert.True(res);
        }

        [Trait("BinaryOperators", "OrElse")]
        [Fact]
        public void OrElse_BooleanFunction_BooleanFunction_False()
        {
            var p = new Person();
            var haf1 = new HasAgeFunction(10);
            var haf2 = new HasAgeFunction(5);
            op = (-haf1) | (-haf2);

            var res = st.Match(op, p);

            Assert.IsType<OrElse<Person>>(op);
            Assert.Equal(0, p.Age);
            Assert.False(res);
        }

        [Trait("BinaryOperators", "And")]
        [Fact]
        public void And_Command_Command_False()
        {
            var p = new Person();
            var gdc1 = new GetDistanceCommand();
            var gdc2 = new GetDistanceCommand();
            op = (gdc1 > 10) && (gdc2 > 20);

            var res = st.Match(op, p);

            Assert.IsType<And<Person>>(op);
            Assert.False(res);
        }

        [Trait("BinaryOperators", "And")]
        [Fact]
        public void And_Command_Command_True()
        {
            var p = new Person() { DistanceTravelled = 25 };
            var gdc1 = new GetDistanceCommand();
            var gdc2 = new GetDistanceCommand();
            op = (gdc1 > 10) && (gdc2 > 20);

            var res = st.Match(op, p);

            Assert.IsType<And<Person>>(op);
            Assert.True(res);
        }

        [Trait("BinaryOperators", "Or")]
        [Fact]
        public void Or_Command_Command_False()
        {
            var p = new Person();
            var gdc1 = new GetDistanceCommand();
            var gdc2 = new GetDistanceCommand();
            op = (gdc1 > 10) || (gdc2 > 20);

            var res = st.Match(op, p);

            Assert.IsType<Or<Person>>(op);
            Assert.False(res);
        }

        [Trait("BinaryOperators", "Or")]
        [Fact]
        public void Or_Command_Command_True()
        {
            var p = new Person() { DistanceTravelled = 15 };
            var gdc1 = new GetDistanceCommand();
            var gdc2 = new GetDistanceCommand();
            op = (gdc1 > 10) || (gdc2 > 20);

            var res = st.Match(op, p);

            Assert.IsType<Or<Person>>(op);
            Assert.True(res);
        }
    }
}
