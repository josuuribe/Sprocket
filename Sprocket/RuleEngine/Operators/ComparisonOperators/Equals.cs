using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators.ComparisonOperators
{
    [DataContract]
    internal sealed class Equals<TTarget, U> : ComparisonOperator<TTarget, U>
        where TTarget : notnull
    {
        public Equals(IOperand<TTarget, U> operateLeft, IOperand<TTarget, U> operateRight) : base(operateLeft, operateRight)
        {
        }

        public override bool Process(Rule<TTarget> target)
        {
            var u1 = OperateLeft.Process(target);
            var u2 = OperateRight.Process(target);
            return u1!.Equals(u2);
        }
    }
}
