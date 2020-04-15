using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators;
using RaraAvis.Sprocket.Parts.Interfaces;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators
{
    /// <summary>
    /// Base class that stores all connective operators.
    /// </summary>
    /// <typeparam name="T">An IElement object.</typeparam>
    [DataContract]
    internal abstract class IterationOperator<T> : ExpressionOperator<T>
    where T : IElement
    {
        /// <summary>
        /// If clause with IOperator to check condition.
        /// </summary>
        [DataMember]
        public IOperator<T> Condition { get; set; }
        /// <summary>
        /// Then clause to process in case true.
        /// </summary>
        [DataMember]
        public IOperator<T> Block { get; set; }
        public IterationOperator() { }
    }
}
