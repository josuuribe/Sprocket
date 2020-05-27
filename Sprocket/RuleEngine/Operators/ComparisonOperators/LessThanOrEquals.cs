using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators.ComparisonOperators
{
    [DataContract]
    internal sealed class LessThanOrEquals<T, U> : ComparisonOperator<T, U>
        where T : notnull
        where U : IComparable
    {
        public LessThanOrEquals(IOperand<T, U> operateLeft, IOperand<T, U> operateRight) : base(operateLeft, operateRight)
        {
        }

        public override bool Process(Rule<T> element)
        {
            var u1 = OperateLeft.Process(element.Target);
            var u2 = OperateRight.Process(element.Target);
            return u1.CompareTo((U)u2) <= 0;
        }
    }
}
