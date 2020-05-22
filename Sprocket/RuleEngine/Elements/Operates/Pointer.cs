using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operates
{
    [DataContract]
    public class Pointer<TElement, TValue> : Operand<TElement, TValue>
        where TElement : IElement
    {
        [DataMember]
        Expression<Func<TElement, TValue>> Expression { get; set; }

        public Pointer()
        {

        }

        public override TValue Process(Rule<TElement> element)
        {
            return Expression.Compile()(element);
        }
    }
}
