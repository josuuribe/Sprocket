﻿using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Casts
{
    [DataContract]
    internal class ValueAsComparable<TTarget, TValue> : Operand<TTarget, IComparable>
        where TTarget : notnull
    {
        [DataMember]
        public TValue Value { get; set; }

        public ValueAsComparable(TValue comparable)
        {
            this.Value = comparable;
        }

        public override IComparable Process(TTarget target)
        {
            return (Value as IComparable)!;
        }
    }
}
