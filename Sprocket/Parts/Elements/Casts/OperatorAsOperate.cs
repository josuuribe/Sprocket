using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RaraAvis.Sprocket.Parts.Elements.Casts
{
    [DataContract]
    internal class OperatorAsOperate<TElement> : Operate<TElement, bool>
        where TElement : IElement
    {
        [DataMember]
        public Operator<TElement> Operator { get; set; }

        public OperatorAsOperate(Operator<TElement> @operator)
        {
            this.Operator = @operator;
        }

        protected internal override bool Process(RuleElement<TElement> element)
        {
            return this.Operator.Match(element);
        }

        //public static implicit operator OperatorAsOperate<TElement>(Operator<TElement> @operator)
        //{
        //    return new OperatorAsOperate<TElement>(@operator);
        //}
    }
}
