using RaraAvis.Sprocket.Parts.Interfaces;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators
{
    /// <summary>
    /// Base class that stores all unary operators.
    /// </summary>
    /// <typeparam name="T">An IElement object.</typeparam>
    [DataContract]
    internal abstract class UnaryOperator<T> : ExpressionOperator<T>
        where T : IElement
    {
        /// <summary>
        /// Operator that processes element.
        /// </summary>
        [DataMember]
        public virtual IOperator<T> Operator { get; set; }

        public UnaryOperator()
        { }

        public UnaryOperator(IOperator<T> @operator)
        {
            this.Operator = @operator;
        }
    }
}
