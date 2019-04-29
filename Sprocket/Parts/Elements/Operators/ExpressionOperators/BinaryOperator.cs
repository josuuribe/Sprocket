using RaraAvis.Sprocket.Parts.Interfaces;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators
{
    /// <summary>
    /// Base class that stores all binary operators.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    internal abstract class BinaryOperator<T> : ExpressionOperator<T>, ICloneable
        where T : IElement
    {
        /// <summary>
        /// Class that processes left operator.
        /// </summary>
        [DataMember]
        public virtual Operator<T> OperatorLeft { get; set; }
        /// <summary>
        /// Class that processes right operator.
        /// </summary>
        [DataMember]
        public virtual Operator<T> OperatorRight { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
