using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Elements.Casts;
using RaraAvis.Sprocket.RuleEngine.Elements.Operates.Commands;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions;
using RaraAvis.Sprocket.Tests.Fakes.System;
using RaraAvis.Sprocket.WorkflowEngine.Entities.Enums;
using System;
using System.Collections.Generic;
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
        public void Batch_Batch_Command_True()
        {
            var p = new Person();
            var wc = new WalkCommand();
            var rc = new RunCommand();
            var op = rc / wc / rc;

            st.Start(op, p);

            Assert.IsType<OperateAsOperator<Person, bool>>(op);
            Assert.Equal(5, p.DistanceTravelled);
        }

        [Trait("Kernel", "Batch")]
        [Fact]
        public void Batch_Command_Batch_True()
        {
            var p = new Person();
            var rc = new RunCommand();
            var wc = new WalkCommand();
            var op = (rc) / (rc / wc);

            st.Start(op, p);

            Assert.IsType<OperateAsOperator<Person, bool>>(op);
            Assert.Equal(5, p.DistanceTravelled);
        }

        [Trait("Kernel", "Batch")]
        [Fact]
        public void Batch_Function_Function_True()
        {
            var p = new Person();
            var sn = new SetNameFunction();
            sn.Parameters = "name";
            var ss = new SetSurnameFunction();
            ss.Parameters = "surname";
            var op = sn / ss;

            st.Start(op, p);

            Assert.IsType<OperateAsOperator<Person, bool>>(op);
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
            var op = b1 / ag3;

            var res = st.Start(op, p);

            Assert.IsType<OperateAsOperator<Person, bool>>(op);
            Assert.Equal(6, p.Age);
            Assert.Equal(ExecutionEngineResult.Correct, res.ExecutionResult);
            Assert.Equal(StageAction.Finish, res.StageAction);
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
            var op = ag3 / b1;

            var res = st.Start(op, p);

            Assert.IsType<OperateAsOperator<Person, bool>>(op);
            Assert.Equal(6, p.Age);
            Assert.Equal(ExecutionEngineResult.Correct, res.ExecutionResult);
            Assert.Equal(StageAction.Finish, res.StageAction);
        }

        [Trait("Kernel", "JMP")]
        [Fact]
        public void Operator_JMP_SeveralLines_True()
        {
            var p = new Person() { Name = string.Empty };
            var snf = new SetNameFunction();
            var ssf = new SetSurnameFunction();
            var rc = new TrueCommand<Person>();
            var opName = (snf - "Name");
            var opSurname = (ssf - "Surname");
            var opAll = opName / opSurname;
            var opConditional = rc % opAll;
            var op = opConditional / opAll / opSurname;

            var res = st.Start(op, p);

            Assert.Equal("Name", p.Name);
            Assert.Equal("Surname", p.Surname);
            Assert.Equal(ExecutionEngineResult.Correct, res.ExecutionResult);
            Assert.Equal(StageAction.Finish, res.StageAction);
        }

        [Trait("Kernel", "JMP")]
        [Fact]
        public void Operator_JMP_SingleLine_True()
        {
            var p = new Person() { Name = string.Empty };
            var snf = new SetNameFunction();
            var ssf = new SetSurnameFunction();
            var rc = new TrueCommand<Person>();
            var opName = (snf - "Name");
            var opSurname = (ssf - "Surname");
            var opConditional = rc % opName;
            var op = opConditional / opName / opSurname;

            var res = st.Start(op, p);

            Assert.Equal("Name", p.Name);
            Assert.Null(p.Surname);
            Assert.Equal(ExecutionEngineResult.Correct, res.ExecutionResult);
            Assert.Equal(StageAction.Finish, res.StageAction);
        }

        [Trait("Kernel", "JMP")]
        [Fact]
        public void Operator_JMP_SeveralLines_False()
        {
            var p = new Person();
            var snf = new SetNameFunction();
            var ssf = new SetSurnameFunction();
            var rc = new FalseCommand<Person>();
            var opName = (snf - "Name");
            var opSurname = (ssf - "Surname");
            var opConditional = rc % opName;
            var opAll = opName / opSurname;
            var op = opConditional / opName / opAll;

            var res = st.Start(op, p);

            Assert.Equal("Name", p.Name);
            Assert.Equal("Surname", p.Surname);
            Assert.Equal(ExecutionEngineResult.Correct, res.ExecutionResult);
            Assert.Equal(StageAction.Finish, res.StageAction);
        }

        [Trait("Kernel", "JMP")]
        [Fact]
        public void Operator_JMP_SingleLine_False()
        {
            var p = new Person();
            var snf = new SetNameFunction();
            var ssf = new SetSurnameFunction();
            var fc = new FalseCommand<Person>();
            var opName = (snf - "Name");
            var opSurname = (ssf - "Surname");
            var opConditional = fc % opName;
            var op = opConditional / opName / opSurname;

            var res = st.Start(op, p);

            Assert.Null(p.Name);
            Assert.Equal("Surname", p.Surname);
            Assert.Equal(ExecutionEngineResult.Correct, res.ExecutionResult);
            Assert.Equal(StageAction.Finish, res.StageAction);
        }

        [Trait("Kernel", "Break")]
        [Fact]
        public void Break()
        {
            Guid id1 = Guid.NewGuid();
            var p = new Person();
            var snf = new SetNameFunction();
            var op = ~(snf - "new");

            var res = st.Start(op, p);

            Assert.Equal("new", p.Name);
        }

        [Trait("Kernel", "Break")]
        [Fact]
        public void Break_ExpressionOperator_Exit()
        {
            Guid id1 = Guid.NewGuid();
            var p = new Person();
            GetDistanceCommand dc = new GetDistanceCommand();
            var op = ~(dc < 5);

            var res = st.Start(op, p);

            Assert.Equal(0, p.DistanceTravelled);
        }

        [Trait("Kernel", "Break")]
        [Fact]
        public void Break_ExpressionOperator_Wrong()
        {
            Guid id1 = Guid.NewGuid();
            var p = new Person();
            GetDistanceCommand dc = new GetDistanceCommand();
            var op = ~(dc > 5);

            var res = st.Start(op, p);

            Assert.Equal(0, p.DistanceTravelled);
        }
    }
}
