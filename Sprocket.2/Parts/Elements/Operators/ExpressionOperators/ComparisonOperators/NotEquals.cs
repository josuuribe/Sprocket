using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ComparisonOperators
{
    [DataContract]
    internal class NotEquals<T, U> : ComparisonOperator<T, U>
        where T : IElement
    {
        public override bool Match(RuleElement<T> element)
        {
            U u1 = OperateLeft.Value(element);
            U u2 = OperateRight.Value(element);
            return !u1.Equals(u2);
        }
    }
}
