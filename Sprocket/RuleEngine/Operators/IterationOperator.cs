using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators
{
    [DataContract]
    internal abstract class IterationOperator<TTarget> : Operator<TTarget>
        where TTarget : notnull
    {
        [DataMember]

        [NotNull]
        public Operator<TTarget> Condition { get; set; }
        protected IterationOperator(Operator<TTarget> condition)
        {
            this.Condition = condition;
        }
    }
}
