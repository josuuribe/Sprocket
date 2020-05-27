using RaraAvis.Sprocket.RuleEngine;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.SyworflowEngineTestem;
using Xunit;

namespace RaraAvis.Sprocket.Tests.EngineEngine
{
    public class Engines : IClassFixture<WorkflowEngineTest>
    {
        private readonly WorkflowEngineTest workflowEngineTest = null;

        public Engines(WorkflowEngineTest worflowEngineTest)
        {
            this.workflowEngineTest = worflowEngineTest;
        }

        [Trait("Engine", "Positive")]
        [Fact]
        public void One_WorflowEngine_Positive()
        {
            GetDistanceCommand dc = new GetDistanceCommand();
            Operator<Person> op = (dc < 10);
            Person p = new Person();

            var result = workflowEngineTest.Start(op, p);

            Assert.Equal(ExecutionResult.Positive, result.ExecutionResult);
        }

        [Trait("Engine", "Positive")]
        [Fact]
        public void One_WorflowEngine_Negative()
        {
            GetDistanceCommand dc = new GetDistanceCommand();
            Operator<Person> op = (dc > 10);
            Person p = new Person();

            var result = workflowEngineTest.Start(op, p);

            Assert.Equal(ExecutionResult.Negative, result.ExecutionResult);
        }

        [Trait("Engine", "Error")]
        [Fact]
        public void One_WorflowEngine_Error()
        {
            IsFamilyCommand ifc = new IsFamilyCommand();
            Person p = new Person
            {
                Family = null
            };
            Operator<Person> op = (ifc);

            var result = workflowEngineTest.Start(op, p);

            Assert.Equal(ExecutionResult.Error, result.ExecutionResult);
        }

        [Trait("Engine", "Exception")]
        [Fact]
        public void One_WorflowEngine_NullOperator()
        {
            Person p = new Person();
            Operator<Person>.NullOperator nullOperator = new Operator<Person>.NullOperator();

            var res = workflowEngineTest.MatchNullSerializer(nullOperator, p);

            Assert.False(res);
        }
    }
}
