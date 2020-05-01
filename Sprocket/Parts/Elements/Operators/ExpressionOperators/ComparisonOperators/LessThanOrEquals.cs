using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ComparisonOperators
{
    [DataContract]
    internal class LessThanOrEquals<T, U> : ComparisonOperator<T, U>
        where T : IElement
        where U : IComparable<U>
    {
        public override bool Match(RuleElement<T> element)
        {
            U u1 = OperateLeft.Process(element);
            U u2 = OperateRight.Process(element);
            return u1.CompareTo(u2) <= 0;
        }
    }
}
