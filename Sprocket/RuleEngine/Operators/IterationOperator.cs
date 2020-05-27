using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators
{
    [DataContract]
    internal abstract class IterationOperator<TTarget> : Operator<TTarget>
        where TTarget : notnull
    {
        [DataMember]
        [DisallowNull]
        [NotNull]
        public Operator<TTarget> Condition { get; set; }
        public IterationOperator([DisallowNull]Operator<TTarget> condition)
        {
            this.Condition = condition;
        }
    }
}
