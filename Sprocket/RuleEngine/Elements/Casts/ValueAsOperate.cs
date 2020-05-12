using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Casts
{
    [DataContract]
    internal class ValueAsOperate<TElement, TValue> : Operand<TElement, TValue>
        where TElement : IElement
    {
        [DataMember]
        public TValue ValueOperate { get; set; }

        public ValueAsOperate(TValue operateValue)
        {
            this.ValueOperate = operateValue;
        }

        public override TValue Value(TElement element)
        {
            return this.ValueOperate;
        }
    }
}
