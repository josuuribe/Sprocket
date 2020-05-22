using RaraAvis.Sprocket.RuleEngine.Interfaces;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators.ExpressionOperators
{
    /// <summary>
    /// Base class that stores all connective operators.
    /// </summary>
    /// <typeparam name="T">An IElement object.</typeparam>
    [DataContract]
    internal abstract class IterationOperator<T> : Operator<T>
    where T : IElement
    {
        /// <summary>
        /// If clause with IOperator to check condition.
        /// </summary>
        [DataMember]
        public Operator<T> Condition { get; set; }
        public IterationOperator() { }
    }
}
