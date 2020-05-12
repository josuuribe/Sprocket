using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.System;
using RaraAvis.Sprocket.WorkflowEngine.Entities.Enums;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace RaraAvis.Sprocket.Tests.WorkflowEngine
{
    public class Workflows : IDisposable
    {
        private WorflowEngineTest st = null;
        private Operator<Person> op = null;


        public Workflows()
        {
            st = new WorflowEngineTest();
        }

        public void Dispose()
        {
            st = null;
        }

        [Trait("Workflow", "Positive")]
        [Fact]
        public void OneStageWorkflow_Correct()
        {
            GetDistanceCommand dc = new GetDistanceCommand();
            Operator<Person> op = (dc < 10);
            Person p = new Person();

            var result = st.Start(op, p);

            Assert.Equal(ExecutionEngineResult.Correct, result.ExecutionResult);
        }

        [Trait("Workflow", "Negative")]
        [Fact]
        public void OneStageWorkflow_Wrong()
        {
            Guid id1 = Guid.NewGuid();
            GetDistanceCommand dc = new GetDistanceCommand();
            Person p = new Person();
            op = (dc > 10);

            var result = st.Start(op, p);

            Assert.Equal(ExecutionEngineResult.Error, result.ExecutionResult);
        }

        [Trait("Workflow", "Error")]
        [Fact]
        public void OneStageWorkflow_Error()
        {
            Guid id1 = Guid.NewGuid();
            IsFamilyCommand ifc = new IsFamilyCommand();
            Person p = new Person();
            p.Family = null;
            op = (ifc);

            var result = st.Start(op, p);

            Assert.Equal(ExecutionEngineResult.Error, result.ExecutionResult);
        }

        [Trait("Workflow", "Exception")]
        [Fact]
        public void OneStageWorkflow_WrongAssemblyPath()
        {
            Guid id1 = Guid.NewGuid();
            IsFamilyCommand ifc = new IsFamilyCommand();
            Person p = new Person();
            p.Family = null;

            Operator<Person> op = (ifc);            
        }
    }
}
