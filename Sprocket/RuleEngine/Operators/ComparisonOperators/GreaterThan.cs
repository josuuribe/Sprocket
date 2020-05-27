using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators.ComparisonOperators
{
    [DataContract]
    internal sealed class GreaterThan<T, U> : ComparisonOperator<T, U>
        where T : notnull
        where U : IComparable
    {
        public GreaterThan([DisallowNull] IOperand<T, U> operateLeft, [DisallowNull] IOperand<T, U> operateRight) : base(operateLeft, operateRight)
        {
        }

        public override bool Process(Rule<T> element)
        {
            U u1 = OperateLeft.Process(element);
            U u2 = OperateRight.Process(element);
            return u1.CompareTo(u2) > 0;
        }
    }
}
