using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators.ComparisonOperators
{
    [DataContract]
    internal sealed class NotEquals<TTarget, U> : ComparisonOperator<TTarget, U>
        where TTarget : notnull
    {
        public NotEquals(IOperand<TTarget, U> operateLeft, IOperand<TTarget, U> operateRight) : base(operateLeft, operateRight)
        {
        }

        public override bool Process(Rule<TTarget> element)
        {
            var u1 = OperateLeft.Process(element);
            var u2 = OperateRight.Process(element);
            return !u1!.Equals(u2);
        }
    }
}
