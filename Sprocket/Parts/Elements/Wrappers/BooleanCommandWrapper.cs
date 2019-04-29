using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RaraAvis.Sprocket.Parts.Elements.Wrappers
{
    [DataContract]
    internal class BooleanCommandWrapper<T> : BooleanCommand<T>
        where T : IElement
    {
        public BooleanCommandWrapper(Operator<T> operatorToWrap)
        {
            this.Operator = operatorToWrap;
        }

        [DataMember]
        public Operator<T> Operator { get; set; }

        public override bool Value(RuleElement<T> element)
        {
            return this.Operator.Match(element);
        }
    }
}
