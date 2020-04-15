using RaraAvis.Sprocket.Parts.Elements.Functions.Kernel;
using RaraAvis.Sprocket.Parts.Elements.Wrappers;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators
{
    /// <summary>
    /// Processes an if clause with else body.
    /// </summary>
    /// <typeparam name="TElement">An IElement object.</typeparam>
    [DataContract]
    public class IfThenElse<TElement> : SelectionOperator<TElement>
        where TElement : IElement
    {
        public override bool Match(RuleElement<TElement> element)
        {
            bool b = If.Match(element);
            bool c = b ? Then.Match(element) : Else.Match(element);
            return b && c;
        }
    }
}
