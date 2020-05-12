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
    internal class OperateAsComparable<TElement, TValue> : Operand<TElement, IComparable>
        where TElement : IElement
    {
        [DataMember]
        public Operand<TElement, TValue> Comparable { get; set; }

        public OperateAsComparable(Operand<TElement, TValue> comparable)
        {
            this.Comparable = comparable;
        }

        public override IComparable Value(TElement element)
        {
            this.element = element;
            return this.Comparable.Value(element) as IComparable;
        }
    }
}
