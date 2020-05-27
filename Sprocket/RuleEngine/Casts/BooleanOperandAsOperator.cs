using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Casts
{
    [DataContract]
    internal class BooleanOperandAsOperator<TTarget> : Operator<TTarget>
        where TTarget : notnull
    {
        [DataMember]
        public Operand<TTarget, bool> Operand { get; set; }

        public BooleanOperandAsOperator(Operand<TTarget, bool> operate) : base()
        {
            this.Operand = operate;
        }

        public override bool Process(Rule<TTarget> rule)
        {
            var next = Operand;
            var res = true;
            do
            {
                res &= next.Process(rule);
                next = next.Next;
            } while (!(next is Operand<TTarget, bool>.Noop));
            return res;
        }
    }
}
