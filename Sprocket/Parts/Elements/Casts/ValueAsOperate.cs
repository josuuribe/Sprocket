using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Casts
{
    [DataContract]
    internal class ValueAsOperate<TElement, TValue> : Operate<TElement, TValue>
        where TElement : IElement
    {
        [DataMember]
        public TValue ValueOperate { get; set; }

        public ValueAsOperate(TValue operateValue)
        {
            this.ValueOperate = operateValue;
        }

        protected internal override TValue Process(RuleElement<TElement> rule)
        {
            return this.ValueOperate;
        }

        //public static implicit operator ValueAsOperate<TElement, TValue>(TValue value)
        //{
        //    return new ValueAsOperate<TElement, TValue>(value);
        //}
    }
}
