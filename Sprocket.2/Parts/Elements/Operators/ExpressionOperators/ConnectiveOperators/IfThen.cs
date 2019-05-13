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
    internal class IfThen<T> : ConnectiveOperator<T>
        where T : IElement
    {
        /// <summary>
        /// If clause with IOperator to check condition.
        /// </summary>
        [DataMember]
        public IOperator<T> If { get; set; }
        /// <summary>
        /// Then clause to process in case true.
        /// </summary>
        [DataMember]
        public IOperator<T> Then { get; set; }

        public override bool Match(RuleElement<T> element)
        {
            return If.Match(element) ? Then.Match(element) : false;
        }
    }
}
