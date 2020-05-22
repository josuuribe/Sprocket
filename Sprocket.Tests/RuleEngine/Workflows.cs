using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Elements.Flows;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.System;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace RaraAvis.Sprocket.Tests.EngineEngine
{
    public class Engines : IDisposable
    {
        private WorflowEngineTest st = null;
        private Operator<Person> op = null;

        public Engines()
        {
            st = new WorflowEngineTest();
        }

        public void Dispose()
        {
            st = null;
        }

        [Trait("Engine", "Positive")]
        [Fact]
        public void OneStageEngine_Positive()
        {
            GetDistanceCommand dc = new GetDistanceCommand();
            Operator<Person> op = (dc < 10);
            Person p = new Person();

            var result = st.Start(op, p);

            Assert.Equal(ExecutionResult.Positive, result.ExecutionResult);
        }

        [Trait("Engine", "Positive")]
        [Fact]
        public void OneStageEngine_Negative()
        {
            GetDistanceCommand dc = new GetDistanceCommand();
            Operator<Person> op = (dc > 10);
            Person p = new Person();

            var result = st.Start(op, p);

            Assert.Equal(ExecutionResult.Negative, result.ExecutionResult);
        }

        [Trait("Engine", "Error")]
        [Fact]
        public void OneStageEngine_Error()
        {
            Guid id1 = Guid.NewGuid();
            IsFamilyCommand ifc = new IsFamilyCommand();
            Person p = new Person();
            p.Family = null;
            op = (ifc);

            var result = st.Start(op, p);

            Assert.Equal(ExecutionResult.Error, result.ExecutionResult);
        }

        [Trait("Engine", "Exception")]
        [Fact]
        public void OneStageEngine_WrongAssemblyPath()
        {
            Guid id1 = Guid.NewGuid();
            IsFamilyCommand ifc = new IsFamilyCommand();
            Person p = new Person();
            p.Family = null;

            Operator<Person> op = (ifc);            
        }

        [Trait("Engine", "Exception")]
        [Fact]
        public void Function_Parameter_True()
        {
            var p = new Person();
            SetParameter sp = new SetParameter("10");
            Operator<Person> op = sp;

            var result = st.Start(op, p);

            Assert.Equal("10", result.Parameters["id"]);
            Assert.Equal(ExecutionResult.Positive, result.ExecutionResult);
        }

        [Trait("Engine", "Exception")]
        [Fact]
        public void Function_Parameter_False()
        {
            var p = new Person();
            SetParameter sp = new SetParameter("10");
            Operator<Person> op = sp;

            var result = st.Start(-op, p);

            Assert.Equal("10", result.Parameters["id"]);
            Assert.Equal(ExecutionResult.Negative, result.ExecutionResult);
        }

        [Trait("Engine", "Exception")]
        [Fact]
        public void Begin()
        {
            var p = new Person();
            var begin = new Begin<Person>();

            bool b = begin.Process(p);

            Assert.True(b);
            Assert.Null(begin.Next);
            Assert.Equal(begin, begin.Previous);
        }

        [Trait("Engine", "Exception")]
        [Fact]
        public void End()
        {
            var p = new Person();
            var end = new End<Person>();

            bool b = end.Process(p);

            Assert.True(b);
        }
    }
}
