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
    internal class ValueAsOperator<TElement, TValue> : Operator<TElement>
        where TElement : IElement
    {
        [DataMember]
        public TValue ValueOperator { get; set; }

        public ValueAsOperator(TValue value)
        {
            this.ValueOperator = value;
        }

        public override bool Operate(Rule<TElement> rule)
        {
            return (ValueOperator as bool?) ?? false;
        }

        //public static implicit operator ValueAsOperator<TElement, TValue>(TValue value)
        //{
        //    return new ValueAsOperator<TElement, TValue>(value);
        //}
    }
}
