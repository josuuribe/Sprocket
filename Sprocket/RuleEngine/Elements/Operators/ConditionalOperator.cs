using RaraAvis.Sprocket.RuleEngine.Interfaces;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators
{
    /// <summary>
    /// Base class that stores all connective operators.
    /// </summary>
    /// <typeparam name="TElement">An IElement object.</typeparam>
    [DataContract]
    public abstract class ConditionalOperator<TElement> : Operator<TElement>
    where TElement : IElement
    {
        /// <summary>
        /// If clause with IOperator to check condition.
        /// </summary>
        [DataMember]
        public Operator<TElement> If { get; set; }
        /// <summary>
        /// Then clause to process in case true.
        /// </summary>
        [DataMember]
        public Operator<TElement> Then { get; set; }
        /// <summary>
        /// Else clause to execute in case false. 
        /// </summary>
        [DataMember]
        public Operator<TElement> Else { get; set; }
    }
}
