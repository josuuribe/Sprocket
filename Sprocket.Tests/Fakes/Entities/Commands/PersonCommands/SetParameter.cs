using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    [DataContract]
    class SetParameter : Operand<Person, bool>
    {
        [DataMember]
        public object Parameter { get; set; }
        public SetParameter(object parameter)
        {
            this.Parameter = parameter;
        }

        public override bool Process(Rule<Person> element)
        {
            element.Parameters.Add("id", Parameter);
            return true;
        }
    }
}
