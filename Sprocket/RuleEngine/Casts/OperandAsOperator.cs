using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Casts
{
    [DataContract]
    internal class OperandAsOperator<TTarget, TValue> : Operator<TTarget>
        where TTarget : notnull
    {
        [DataMember]
        public IOperand<TTarget, TValue> Operand { get; set; }

        public OperandAsOperator(Operand<TTarget, TValue> operate) : base()
        {
            this.Operand = operate;
        }

        public override bool Process(Rule<TTarget> rule)
        {
            Operand.Process(rule);
            return true;
        }
    }
}
