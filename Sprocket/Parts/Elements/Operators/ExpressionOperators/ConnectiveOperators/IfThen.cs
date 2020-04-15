using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators
{
    /// <summary>
    /// Processes an if clause.
    /// </summary>
    /// <typeparam name="T">An IElement object.</typeparam>
    [DataContract]
    public class IfThen<T> : SelectionOperator<T>
        where T : IElement
    {

        public override bool Match(RuleElement<T> element)
        {
            return If.Match(element) ? Then.Match(element) : false;
        }
    }
}
