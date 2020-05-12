using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators.ComparisonOperators
{
    [DataContract]
    internal class LessThanOrEquals<T, U> : ComparisonOperator<T, U>
        where T : IElement
        where U : IComparable
    {
        public override bool Operate(Rule<T> element)
        {
            U u1 = OperateLeft.Value(element.Element);
            U u2 = OperateRight.Value(element.Element);
            return u1.CompareTo(u2) <= 0;
        }
    }
}
