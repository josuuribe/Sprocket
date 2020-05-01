using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions;
using RaraAvis.Sprocket.Tests.Fakes.System;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class Functions : IDisposable
    {
        private static Person p = null;
        private static RuleElement<Person> re = null;
        private static Operator<Person> op = null;
        private static SerializeTest st = null;


        public Functions()
        {
            st = new SerializeTest();
            p = new Person();
            st.BeginSerialize();
        }

        [Trait("RuleEngine", "Functions")]
        [Fact]
        public static void Function_Equals_True()
        {
            string name = "Son";
            Person p = new Person() { Name = "Name" };
            p.Family.Add(new Person() { Name = name });
            SonsFunction sf = new SonsFunction(p, 0);
            GetNameFunction gnf = new GetNameFunction(p, sf);

            var res = gnf.Value;

            Assert.Equal(name, gnf.Value);
        }

        [Trait("RuleEngine", "Functions")]
        [Fact]
        public static void Function_Equals_False()
        {
            int distanceTravelled = 20;
            Person p = new Person() { DistanceTravelled = 20 };
            DistanceRemainingFunction drf = new DistanceRemainingFunction(p, 40);

            var res = drf.Value;

            Assert.Equal(distanceTravelled, res);
        }

        public void Dispose()
        {
            st.EndSerialize();
        }
    }
}
