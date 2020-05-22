using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Casts
{
    [DataContract]
    internal class ValueAsComparable<TElement, TValue> : Operand<TElement, IComparable>
        where TElement : IElement
    {
        [DataMember]
        public TValue Value { get; set; }

        public ValueAsComparable(TValue comparable)
        {
            this.Value = comparable;
        }

        public override IComparable Process(Rule<TElement> element)
        {
            return Value as IComparable;
        }
    }
}
