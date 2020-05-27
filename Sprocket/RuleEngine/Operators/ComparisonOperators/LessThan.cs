using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators.ComparisonOperators
{
    [DataContract]
    internal sealed class LessThan<TTarget, U> : ComparisonOperator<TTarget, U>
        where TTarget : notnull
        where U : IComparable
    {
        public LessThan(IOperand<TTarget, U> operateLeft, IOperand<TTarget, U> operateRight) : base(operateLeft, operateRight)
        {
        }

        public override bool Process(Rule<TTarget> target)
        {
            U u1 = OperateLeft.Process(target);
            U u2 = OperateRight.Process(target);
            return u1.CompareTo(u2) < 0;
        }
    }
}
