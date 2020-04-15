using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Functions.PersonFunctions;
using RaraAvis.Sprocket.Tests.Fakes.System;
using RaraAvis.Sprocket.WorkflowEngine;
using System;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class Functions : IDisposable
    {
        private static Person p = null;
        private static RuleElement<Person> re = null;
        private static DistanceRemainingFunction drf = null;
        private static Operator<Person> op = null;
        private static SerializeTest st = null;


        public Functions()
        {
            drf = new DistanceRemainingFunction();
            st = new SerializeTest();
            p = new Person();
            st.BeginSerialize();
        }

        public void Dispose()
        {
            st.EndSerialize();
        }
    }
}
