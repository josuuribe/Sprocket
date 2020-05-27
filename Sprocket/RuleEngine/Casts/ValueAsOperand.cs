using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Casts
{
    [DataContract]
    internal class ValueAsOperand<TTarget, TValue> : Operand<TTarget, TValue>
        where TTarget : notnull
    {
        [DataMember]
        public TValue ValueOperate { get; set; }

        public ValueAsOperand(TValue operateValue)
        {
            this.ValueOperate = operateValue;
        }

        public override TValue Process(TTarget target)
        {
            return this.ValueOperate;
        }
    }
}
