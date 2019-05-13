using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands;
using RaraAvis.Sprocket.Tests.Fakes.System;
using RaraAvis.Sprocket.WorkflowEngine;
using Xunit;

namespace RaraAvis.Sprocket.Tests.RuleEngine
{
    public class LogicalOperators
    {
        private static RunCommand rc = null;
        private static WalkCommand wc = null;
        private static RuleElement<Person> re = null;
        private static DistanceCommand dc = null;
        private static SerializeTest st = null;
        private static Operator<Person> op = null;
        private static Person p = null;
        private static FalseCommand fc = null;


        public LogicalOperators()
        {
            rc = new RunCommand();
            wc = new WalkCommand();
            dc = new DistanceCommand();
            re = new RuleElement<Person>();
            st = new SerializeTest();
            fc = new FalseCommand();

            p = new Person();
            st.BeginSerialize();
        }

        public void Dispose()
        {
            st.EndSerialize();
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void AndAlsoATrue()
        {
            op = (rc + wc) & (rc + wc);

            var res = st.Match(op, p);

            Assert.True(p.DistanceTravelled == 6 && res, "'And' operator (&) is not true.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void AndAlsoFalse()
        {
            op = (rc + wc) & !(rc + wc);

            var res = st.Match(op, p);

            Assert.True(p.DistanceTravelled == 6 && !res, "'And' operator (&) is not false.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void AndTrue()
        {
            op = (rc + wc) && (rc + wc);

            var res = st.Match(op, p);

            Assert.True(p.DistanceTravelled == 6 && res, "'And' operator (&&) is false.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void AndFalse()
        {
            op = !(rc + wc) && (rc + wc);

            var res = st.Match(op, p);

            Assert.True(p.DistanceTravelled == 3 && !res, "'And' operator (&&) is true.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void OrElseTrue()
        {
            op = (rc + wc) | (rc + wc);

            var res = st.Match(op, p);

            Assert.True(p.DistanceTravelled == 6 && res, "'Or' operator (|) is true.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void OrTrue()
        {
            op = (rc + wc) || (rc + wc);

            var res = st.Match(op, p);

            Assert.True(p.DistanceTravelled == 3 && res, "'Or' operator (||) is true.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void OrFalse()
        {
            op = !(rc + wc) || !(rc + wc);

            var res = st.Match(op, p);

            Assert.True(p.DistanceTravelled == 6 && !res, "'Or' operator (||) is false.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void OperatorIfThenElseTrue()
        {
            op = (dc < 10) % (rc);

            var res = st.Match(op, p);

            Assert.True(p.DistanceTravelled == 2, "Incorrect distance travelled.");
            Assert.True(res, "'IfThenElse' operator %(x)+(y-z) is false.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void IfThenElseTrue()
        {
            op = (true) % ((rc + wc) / fc);

            var res = st.Match(op, p);

            Assert.True(p.DistanceTravelled == 3, "Incorrect distance travelled.");
            Assert.True(res, "'IfThenElse' operator %(x)+(y-z) is false.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void IfThenElseFalse()
        {
            op = (false) % ((rc + wc) / (rc + wc));

            var res = st.Match(op, p);

            Assert.True(p.DistanceTravelled == 3, "Incorrect distance travelled.");
            Assert.False(res, "'IfThenElse' operator %(x)+(y-z) is true.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void IfThenTrue()
        {
            op = (true) % ((rc + wc));

            var res = st.Match(op, p);

            Assert.True(p.DistanceTravelled == 3, "Incorrect distance travelled.");
            Assert.True(res, "'IfThen' operator %(x)+(y) is false.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void IfThenFalse()
        {
            op = (false) % ((rc + wc));

            var res = st.Match(op, p);

            Assert.True(p.DistanceTravelled == 0, "Incorrect distance travelled.");
            Assert.True(!res, "'IfThen' operator (x)%(y) is true.");
        }

        [Trait("RuleEngine", "LogicalOperators")]
        [Fact]
        public void LoopTrue()
        {
            op = (dc < 10) * ((rc + wc));

            var res = st.Match(op, p);

            Assert.True(p.DistanceTravelled == 12, "Incorrect distance travelled.");
            Assert.True(res, "'Loop' operator %(x)*(y) is false.");
        }
    }
}
