﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators
{
    [DataContract]
    internal abstract class UnaryOperator<TTarget> : Operator<TTarget>
        where TTarget : notnull
    {
        [DataMember]
        [DisallowNull]
        public virtual Operator<TTarget> Operator { get; set; }
        protected UnaryOperator()
        {
            this.Operator = new NullOperator();
        }
        protected UnaryOperator([DisallowNull]Operator<TTarget> @operator)
        {
            this.Operator = @operator;
        }
    }
}