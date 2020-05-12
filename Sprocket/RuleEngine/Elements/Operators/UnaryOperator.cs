using RaraAvis.Sprocket.RuleEngine.Interfaces;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators
{
    /// <summary>
    /// Base class that stores all unary operators.
    /// </summary>
    /// <typeparam name="T">An IElement object.</typeparam>
    [DataContract]
    internal abstract class UnaryOperator<T> : Operator<T>
        where T : IElement
    {
        /// <summary>
        /// Operator that processes element.
        /// </summary>
        [DataMember]
        public virtual Operator<T> Operator { get; set; }

        public UnaryOperator() { }

        public UnaryOperator(Operator<T> @operator)
        {
            this.Operator = @operator;
        }
    }
}
