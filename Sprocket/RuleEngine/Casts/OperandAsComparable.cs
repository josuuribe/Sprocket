using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Casts
{
    [DataContract]
    internal class OperandAsComparable<TTarget, TValue> : Operand<TTarget, IComparable>
        where TTarget : notnull
    {
        [DataMember]
        public Operand<TTarget, TValue> Comparable { get; set; }

        public OperandAsComparable(Operand<TTarget, TValue> comparable)
        {
            this.Comparable = comparable;
        }

        public override IComparable Process(TTarget target)
        {
            return (this.Comparable.Process(target) as IComparable)!;
        }
    }
}
