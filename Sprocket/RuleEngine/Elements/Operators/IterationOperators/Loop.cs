using RaraAvis.Sprocket.RuleEngine.Elements.Operators.ExpressionOperators;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators.IterationOperators
{
    [DataContract]
    internal class Loop<T> : IterationOperator<T>
        where T : IElement
    {
        public override bool Operate(Rule<T> element)
        {
            bool b = true;
            while (Condition.Operate(element))
            {
                b &= Block.Operate(element);
            }
            return b;
        }
    }
}
