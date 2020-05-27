using RaraAvis.Sprocket.RuleEngine.Operators.BinaryOperators;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions;
using RaraAvis.Sprocket.Tests.Fakes.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.SyworflowEngineTestem;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class BinaryOperatorsTests : IClassFixture<WorkflowEngineTest>
    {
        private readonly WorkflowEngineTest workflowEngineTest;
        public BinaryOperatorsTests(WorkflowEngineTest worflowEngineTest)
        {
            this.workflowEngineTest = worflowEngineTest;
        }

        [Trait("BinaryOperators", "And")]
        [Fact]
        public void And_Operator_Operator_True()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var rc = new RunCommand();
            var op1 = (dc < 10) % (rc);
            var op2 = (dc < 10) % (rc);
            var op = op1 && op2;

            var res = workflowEngineTest.Match(op, p);

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
            var op = op1 && op2;

            var res = workflowEngineTest.Match(op, p);

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
            var op1 = (dc < 10) % (rc);
            var op2 = (dc < 10) % (rc);
            var op = op1 & op2;

            var res = workflowEngineTest.Match(op, p);

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
            var op1 = (dc < 10) % (rc);
            var op2 = (dc > 10) % (rc);
            var op = op1 & op2;

            var res = workflowEngineTest.Match(op, p);

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
            var op1 = (dc < 10) % (rc);
            var op = op1 | false;

            var res = workflowEngineTest.Match(op, p);

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
            var op1 = (dc > 10) % (rc);
            var op = op1 | false;

            var res = workflowEngineTest.Match(op, p);

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
            var op = op1 & true;

            var res = workflowEngineTest.Match(op, p);

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
            var op = op1 & false;

            var res = workflowEngineTest.Match(op, p);

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
            var op = true & op1;

            var res = workflowEngineTest.Match(op, p);

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
            var op = false & op1;

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<AndAlso<Person>>(op);
            Assert.Equal(2, p.DistanceTravelled);
            Assert.False(res);
        }

        [Trait("BinaryOperators", "AndAlso")]
        [Fact]
        public void AndAlso_Function_Function_False()
        {
            Person p = new Person();
            HasAgeFunction haf = new HasAgeFunction(10);
            AddAgeFunction aaf = new AddAgeFunction(20);
            var op = (-haf) & (-aaf);

            var res = workflowEngineTest.Match(op, p);

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
            var op1 = (dc > 10) % (rc);
            var op = false | op1;

            var res = workflowEngineTest.Match(op, p);

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
            var op1 = (dc < 10) % (rc);
            var op2 = (dc < 10) % (rc);
            var op = op1 | op2;

            var res = workflowEngineTest.Match(op, p);

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
            var op1 = (dc > 10) % (rc);
            var op2 = (dc > 10) % (rc);
            var op = op1 | op2;

            var res = workflowEngineTest.Match(op, p);

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
            var op = (+haf1) | (+aaf1);

            var res = workflowEngineTest.Match(op, p);

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
            var op = (-haf1) | (-haf2);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<OrElse<Person>>(op);
            Assert.Equal(0, p.Age);
            Assert.False(res);
        }

        [Trait("BinaryOperators", "And")]
        [Fact]
        public void And_Operand_Command_False()
        {
            var p = new Person();
            var gdc1 = new GetDistanceCommand();
            var gdc2 = new GetDistanceCommand();
            var op = (gdc1 > 10) && (gdc2 > 20);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<And<Person>>(op);
            Assert.False(res);
        }

        [Trait("BinaryOperators", "And")]
        [Fact]
        public void And_Operand_Command_True()
        {
            var p = new Person() { DistanceTravelled = 25 };
            var gdc1 = new GetDistanceCommand();
            var gdc2 = new GetDistanceCommand();
            var op = (gdc1 > 10) && (gdc2 > 20);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<And<Person>>(op);
            Assert.True(res);
        }

        [Trait("BinaryOperators", "Or")]
        [Fact]
        public void Or_Operand_Command_False()
        {
            var p = new Person();
            var gdc1 = new GetDistanceCommand();
            var gdc2 = new GetDistanceCommand();
            var op = (gdc1 > 10) || (gdc2 > 20);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<Or<Person>>(op);
            Assert.False(res);
        }

        [Trait("BinaryOperators", "Or")]
        [Fact]
        public void Or_Operand_Command_True()
        {
            var p = new Person() { DistanceTravelled = 15 };
            var gdc1 = new GetDistanceCommand();
            var gdc2 = new GetDistanceCommand();
            var op = (gdc1 > 10) || (gdc2 > 20);

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<Or<Person>>(op);
            Assert.True(res);
        }
    }
}
