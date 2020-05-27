using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators
{
    [DataContract]
    internal abstract class BinaryOperator<TTarget> : Operator<TTarget>, ICloneable
        where TTarget : notnull
    {
        [DataMember]
        [DisallowNull]
        [NotNull]
        public virtual Operator<TTarget> OperatorLeft { get; set; }
        [DataMember]
        [DisallowNull]
        [NotNull]
        public virtual Operator<TTarget> OperatorRight { get; set; }

        public BinaryOperator()
        {
            this.OperatorLeft = new Operator<TTarget>.NullOperator();
            this.OperatorRight = new Operator<TTarget>.NullOperator();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
