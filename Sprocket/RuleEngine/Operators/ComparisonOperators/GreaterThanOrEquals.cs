using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators.ComparisonOperators
{
    [DataContract]
    internal sealed class GreaterThanOrEquals<T, U> : ComparisonOperator<T, U>
        where T : notnull
        where U : IComparable
    {
        public GreaterThanOrEquals(IOperand<T, U> operateLeft, IOperand<T, U> operateRight) : base(operateLeft, operateRight)
        {
        }

        public override bool Process(Rule<T> element)
        {
            var u1 = OperateLeft.Process(element);
            var u2 = OperateRight.Process(element);
            return u1.CompareTo((U)u2) >= 0;
        }
    }
}
