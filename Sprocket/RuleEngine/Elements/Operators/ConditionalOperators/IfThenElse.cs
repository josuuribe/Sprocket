using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators.ConditionalOperators
{
    /// <summary>
    /// Processes an if clause with else body.
    /// </summary>
    /// <typeparam name="TElement">An IElement object.</typeparam>
    [DataContract]
    public class IfThenElse<TElement> : ConditionalOperator<TElement>
        where TElement : IElement
    {
        public override bool Operate(Rule<TElement> rule)
        {
            bool b = If.Operate(rule);
            bool c = b ? Then.Operate(rule) : Else.Operate(rule);
            return b && c;
        }
    }
}
