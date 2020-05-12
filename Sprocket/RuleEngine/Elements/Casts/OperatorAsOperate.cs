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
    internal class OperatorAsOperate<TElement> : Operand<TElement, bool>
        where TElement : IElement
    {
        [DataMember]
        public Operator<TElement> Operator { get; set; }

        public OperatorAsOperate(Operator<TElement> @operator)
        {
            this.Operator = @operator;
        }

        public override bool Value(TElement element)
        {
            return this.Operator.Operate(element);
        }

        //public static implicit operator OperatorAsOperate<TElement>(Operator<TElement> @operator)
        //{
        //    return new OperatorAsOperate<TElement>(@operator);
        //}
    }
}
