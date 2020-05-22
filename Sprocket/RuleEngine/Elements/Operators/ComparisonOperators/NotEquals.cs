using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators.ComparisonOperators
{
    [DataContract]
    internal class NotEquals<T, U> : ComparisonOperator<T, U>
        where T : IElement
    {
        public override bool Process(Rule<T> element)
        {
            var u1 = OperateLeft.Process(element);
            var u2 = OperateRight.Process(element);
            return !u1.Equals(u2);
        }
    }
}
