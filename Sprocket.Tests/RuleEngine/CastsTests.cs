using RaraAvis.Sprocket.RuleEngine;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.PersonCommands;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class CastTests
    {
        [Trait("Cast", "Operate")]
        [Fact]
        public void Cast_Operand_String()
        {
            var p = new Person() { Name = "Name" };
            var gnc = new GetNameCommand(p);

            string name = gnc;
            name = name.ToUpperInvariant();

            Assert.Equal(p.Name.ToUpperInvariant(), name);
        }

        [Trait("Cast", "Operate")]
        [Fact]
        public void Cast_Operand_SameValue()
        {
            var p = new Person() { Name = "Name" };
            var gnc = new GetNameCommand(p);

            var res = gnc.Process(p);

            Assert.Equal(gnc, res);
        }

        [Trait("Cast", "Operate")]
        [Fact]
        public void Cast_Operate_Boolean()
        {
            Person p = new Person();
            Operand<Person, bool> gac = new RightCommand(p);
            bool b = gac;

            Assert.True(b);
        }

        [Trait("Cast", "Operate")]
        [Fact]
        public void Cast_Operate_String()
        {
            string expectedName = "Name";
            Person p = new Person() { Name = expectedName };
            Operand<Person, string> gac = new GetNameCommand(p);
            string name = gac;

            Assert.Equal(expectedName, name);
        }

        [Trait("Cast", "Operate")]
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
