using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators;
using RaraAvis.Sprocket.Parts.Interfaces;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators
{
    /// <summary>
    /// Base class that stores all connective operators.
    /// </summary>
    /// <typeparam name="TElement">An IElement object.</typeparam>
    [DataContract]
    public abstract class SelectionOperator<TElement> : ExpressionOperator<TElement>
    where TElement : IElement
    {
        /// <summary>
        /// If clause with IOperator to check condition.
        /// </summary>
        [DataMember]
        public IOperator<TElement> If { get; set; }
        /// <summary>
        /// Then clause to process in case true.
        /// </summary>
        [DataMember]
        public IOperator<TElement> Then { get; set; }
        /// <summary>
        /// Else clause to execute in case false. 
        /// </summary>
        [DataMember]
        public IOperator<TElement> Else { get; set; }
        public SelectionOperator() { }
    }
}
