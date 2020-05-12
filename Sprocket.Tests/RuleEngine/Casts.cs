using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Elements.Operates.Commands;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.System;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class Casts : IDisposable
    {
        private WorflowEngineTest st = null;
        private Operator<Person> op = null;

        public Casts()
        {
            st = new WorflowEngineTest();
        }

        public void Dispose()
        {
            st = null;
        }

        [Trait("Casts", "Command")]
        [Fact]
        public void Cast_Command_String()
        {
            var p = new Person() { Name = "Name" };
            var gnc = new GetNameCommand(p);

            string name = gnc;
            name = name.ToUpperInvariant();

            Assert.Equal(p.Name.ToUpperInvariant(), name);
        }

        [Trait("Casts", "Command")]
        [Fact]
        public void Cast_FalseCommand_Bool()
        {
            var p = new Person();
            var fc = new FalseCommand<Person>(p);

            Assert.False(fc);
        }

        [Trait("Casts", "Command")]
        [Fact]
        public void Cast_TrueCommand_Bool()
        {
            var p = new Person();
            var fc = new TrueCommand<Person>(p);

            Assert.True(fc);
        }

        [Trait("Casts", "Command")]
        [Fact]
        public void Cast_Command_SameValue()
        {
            var p = new Person() { Name = "Name" };
            var gnc = new GetNameCommand(p);

            var res = gnc.Value(p);

            Assert.Equal(gnc, res);
        }

        [Trait("Casts", "Command")]
        [Fact]
        public void Cast_Command_SameElement()
        {
            var p = new Person();
            var gnc = new GetNameCommand(p);

            Assert.Equal(p, gnc.Element);
        }

        [Trait("Casts", "Operate")]
        [Fact]
        public void Cast_Operate_Boolean()
        {
            Person p = new Person();
            Operand<Person, bool> gac = new RightCommand(p);
            bool b = gac;

            Assert.True(b);
        }

        [Trait("Casts", "Operate")]
        [Fact]
        public void Cast_Operate_String()
        {
            string expectedName = "Name";
            Person p = new Person() { Name = expectedName };
            Operand<Person, string> gac = new GetNameCommand(p);
            string name = gac;

            Assert.Equal(expectedName, name);
        }

        [Trait("Casts", "Operate")]
        [Fact]
        public void Cast_Operate_Int()
        {
            int expectedAge = 10;
            Person p = new Person() { Age = expectedAge };
            Operand<Person, int> gac = new GetAgeCommand(p);
            int age = gac;

            Assert.Equal(expectedAge, age);
        }
    }
}
