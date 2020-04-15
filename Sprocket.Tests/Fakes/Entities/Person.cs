using RaraAvis.Sprocket.Parts.Interfaces;
using System;
using System.Collections.Generic;
using System.Composition;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities
{
    enum Feature { Black, Blonde, Brown, White, Asian, Skin }
    public enum Status { WakeUp, Sleep }
    [Export]
    public class Person : IElement, ICloneable
    {
        public Status Status { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public Guid Id { get; set; }

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

        public void Drive(int miles)
        {
            DistanceTravelled += miles;
        }

        public void WakeUp()
        {
            Status = Status.WakeUp;
        }

        public void Sleep()
        {
            Status = Status.Sleep;
        }

        public void Eat()
        {
            this.IsHungry = false;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
