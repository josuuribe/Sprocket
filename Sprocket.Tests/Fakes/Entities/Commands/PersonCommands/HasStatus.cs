using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    [DataContract]
    class HasStatus : Operand<Person, bool>
    {
        [DataMember]
        public Status status { get; set; }

        public HasStatus(Status status)
        {
            this.status = status;
        }

        public override bool Process(Rule<Person> element)
        {
            return element.Element.Status == status;
        }
    }
}
