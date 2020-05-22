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
        public override bool Process(Rule<T> element)
        {
            var u1 = OperateLeft.Process(element.Element);
            var u2 = OperateRight.Process(element.Element);
            return u1.CompareTo((U)u2) <= 0;
        }
    }
}
