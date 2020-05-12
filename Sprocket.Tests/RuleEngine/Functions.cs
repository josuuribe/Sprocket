using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions;
using RaraAvis.Sprocket.Tests.Fakes.System;
using RaraAvis.Sprocket.WorkflowEngine.Entities.Enums;
using System;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class Functions
    {
        private WorflowEngineTest st = null;
        private Operator<Person> op = null;

        public Functions()
        {
            st = new WorflowEngineTest();
        }

        public void Dispose()
        {
            st = null;
        }

        [Trait("Functions", "Parameters")]
        [Fact]
        public void FunctionWithParameters_Equals_True()
        {
            var p = new Person();
            var son1 = new Person();
            var son2 = new Person();
            son2.Name = "Son2Name";
            p.Family.Add(son1);
            p.Family.Add(son2);
            var sc = new SonsFunction();
            var gn = new GetNameFunction(p);
            op = (gn - (sc - 1) == son2.Name);

            var res = st.Start(op, p);

            Assert.Equal(ExecutionEngineResult.Correct, res.ExecutionResult);
        }

        [Trait("Functions", "Parameters")]
        [Fact]
        public void FunctionWithParameters_Equals_False()
        {
            var p = new Person();
            var son1 = new Person();
            var son2 = new Person();
            son2.Name = "Son2Name";
            p.Family.Add(son1);
            p.Family.Add(son2);
            var sc = new SonsFunction();
            var gn = new GetNameFunction(p);
            op = (gn - (sc - 1) != son2.Name);

            var res = st.Start(op, p);

            Assert.Equal(ExecutionEngineResult.Correct, res.ExecutionResult);
        }

        [Trait("Functions", "Parameters")]
        [Fact]
        public void FunctionWithParameters_NotEquals_True()
        {
            var p = new Person();
            var son1 = new Person();
            var son2 = new Person();
            son2.Name = "Son2";
            son2.Name = "Son2Name";
            p.Family.Add(son1);
            p.Family.Add(son2);
            var sc = new SonsFunction();
            var gn = new GetNameFunction(p);
            op = (gn - (sc - 1) != "Get:");

            var res = st.Start(op, p);

            Assert.Equal(ExecutionEngineResult.Correct, res.ExecutionResult);
        }

        [Trait("Functions", "Parameters")]
        [Fact]
        public void FunctionWithParameters_NotEquals_False()
        {
            var p = new Person();
            var son1 = new Person();
            var son2 = new Person();
            son2.Name = "Son2";
            son2.Name = "Son2Name";
            p.Family.Add(son1);
            p.Family.Add(son2);
            var sc = new SonsFunction();
            var gn = new GetNameFunction(p);
            op = (gn - (sc - 1) != "Son2Name");

            var res = st.Start(op, p);

            Assert.Equal(ExecutionEngineResult.Correct, res.ExecutionResult);
        }
    }
}
