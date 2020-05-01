using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.Parts.Elements.Functions;
using RaraAvis.Sprocket.Parts.Elements.Operators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.BinaryOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.IterationOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.UnaryOperators;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions;
using RaraAvis.Sprocket.Tests.Fakes.System;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class LogicalOperators : IDisposable
    {
        private static RunCommand rc = null;
        private static WalkCommand wc = null;
        private static RuleElement<Person> re = null;
        private static GetDistanceCommand dc = null;
        private static SerializeTest st = null;
        private static Person p = null;


        public LogicalOperators()
        {
            rc = new RunCommand();
            wc = new WalkCommand();
            dc = new GetDistanceCommand();
            re = new RuleElement<Person>();
            st = new SerializeTest();

            p = new Person();
            st.BeginSerialize();
        }

        public void Dispose()
        {
            st.EndSerialize();
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void And_Operator_Operator_True()
        {
            var op1 = (dc < 10) % (rc);
            var op2 = (dc < 10) % (rc);
            var op3 = op1 && op2;

            var res = st.Match(op3, p);

            Assert.IsType<And<Person>>(op3);
            Assert.True(p.DistanceTravelled == 4, "'And' operator (&) is not true.");
            Assert.True(res, "'And' operator (&) is not true.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void And_Operator_Operator_False()
        {
            var op1 = (dc < 10);
            var op2 = (dc > 10);
            var op3 = op1 && op2;

            var res = st.Match(op3, p);

            Assert.IsType<And<Person>>(op3);
            Assert.False(res, "'And' operator (&) is not false.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void AndAlso_Operator_Operator_True()
        {
            var op1 = (dc < 10) % (rc);
            var op2 = (dc < 10) % (rc);
            var op3 = op1 & op2;

            var res = st.Match(op3, p);

            Assert.IsType<AndAlso<Person>>(op3);
            Assert.True(p.DistanceTravelled == 4, "'And' operator (&) is not true.");
            Assert.True(res, "'And' operator (&) is not true.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void AndAlso_Operator_Operator_False()
        {
            var op1 = (dc < 10) % (rc);
            var op2 = (dc > 10) % (rc);
            var op3 = op1 & op2;

            var res = st.Match(op3, p);

            Assert.IsType<AndAlso<Person>>(op3);
            Assert.True(p.DistanceTravelled == 2, "'And' operator (&) is not true.");
            Assert.False(res, "'And' operator (&) is not true.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void Or_Operator_Bool_True()
        {
            Operator<Person> op = (dc < 10) % (rc);
            var op2 = op | false;

            var res = st.Match(op2, p);

            Assert.IsType<OrElse<Person>>(op2);
            Assert.True(p.DistanceTravelled == 2, "'And' operator (&) is not true.");
            Assert.True(res, "'Or' operator (|) is not true.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void Or_Operator_Bool_False()
        {
            Operator<Person> op = (dc > 10) % (rc);
            var op2 = op | false;

            var res = st.Match(op2, p);

            Assert.IsType<OrElse<Person>>(op2);
            Assert.True(p.DistanceTravelled == 0);
            Assert.False(res);
        }


        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void AndAlso_Operator_Bool_True()
        {
            Operator<Person> op = (dc < 10) % (rc);
            var op2 = op & true;

            var res = st.Match(op2, p);

            Assert.IsType<AndAlso<Person>>(op2);
            Assert.True(p.DistanceTravelled == 2, "'And' operator (&) is not true.");
            Assert.True(res, "'And' operator (&) is not true.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void AndAlso_Operator_Bool_False()
        {
            Operator<Person> op = (dc < 10) % (rc);
            var op2 = op & false;

            var res = st.Match(op2, p);

            Assert.IsType<AndAlso<Person>>(op2);
            Assert.True(p.DistanceTravelled == 2, "'And' operator (&) is not true.");
            Assert.False(res, "'And' operator (&) is not true.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void AndAlso_Bool_Operator_True()
        {
            Operator<Person> op = (dc < 10) % (rc);
            var op2 = true & op;

            var res = st.Match(op2, p);

            Assert.IsType<AndAlso<Person>>(op2);
            Assert.True(p.DistanceTravelled == 2, "'And' operator (&) is not true.");
            Assert.True(res, "'And' operator (&) is not true.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void AndAlso_Bool_Operator_False()
        {
            RunCommand rc = new RunCommand();
            Operator<Person> op = (dc < 10) % (rc);
            var op2 = false & op;

            var res = st.Match(op2, p);

            Assert.IsType<AndAlso<Person>>(op2);
            Assert.True(p.DistanceTravelled == 2, "'And' operator (&) is not true.");
            Assert.False(res, "'And' operator (&) is not true.");
        }

        //[Trait("RuleEngine", "LogicalOperators")]
        //[Fact]
        //public void AndAlso_BooleanCommand_Operator_True()
        //{
        //    TrueCommand<Person> tc = new TrueCommand<Person>();
        //    SetAsianCommand rc = new SetAsianCommand();
        //    Operator<Person> op1 = tc;
        //    Operator<Person> op2 = rc;
        //    var op3 = op1 & op2;

        //    var res = st.Match(op3, p);

        //    Assert.IsType<AndAlso<Person>>(op3);
        //    Assert.True(st.UserStatus == (int)Feature.Asian);
        //    Assert.True(res);
        //}

        //[Trait("RuleEngine", "LogicalOperators")]
        //[Fact]
        //public void AndAlso_BooleanCommand_Operator_False()
        //{
        //    FalseCommand<Person> tc = new FalseCommand<Person>();
        //    SetAsianCommand rc = new SetAsianCommand();
        //    Operator<Person> op1 = tc;
        //    Operator<Person> op2 = rc;
        //    var op3 = op2 & op1;

        //    var res = st.Match(op3, p);

        //    Assert.IsType<AndAlso<Person>>(op3);
        //    Assert.True(st.UserStatus == (int)Feature.Asian);
        //    Assert.False(res);
        //}

        //[Trait("RuleEngine", "LogicalOperators")]
        //[Fact]
        //public void AndAlso_Operator_BooleanCommand_True()
        //{
        //    TrueCommand<Person> tc = new TrueCommand<Person>();
        //    SetAsianCommand rc = new SetAsianCommand();
        //    Operator<Person> op1 = tc;
        //    Operator<Person> op2 = rc;
        //    var op3 = op2 & op1;

        //    var res = st.Match(op3, p);

        //    Assert.IsType<AndAlso<Person>>(op3);
        //    Assert.True(st.UserStatus == (int)Feature.Asian);
        //    Assert.True(res);
        //}


        //[Trait("RuleEngine", "LogicalOperators")]
        //[Fact]
        //public void AndAlso_Operator_BooleanCommand_False()
        //{
        //    FalseCommand<Person> tc = new FalseCommand<Person>();
        //    SetAsianCommand rc = new SetAsianCommand();
        //    Operator<Person> op1 = tc;
        //    Operator<Person> op2 = rc;
        //    var op3 = op2 & op1;

        //    var res = st.Match(op3, p);

        //    Assert.IsType<AndAlso<Person>>(op3);
        //    Assert.True(st.UserStatus == (int)Feature.Asian);
        //    Assert.False(res);
        //}

        //[Trait("RuleEngine", "LogicalOperators")]
        //[Fact]
        //public void OrElse_BooleanCommand_Command_False()
        //{
        //    TrueCommand<Person> tc = new TrueCommand<Person>();
        //    RightCommand rc = new RightCommand();
        //    p.Correct = true;
        //    Operator<Person> op = !tc | !rc;

        //    var res = st.Match(op, p);

        //    Assert.IsType<OrElse<Person>>(op);
        //    Assert.True(p.Correct);
        //    Assert.False(res);
        //}

        //[Trait("RuleEngine", "LogicalOperators")]
        //[Fact]
        //public void OrElse_BooleanCommand_Command_True()
        //{
        //    FalseCommand<Person> fc = new FalseCommand<Person>();
        //    WrongCommand wc = new WrongCommand();
        //    p.Correct = false;
        //    Operator<Person> op3 = !fc | !wc;

        //    var res = st.Match(op3, p);

        //    Assert.IsType<OrElse<Person>>(op3);
        //    Assert.False(p.Correct);
        //    Assert.True(res);
        //}

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void AndAlso_Function_Function_True()
        {
            p.Age = 10;
            HasAgeFunction haf = new HasAgeFunction(10);
            AddAgeFunction aaf = new AddAgeFunction(20);
            var op = (+haf) & (+aaf);

            var res = st.Match(op, p);

            Assert.IsType<AndAlso<Person>>(op);
            Assert.Equal(30, p.Age);
            Assert.True(res);
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void AndAlso_Function_Function_False()
        {
            HasAgeFunction haf = new HasAgeFunction(10);
            AddAgeFunction aaf = new AddAgeFunction(20);
            var op = (-haf) & (-aaf);

            var res = st.Match(op, p);

            Assert.IsType<AndAlso<Person>>(op);
            Assert.Equal(20, p.Age);
            Assert.False(res);
        }

        //[Trait("RuleEngine", "LogicalOperators")]
        //[Fact]
        //public void AndAlso_BooleanCommand_BooleanCommand_True()
        //{
        //    WalkCommand wc = new WalkCommand();
        //    Operator<Person> op = +wc & +wc;

        //    var res = st.Match(op, p);

        //    Assert.IsType<AndAlso<Person>>(op);
        //    Assert.True(p.DistanceTravelled == 2);
        //    Assert.True(res);
        //}

        //[Trait("RuleEngine", "LogicalOperators")]
        //[Fact]
        //public void AndAlso_BooleanCommand_BooleanCommand_False()
        //{
        //    WalkCommand wc = new WalkCommand();
        //    Operator<Person> op = (+wc) & !(+wc);

        //    var res = st.Match(op, p);

        //    Assert.IsType<AndAlso<Person>>(op);
        //    Assert.True(p.DistanceTravelled == 2);
        //    Assert.False(res);
        //}

        //[Trait("RuleEngine", "LogicalOperators")]
        //[Fact]
        //public void And_BooleanCommand_BooleanCommand_True()
        //{
        //    Operator<Person> op = +(rc + wc) && +(rc + wc);

        //    var res = st.Match(op, p);

        //    Assert.IsType<And<Person>>(op);
        //    Assert.True(p.DistanceTravelled == 6);
        //    Assert.True(res);
        //}

        //[Trait("RuleEngine", "LogicalOperators")]
        //[Fact]
        //public void And_BooleanCommand_BooleanCommand_False()
        //{
        //    RunCommand rc = new RunCommand();
        //    WalkCommand wc = new WalkCommand();
        //    Operator<Person> op = !(rc + wc) && +(rc + wc);

        //    var res = st.Match(op, p);

        //    Assert.IsType<And<Person>>(op);
        //    Assert.True(p.DistanceTravelled == 3);
        //    Assert.False(res);
        //}


        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void OrElse_Bool_Operator_False()
        {
            var op = (dc > 10) % (rc);
            var op2 = false | op;

            var res = st.Match(op2, p);

            Assert.IsType<OrElse<Person>>(op2);
            Assert.True(p.DistanceTravelled == 0, "'And' operator (&) is not true.");
            Assert.False(res, "'Or' operator (|) is not true.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void OrElse_Operator_Operator_True()
        {
            var op1 = (dc < 10) % (rc);
            var op2 = (dc < 10) % (rc);
            var op3 = op1 | op2;

            var res = st.Match(op3, p);

            Assert.IsType<OrElse<Person>>(op3);
            Assert.True(p.DistanceTravelled == 4, "'And' operator (&) is not true.");
            Assert.True(res, "'And' operator (&) is not true.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void OrElse_Operator_Operator_False()
        {
            var op1 = (dc > 10) % (rc);
            var op2 = (dc > 10) % (rc);
            var op3 = op1 | op2;

            var res = st.Match(op3, p);

            Assert.IsType<OrElse<Person>>(op3);
            Assert.True(p.DistanceTravelled == 0, "'And' operator (&) is not true.");
            Assert.False(res, "'And' operator (&) is not true.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void OrElse_BooleanFunction_BooleanFunction_True()
        {
            AddAgeFunction aaf1 = new AddAgeFunction(10);
            HasAgeFunction haf1 = new HasAgeFunction(10);
            var op = (+haf1) | (+aaf1);

            var res = st.Match(op, p);

            Assert.IsType<OrElse<Person>>(op);
            Assert.True(p.Age == 10);
            Assert.True(res);
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void OrElse_BooleanFunction_BooleanFunction_False()
        {
            HasAgeFunction haf1 = new HasAgeFunction(10);
            HasAgeFunction haf2 = new HasAgeFunction(5);
            var op = (-haf1) | (-haf2);

            var res = st.Match(op, p);

            Assert.IsType<OrElse<Person>>(op);
            Assert.True(p.Age == 0);
            Assert.False(res);
        }

        //[Trait("RuleEngine", "LogicalOperators")]
        //[Fact]
        //public void OrElse_BooleanCommand_BooleanCommand_True()
        //{
        //    Operator<Person> op = +(rc + wc) | +(rc + wc);

        //    var res = st.Match(op, p);

        //    Assert.IsType<OrElse<Person>>(op);
        //    Assert.True(p.DistanceTravelled == 6);
        //    Assert.True(res);
        //}

        //[Trait("RuleEngine", "LogicalOperators")]
        //[Fact]
        //public void Or_BooleanCommand_Batch_True()
        //{
        //    FalseCommand<Person> fc = new FalseCommand<Person>();
        //    RunCommand rc = new RunCommand();
        //    WalkCommand wc = new WalkCommand();
        //    Operator<Person> op = (+fc) || (rc + wc);

        //    var res = st.Match(op, p);

        //    Assert.IsType<Or<Person>>(op);
        //    Assert.True(p.DistanceTravelled == 0);
        //    Assert.True(res);
        //}

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void And_Command_Command_False()
        {
            GetDistanceCommand gdc1 = new GetDistanceCommand();
            GetDistanceCommand gdc2 = new GetDistanceCommand();

            Operator<Person> op = (gdc1 > 10) && (gdc2 > 20);
            var res = st.Match(op, p);

            Assert.IsType<And<Person>>(op);
            Assert.False(res);
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void And_Command_Command_True()
        {
            GetDistanceCommand gdc1 = new GetDistanceCommand();
            GetDistanceCommand gdc2 = new GetDistanceCommand();
            Person p = new Person() { DistanceTravelled = 25 };

            Operator<Person> op = (gdc1 > 10) && (gdc2 > 20);
            var res = st.Match(op, p);

            Assert.IsType<And<Person>>(op);
            Assert.True(res);
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void Or_Command_Command_False()
        {
            GetDistanceCommand gdc1 = new GetDistanceCommand();
            GetDistanceCommand gdc2 = new GetDistanceCommand();

            Operator<Person> op = (gdc1 > 10) || (gdc2 > 20);
            var res = st.Match(op, p);

            Assert.IsType<Or<Person>>(op);
            Assert.False(res);
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void Or_Command_Command_True()
        {
            GetDistanceCommand gdc1 = new GetDistanceCommand();
            GetDistanceCommand gdc2 = new GetDistanceCommand();
            p.DistanceTravelled = 15;

            Operator<Person> op = (gdc1 > 10) || (gdc2 > 20);
            var res = st.Match(op, p);

            Assert.True(res);
            Assert.IsType<Or<Person>>(op);
        }

        //[Trait("RuleEngine", "LogicalOperators")]
        //[Fact]
        //public void Or_BooleanCommand_BooleanCommand_False()
        //{
        //    RunCommand rc = new RunCommand();
        //    WalkCommand wc = new WalkCommand();
        //    Operator<Person> op = (!(rc + wc)) || (!(rc + wc));

        //    var res = st.Match(op, p);

        //    Assert.IsType<Or<Person>>(op);
        //    Assert.True(p.DistanceTravelled == 6);
        //    Assert.False(res);
        //}

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void If_bool_Then_ExpressionOperator_Else_ExpressionOperator_True()
        {
            Operator<Person> op = (true) % (rc / wc);

            var res = st.Match(op, p);

            Assert.IsType<IfThenElse<Person>>(op);
            Assert.True(p.DistanceTravelled == 2);
            Assert.True(res);
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void If_bool_Then_ExpressionOperator_Else_ExpressionOperator_False()
        {
            Operator<Person> op = (false) % (rc / wc);

            var res = st.Match(op, p);

            Assert.IsType<IfThenElse<Person>>(op);
            Assert.True(p.DistanceTravelled == 1);
            Assert.False(res);
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void If_ExpressionOperator_Then_ExpressionOperator_Else_ExpressionOperator_True()
        {
            Operator<Person> op = (dc < 10) % (rc / wc);

            var res = st.Match(op, p);

            Assert.IsType<IfThenElse<Person>>(op);
            Assert.True(p.DistanceTravelled == 2);
            Assert.True(res);
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void If_ExpressionOperator_Then_ExpressionOperator_Else_ExpressionOperator_False()
        {
            Operator<Person> op = (dc > 10) % (rc / wc);

            var res = st.Match(op, p);

            Assert.IsType<IfThenElse<Person>>(op);
            Assert.True(p.DistanceTravelled == 1);
            Assert.False(res);
        }

        //[Trait("RuleEngine", "LogicalOperators")]
        //[Fact]
        //public void If_ExpressionOperator_Then_BooleanCommand_True()
        //{
        //    Operator<Person> op = (dc < 10) % (rc);

        //    var res = st.Match(op, p);

        //    Assert.IsType<IfThen<Person>>(op);
        //    Assert.Equal(2, p.DistanceTravelled);
        //    Assert.True(res);
        //}

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void If_Operator_Then_Command_True()
        {
            SleepCommand sc = new SleepCommand();
            Operator<Person> op = (dc < 10) % (sc);

            var res = st.Match(op, p);

            Assert.IsType<IfThen<Person>>(op);
            Assert.Equal(Status.Sleep, p.Status);
            Assert.True(res);
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void If_Operator_Then_Command_False()
        {
            SleepCommand sc = new SleepCommand();
            p.WakeUp();
            Operator<Person> op = (dc > 10) % (sc);
            var res = st.Match(op, p);

            Assert.IsType<IfThen<Person>>(op);
            Assert.True(p.Status == Status.WakeUp, "Incorrect status.");
            Assert.False(res, "'IfThen' operator is false.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void If_Operator_Then_Function_True()
        {
            GetAgeCommand gac = new GetAgeCommand();
            AddAgeFunction aaf = new AddAgeFunction();
            Operator<Person> op = (gac <= 0) % (aaf - 1);

            var res = st.Match(op, p);

            Assert.IsType<IfThen<Person>>(op);
            Assert.True(p.Age == 1);
            Assert.True(res);
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void If_Operator_Then_Function_False()
        {
            GetAgeCommand gac = new GetAgeCommand();
            AddAgeFunction aaf = new AddAgeFunction();
            Operator<Person> op = (gac > 10) % (aaf);

            var res = st.Match(op, p);

            Assert.IsType<IfThen<Person>>(op);
            Assert.True(p.Age == 0);
            Assert.False(res);
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void If_Bool_Then_Command_True()
        {
            SleepCommand sc = new SleepCommand();
            p.Status = Status.WakeUp;
            Operator<Person> op = (true) % (sc);

            var res = st.Match(op, p);

            Assert.IsType<IfThen<Person>>(op);
            Assert.Equal(Status.Sleep, p.Status);
            Assert.True(res);
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void If_Bool_Then_Command_False()
        {
            SleepCommand sc = new SleepCommand();
            p.Status = Status.WakeUp;
            Operator<Person> op = (false) % (sc);

            var res = st.Match(op, p);

            Assert.IsType<IfThen<Person>>(op);
            Assert.Equal(Status.WakeUp, p.Status);
            Assert.False(res);
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void If_Bool_Then_BooleanFunction_True()
        {
            AddAgeFunction aaf = new AddAgeFunction(10);
            Operator<Person> op = (true) % (aaf);

            var res = st.Match(op, p);

            Assert.IsType<IfThen<Person>>(op);
            Assert.Equal(10, p.Age);
            Assert.True(res);
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void If_Bool_Then_BooleanFunction_False()
        {
            AddAgeFunction aaf = new AddAgeFunction();
            aaf.Parameters = 10;
            Operator<Person> op = (false) % (aaf);

            var res = st.Match(op, p);

            Assert.IsType<IfThen<Person>>(op);
            Assert.Equal(0, p.Age);
            Assert.False(res);
        }

        //[Trait("RuleEngine", "LogicalOperators")]
        //[Fact]
        //public void If_Bool_Then_Command_True()
        //{
        //    op = true % (rc + wc);

        //    var res = st.Match(op, p);

        //    Assert.IsType<IfThen<Person>>(op);
        //    Assert.True(p.DistanceTravelled == 3, "Incorrect distance travelled.");
        //    Assert.True(res, "'IfThen' operator %(x)+(y) is false.");
        //}

        //[Trait("RuleEngine", "LogicalOperators")]
        //[Fact]
        //public void If_Bool_Then_Command_False()
        //{
        //    op = (false) % (rc + wc);

        //    var res = st.Match(op, p);

        //    Assert.IsType<IfThen<Person>>(op);
        //    Assert.True(p.DistanceTravelled == 0, "Incorrect distance travelled.");
        //    Assert.True(!res, "'IfThen' operator (x)%(y) is true.");
        //}

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void Loop_ExpressionOperator_Command_True()
        {
            Operator<Person> op = (dc < 10) * (rc + wc);

            var res = st.Match(op, p);

            Assert.IsType<Loop<Person>>(op);
            Assert.True(p.DistanceTravelled == 12, "Incorrect distance travelled.");
            Assert.True(res, "'Loop' operator %(x)*(y) is false.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void True_Operator()
        {
            Operator<Person> op = +(dc < 10);

            var res = st.Match(op, p);

            Assert.IsType<True<Person>>(op);
            Assert.True(res);
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void False_Operator()
        {
            Operator<Person> op = -(dc < 10);

            var res = st.Match(op, p);

            Assert.IsType<False<Person>>(op);
            Assert.False(res, "'Loop' operator %(x)*(y) is false.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void Not_Operator_True()
        {
            Operator<Person> op = !(dc > 10);

            var res = st.Match(op, p);

            Assert.IsType<Not<Person>>(op);
            Assert.True(res, "'And' operator (&) is not true.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void Not_Operator_False()
        {
            Operator<Person> op = !(dc < 10);

            var res = st.Match(op, p);

            Assert.IsType<Not<Person>>(op);
            Assert.False(res, "'And' operator (&) is not true.");
        }

        [Trait("RuleEngine", "UnaryOperators")]
        [Fact]
        public void Not_Function_True()
        {
            SetNameFunction snf = new SetNameFunction();
            var op = snf;

            var res = st.Match(op, p);

            Assert.True(res);
        }

        [Trait("RuleEngine", "UnaryOperators")]
        [Fact]
        public void Not_Function_False()
        {
            SetNameFunction snf = new SetNameFunction();
            var op = !snf;

            var res = st.Match(op, p);

            Assert.IsType<Not<Person>>(op);
            Assert.False(res);
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void Loop_ExpressionOperator_Function_True()
        {
            GetAgeCommand gac = new GetAgeCommand();
            AddAgeFunction aaf = new AddAgeFunction();
            aaf.Parameters = 1;
            Operator<Person> op = (gac < 10) * (aaf - 1);

            var res = st.Match(op, p);

            Assert.IsType<Loop<Person>>(op);
            Assert.True(p.Age == 10);
            Assert.True(res);
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void Loop_Bool_Function_True()
        {
            AddAgeFunction aaf = new AddAgeFunction();
            aaf.Parameters = 1;
            Operator<Person> op = (false) * (aaf - 1);

            var res = st.Match(op, p);

            Assert.IsType<Loop<Person>>(op);
            Assert.True(p.Age == 0);
            Assert.True(res);
        }
    }
}
