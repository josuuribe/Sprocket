using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.IterationOperators;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions;
using RaraAvis.Sprocket.Tests.Fakes.System;
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
        public void Loop_ExpressionOperator_Command_True()
        {
            var p = new Person();
            var dc = new GetDistanceCommand();
            var rc = new RunCommand();
            var wc = new WalkCommand();
            var op = (dc < 10) * (rc / wc);

            var res = st.Match(op, p);

            Assert.IsType<Loop<Person>>(op);
            Assert.Equal(10, p.DistanceTravelled);
            Assert.True(res);
        }

        [Trait("IterationOperators", "Loop")]
        [Fact]
        public void Loop_ExpressionOperator_Function_True()
        {
            var p = new Person();
            var gac = new GetAgeCommand();
            var aaf = new AddAgeFunction();
            aaf.Parameters = 1;
            var op = (gac < 10) * (aaf - 1);

            var res = st.Match(op, p);

            Assert.IsType<Loop<Person>>(op);
            Assert.Equal(10, p.Age);
            Assert.True(res);
        }

        [Trait("IterationOperators", "Loop")]
        [Fact]
        public void Loop_Bool_Function_True()
        {
            var p = new Person();
            var aaf = new AddAgeFunction();
            aaf.Parameters = 1;
            var op = (false) * (aaf - 1);

            var res = st.Match(op, p);

            Assert.IsType<Loop<Person>>(op);
            Assert.Equal(0, p.Age);
            Assert.True(res);
        }
    }
}
