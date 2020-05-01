using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.System;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class Operate
    {
        private static Person p = null;
        private static SerializeTest st = null;

        public Operate()
        {
            st = new SerializeTest();
            p = new Person();
            st.BeginSerialize();
        }

        public void Dispose()
        {
            st.EndSerialize();
        }

        [Fact]
        public void CastOperateAsBoolean()
        {
            Person p = new Person();
            Operate<Person, bool> gac = new RightCommand(p);
            bool b = gac;            

            Assert.True(b);
        }

        [Fact]
        public void CastOperateAsString()
        {
            string expectedName = "Name";
            Person p = new Person() { Name = expectedName };
            Operate<Person, string> gac = new GetNameCommand(p);
            string name = gac;

            Assert.Equal(expectedName, name);
        }

        [Fact]
        public void CastOperateAsInt()
        {
            int expectedAge = 10;
            Person p = new Person() { Age = expectedAge };
            Operate<Person, int> gac = new GetAgeCommand(p);
            int age = gac;

            Assert.Equal(expectedAge, age);
        }
    }
}
