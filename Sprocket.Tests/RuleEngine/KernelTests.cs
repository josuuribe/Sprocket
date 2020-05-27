using RaraAvis.Sprocket.RuleEngine;
using RaraAvis.Sprocket.RuleEngine.Casts;
using RaraAvis.Sprocket.RuleEngine.Operators.ComparisonOperators;
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
    public class KernelTests : IClassFixture<WorkflowEngineTest>
    {
        private readonly WorkflowEngineTest workflowEngineTest = null;

        public KernelTests(WorkflowEngineTest worflowEngineTest)
        {
            this.workflowEngineTest = worflowEngineTest;
        }

        [Trait("Kernel", "Batch")]
        [Fact]
        public void Batch_PointerOperator_True()
        {
            var p = new Person() { Age = 10 };
            Expression<Func<Rule<Person>, bool>> f = (p) => p.Target.Age > 5;
            Operator<Person> op = f;

            var res = workflowEngineTest.Start(op, p);

            Assert.IsType<BooleanOperandAsOperator<Person>>(op);
            Assert.Equal(10, p.Age);
            Assert.Equal(ExecutionResult.Positive, res.ExecutionResult);
        }

        [Trait("Kernel", "Batch")]
        [Fact]
        public void Batch_PointerOperator_False()
        {
            var p = new Person() { Age = 10 };
            Expression<Func<Rule<Person>, bool>> f = (p) => p.Target.Age < 5;
            Operator<Person> op = f;

            var res = workflowEngineTest.Start(op, p);

            Assert.IsType<BooleanOperandAsOperator<Person>>(op);
            Assert.Equal(10, p.Age);
            Assert.Equal(ExecutionResult.Negative, res.ExecutionResult);
        }

        [Trait("Kernel", "Batch")]
        [Fact]
        public void Batch_PointerOperand_True()
        {
            var p = new Person() { Age = 10 };
            Expression<Func<Rule<Person>, int>> f = (p) => p.Target.Age;
            Operand<Person, int> operand = f;

            var op = operand > 5;

            var res = workflowEngineTest.Start(op, p);

            Assert.IsType<GreaterThan<Person, IComparable>>(op);
            Assert.Equal(10, p.Age);
            Assert.Equal(ExecutionResult.Positive, res.ExecutionResult);
        }

        [Trait("Kernel", "Batch")]
        [Fact]
        public void Batch_PointerOperand_False()
        {
            var p = new Person() { Age = 10 };
            Expression<Func<Rule<Person>, int>> f = (p) => p.Target.Age;
            Operand<Person, int> operand = f;

            var op = operand < 5;

            var res = workflowEngineTest.Start(op, p);

            Assert.IsType<LessThan<Person, IComparable>>(op);
            Assert.Equal(10, p.Age);
            Assert.Equal(ExecutionResult.Negative, res.ExecutionResult);
        }

        [Trait("Kernel", "Batch")]
        [Fact]
        public void Batch_Operand()
        {
            var p = new Person();
            var wc = new WalkCommand();
            var rc1 = new RunCommand();
            var rc2 = new RunCommand();
            Operator<Person> op = rc1 / wc / rc2;

            workflowEngineTest.Match(op, p);

            Assert.IsType<BooleanOperandAsOperator<Person>>(op);
            Assert.Equal(5, p.DistanceTravelled);
        }

        [Trait("Kernel", "Batch")]
        [Fact]
        public void Batch_Function_Parameter()
        {
            var p = new Person();
            var sn = new SetNameFunction("name");
            var ss = new SetSurnameFunction("surname");
            Operator<Person> op = sn / ss;

            workflowEngineTest.Match(op, p);

            Assert.IsType<BooleanOperandAsOperator<Person>>(op);
            Assert.Equal("name", p.Name);
            Assert.Equal("surname", p.Surname);
        }

        [Trait("Kernel", "Batch")]
        [Fact]
        public void Batch_Function()
        {
            var p = new Person();
            var ag1 = new AddAgeFunction(1);
            var ag2 = new AddAgeFunction(2);
            var ag3 = new AddAgeFunction(3);
            var b1 = ag1 / ag2;
            Operator<Person> op = b1 / ag3;

            var res = workflowEngineTest.Match(op, p);

            Assert.IsType<BooleanOperandAsOperator<Person>>(op);
            Assert.Equal(6, p.Age);
            Assert.True(res);
        }

        [Trait("Kernel", "Break")]
        [Fact]
        public void Break_Operator_Exit()
        {
            var p = new Person();
            GetDistanceCommand dc = new GetDistanceCommand();
            var op = ~(dc < 5);

            var res = workflowEngineTest.Start(op, p);

            Assert.Equal(0, p.DistanceTravelled);
            Assert.Equal(ExecutionResult.Exit, res.ExecutionResult);
        }

        [Trait("Kernel", "Break")]
        [Fact]
        public void Break_Operator_Negative()
        {
            var p = new Person();
            GetDistanceCommand dc = new GetDistanceCommand();
            var op = ~(dc > 5);

            var res = workflowEngineTest.Start(op, p);

            Assert.Equal(0, p.DistanceTravelled);
            Assert.Equal(ExecutionResult.Negative, res.ExecutionResult);
        }

        [Trait("Kernel", "Break")]
        [Fact]
        public void Jump_True()
        {
            var p = new Person();
            GetDistanceCommand dc = new GetDistanceCommand();
            RunCommand rc = new RunCommand();
            var op = (dc < 5) % rc;

            var res = workflowEngineTest.Start(op, p);

            Assert.Equal(2, p.DistanceTravelled);
            Assert.Equal(ExecutionResult.Positive, res.ExecutionResult);
        }

        [Trait("Kernel", "Break")]
        [Fact]
        public void Jump_False()
        {
            var p = new Person();
            GetDistanceCommand dc = new GetDistanceCommand();
            RunCommand rc = new RunCommand();
            WalkCommand wc = new WalkCommand();
            var op = (dc > 5) % (rc, wc);

            var res = workflowEngineTest.Start(op, p);

            Assert.Equal(1, p.DistanceTravelled);
            Assert.Equal(ExecutionResult.Negative, res.ExecutionResult);
        }

        [Trait("Kernel", "AddFlag")]
        [Fact]
        public void AddFlag_True()
        {
            var p = new Person() { Age = 10 };
            Operator<Person> haf = new HasAgeFunction(10);
            var op = (haf) >> 1;

            var res = workflowEngineTest.Start(op, p);

            Assert.Equal(ExecutionResult.Positive, res.ExecutionResult);
            Assert.Equal(1, res.UserStatus);
        }

        [Trait("Kernel", "AddFlag")]
        [Fact]
        public void AddFlag_False()
        {
            var p = new Person() { Age = 5 };
            Operator<Person> haf = new HasAgeFunction(10);
            var op = (haf) >> 1;

            var res = workflowEngineTest.Start(op, p);

            Assert.Equal(ExecutionResult.Negative, res.ExecutionResult);
            Assert.Equal(0, res.UserStatus);
        }

        [Trait("Kernel", "RemoveFlag")]
        [Fact]
        public void Remove_True()
        {
            var p = new Person() { Age = 10 };
            Operator<Person> haf = new HasAgeFunction(10);
            var op = (haf) >> 15 && (haf) << 1;

            var res = workflowEngineTest.Start(op, p);

            Assert.Equal(ExecutionResult.Positive, res.ExecutionResult);
            Assert.Equal(14, res.UserStatus);
        }

        [Trait("Kernel", "RemoveFlag")]
        [Fact]
        public void Remove_False()
        {
            var p = new Person() { Age = 5 };
            Operator<Person> haf = new HasAgeFunction(10);
            var op = (haf) << 1;

            var res = workflowEngineTest.Start(op, p);

            Assert.Equal(ExecutionResult.Negative, res.ExecutionResult);
            Assert.Equal(0, res.UserStatus);
        }
    }
}
