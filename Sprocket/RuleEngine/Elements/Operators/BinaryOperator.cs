using RaraAvis.Sprocket.RuleEngine.Interfaces;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators
{
    /// <summary>
    /// Base class that stores all binary operators.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    public abstract class BinaryOperator<T> : Operator<T>, ICloneable
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
