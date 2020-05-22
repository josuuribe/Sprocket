using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators.ComparisonOperators
{
    [DataContract]
    internal class LessThan<T, U> : ComparisonOperator<T, U>
        where T : IElement
        where U : IComparable
    {
        public override bool Process(Rule<T> element)
        {
            U u1 = OperateLeft.Process(element);
            U u2 = OperateRight.Process(element);
            return u1.CompareTo(u2) < 0;
        }
    }
}
