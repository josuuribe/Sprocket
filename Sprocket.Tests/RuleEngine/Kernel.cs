using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Elements.Casts;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.ComparisonOperators;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions;
using RaraAvis.Sprocket.Tests.Fakes.System;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class Kernel
    {
        private static WorflowEngineTest st = null;

        public Kernel()
        {
            st = new WorflowEngineTest();
        }

        public void Dispose()
        {
            st = null;
        }

        [Trait("Kernel", "Batch")]
        [Fact]
        public void Batch_PointerOperator_True()
        {
            var p = new Person() { Age = 10 };
            Expression<Func<Rule<Person>, bool>> f = (p) => p.Element.Age > 5;
            Operator<Person> op = f;

            var res = st.Start(op, p);

            Assert.IsType<BooleanOperandAsOperator<Person>>(op);
            Assert.Equal(10, p.Age);
            Assert.Equal(ExecutionResult.Positive, res.ExecutionResult);
        }

        [Trait("Kernel", "Batch")]
        [Fact]
        public void Batch_PointerOperator_False()
        {
            var p = new Person() { Age = 10 };
            Expression<Func<Rule<Person>, bool>> f = (p) => p.Element.Age < 5;
            Operator<Person> op = f;

            var res = st.Start(op, p);

            Assert.IsType<BooleanOperandAsOperator<Person>>(op);
            Assert.Equal(10, p.Age);
            Assert.Equal(ExecutionResult.Negative, res.ExecutionResult);
        }

        [Trait("Kernel", "Batch")]
        [Fact]
        public void Batch_PointerOperand_True()
        {
            var p = new Person() { Age = 10 };
            Expression<Func<Rule<Person>, int>> f = (p) => p.Element.Age;
            Operand<Person, int> operand = f;

            var op = operand > 5;

            var res = st.Start(op, p);

            Assert.IsType<GreaterThan<Person, IComparable>>(op);
            Assert.Equal(10, p.Age);
            Assert.Equal(ExecutionResult.Positive, res.ExecutionResult);
        }

        [Trait("Kernel", "Batch")]
        [Fact]
        public void Batch_PointerOperand_False()
        {
            var p = new Person() { Age = 10 };
            Expression<Func<Rule<Person>, int>> f = (p) => p.Element.Age;
            Operand<Person, int> operand = f;

            var op = operand < 5;

            var res = st.Start(op, p);

            Assert.IsType<LessThan<Person, IComparable>>(op);
            Assert.Equal(10, p.Age);
            Assert.Equal(ExecutionResult.Negative, res.ExecutionResult);
        }

        [Trait("Kernel", "Batch")]
        [Fact]
        public void Batch_Batch_Command_True()
        {
            var p = new Person();
            var wc = new WalkCommand();
            var rc1 = new RunCommand();
            var rc2 = new RunCommand();
            Operator<Person> op = rc1 / wc / rc2;

            st.Start(op, p);

            Assert.IsType<BooleanOperandAsOperator<Person>>(op);
            Assert.Equal(5, p.DistanceTravelled);
        }

        [Trait("Kernel", "Batch")]
        [Fact]
        public void Batch_Command_Batch_True()
        {
            var p = new Person();
            var rc1 = new RunCommand();
            var rc2 = new RunCommand();
            var wc = new WalkCommand();
            Operator<Person> op = (rc1) / (rc2 / wc);

            st.Start(op, p);

            Assert.IsType<BooleanOperandAsOperator<Person>>(op);
            Assert.Equal(5, p.DistanceTravelled);
        }

        [Trait("Kernel", "Batch")]
        [Fact]
        public void Batch_Function_Function_True()
        {
            var p = new Person();
            var sn = new SetNameFunction("name");
            var ss = new SetSurnameFunction("surname");
            Operator<Person> op = sn / ss;

            st.Start(op, p);

            Assert.IsType<BooleanOperandAsOperator<Person>>(op);
            Assert.Equal("name", p.Name);
            Assert.Equal("surname", p.Surname);
        }

        [Trait("Kernel", "Batch")]
        [Fact]
        public void Batch_Batch_Function_True()
        {
            var p = new Person();
            var ag1 = new AddAgeFunction(1);
            var ag2 = new AddAgeFunction(2);
            var ag3 = new AddAgeFunction(3);
            var b1 = ag1 / ag2;
            Operator<Person> op = b1 / ag3;

            var res = st.Start(op, p);

            Assert.IsType<BooleanOperandAsOperator<Person>>(op);
            Assert.Equal(6, p.Age);
            Assert.Equal(ExecutionResult.Positive, res.ExecutionResult);
        }

        [Trait("Kernel", "Batch")]
        [Fact]
        public void Batch_Function_Batch_True()
        {
            var p = new Person();
            var ag1 = new AddAgeFunction(1);
            var ag2 = new AddAgeFunction(2);
            var ag3 = new AddAgeFunction(3);
            var b1 = ag1 / ag2;
            Operator<Person> op = ag3 / b1;

            var res = st.Start(op, p);

            Assert.IsType<BooleanOperandAsOperator<Person>>(op);
            Assert.Equal(6, p.Age);
            Assert.Equal(ExecutionResult.Positive, res.ExecutionResult);
        }

        //[Trait("Kernel", "JMP")]
        //[Fact]
        //public void Operator_Jump_True()
        //{
        //    var p = new Person() { Name = string.Empty };
        //    var snf = new SetNameFunction();
        //    var ssf1 = new SetSurnameFunction();
        //    var ssf2 = new SetSurnameFunction();
        //    var tc = new TrueCommand<Person>();
        //    var opFalse = (ssf2 - "SurnameWrong");
        //    var opTrue = (snf - "Name") / (ssf1 - "Surname");
        //    Operator<Person> ok = true;
        //    var opConditional = ok % (opTrue, opFalse);

        //    var res = st.Start(opConditional, p);

        //    Assert.Equal("Name", p.Name);
        //    Assert.Equal("Surname", p.Surname);
        //    Assert.Equal(ExecutionEngineResult.Correct, res.ExecutionResult);
        //    Assert.Equal(StageAction.Finish, res.StageAction);
        //}

        //[Trait("Kernel", "JMP")]
        //[Fact]
        //public void Operator_Jump_False()
        //{
        //    var p = new Person();
        //    var snf = new SetNameFunction();
        //    var ssf1 = new SetSurnameFunction();
        //    var ssf2 = new SetSurnameFunction();
        //    var fc = new FalseCommand<Person>();
        //    var opFalse = (ssf2 - "SurnameWrong");
        //    var opTrue = (snf - "Name") / (ssf1 - "Surname");
        //    var opConditional = fc % (opTrue, opFalse);

        //    var res = st.Start(opConditional, p);

        //    Assert.Null(p.Name);
        //    Assert.Equal("SurnameWrong", p.Surname);
        //    Assert.Equal(ExecutionEngineResult.Correct, res.ExecutionResult);
        //    Assert.Equal(StageAction.Finish, res.StageAction);
        //}

        //[Trait("Kernel", "Break")]
        //[Fact]
        //public void Break()
        //{
        //    Guid id1 = Guid.NewGuid();
        //    var p = new Person();
        //    var snf = new SetNameFunction("new");
        //    var op = ~(snf);

        //    var res = st.Start(op, p);

        //    Assert.Equal("new", p.Name);
        //}

        [Trait("Kernel", "Break")]
        [Fact]
        public void Break_Operator_Exit()
        {
            var p = new Person();
            GetDistanceCommand dc = new GetDistanceCommand();
            var op = (dc < 5) ^ ExecutionResult.Exit;

            var res = st.Start(op, p);

            Assert.Equal(0, p.DistanceTravelled);
            Assert.Equal(ExecutionResult.Exit, res.ExecutionResult);
        }

        [Trait("Kernel", "Break")]
        [Fact]
        public void Break_Operator_Negative()
        {
            var p = new Person();
            GetDistanceCommand dc = new GetDistanceCommand();
            var op = (dc > 5) ^ ExecutionResult.Negative;

            var res = st.Start(op, p);

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
            var op = (dc < 5) % rc ;

            var res = st.Start(op, p);

            Assert.Equal(2, p.DistanceTravelled);
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

            var res = st.Start(op, p);

            Assert.Equal(1, p.DistanceTravelled);
        }

        [Trait("Kernel", "Break")]
        [Fact]
        public void AddFlag_True()
        {
            var p = new Person() { Age = 10 };
            Operator<Person> haf = new HasAgeFunction(10);
            var op = (haf) >> 1;

            var res = st.Start(op, p);

            Assert.Equal(ExecutionResult.Positive, res.ExecutionResult);
            Assert.Equal(1, res.UserStatus);
        }

        [Trait("Kernel", "Break")]
        [Fact]
        public void AddFlag_False()
        {
            var p = new Person() { Age = 5 };
            Operator<Person> haf = new HasAgeFunction(10);
            var op = (haf) >> 1;

            var res = st.Start(op, p);

            Assert.Equal(ExecutionResult.Negative, res.ExecutionResult);
            Assert.Equal(0, res.UserStatus);
        }

        [Trait("Kernel", "Break")]
        [Fact]
        public void Remove_True()
        {
            var p = new Person() { Age = 10 };
            Operator<Person> haf = new HasAgeFunction(10);
            var op = (haf) >> 15 && (haf) << 1;

            var res = st.Start(op, p);

            Assert.Equal(ExecutionResult.Positive, res.ExecutionResult);
            Assert.Equal(14, res.UserStatus);
        }

        [Trait("Kernel", "Break")]
        [Fact]
        public void Remove_False()
        {
            var p = new Person() { Age = 5 };
            Operator<Person> haf = new HasAgeFunction(10);
            var op = (haf) << 1;

            var res = st.Start(op, p);

            Assert.Equal(ExecutionResult.Negative, res.ExecutionResult);
            Assert.Equal(0, res.UserStatus);
        }
    }
}
