using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators.ConditionalOperators
{
    /// <summary>
    /// Processes an if clause.
    /// </summary>
    /// <typeparam name="T">An IElement object.</typeparam>
    [DataContract]
    public class IfThen<T> : ConditionalOperator<T>
        where T : IElement
    {
        public override bool Operate(Rule<T> element)
        {
            return If.Operate(element) ? Then.Operate(element) : false;
        }
    }
}
