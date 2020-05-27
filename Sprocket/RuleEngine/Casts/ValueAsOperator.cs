using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Casts
{
    [DataContract]
    internal class ValueAsOperator<TTarget, TValue> : Operator<TTarget>
        where TTarget : notnull
    {
        [DataMember]
        public TValue ValueOperator { get; set; }

        public ValueAsOperator(TValue value)
        {
            this.ValueOperator = value;
        }

        public override bool Process(Rule<TTarget> rule)
        {
            return (ValueOperator as bool?) ?? false;
        }
    }
}
