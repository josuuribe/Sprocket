using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.IterationOperators
{
    [DataContract]
    internal class Loop<T> : IterationOperator<T>
        where T : IElement
    {
        public override bool Match(RuleElement<T> element)
        {
            bool b = true;
            while (Condition.Match(element))
            {
                b &= Block.Match(element);
            }
            return b;
        }
    }
}
