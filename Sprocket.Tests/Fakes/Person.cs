using System;
using System.Collections.Generic;
using System.Composition;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities
{
    enum Size { Tiny, Small, Medium, Big, Huge }
    public enum worflowEngineTestatus { WakeUp, Sleep }
    [Export]
    [DataContract]
    public class Person : ICloneable
    {
        public worflowEngineTestatus worflowEngineTestatus { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public Guid Id { get; set; }
        [DataMember]
        public int Age { get; set; }

        public int DistanceTravelled { get; set; }

        public IList<Person> Family { get; set; }

        public bool Correct { get; set; }

        public bool IsHungry { get; set; }

        public Person()
        {
            this.Family = new List<Person>();
            IsHungry = true;
        }

        public void Walk()
        {
            this.DistanceTravelled++;
        }

        public void Run()
        {
            this.DistanceTravelled += 2;
        }

        public void WakeUp()
        {
            worflowEngineTestatus = worflowEngineTestatus.WakeUp;
        }

        public void Sleep()
        {
            worflowEngineTestatus = worflowEngineTestatus.Sleep;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
