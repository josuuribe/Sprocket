using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators
{
    [DataContract]
    internal abstract class Kernel<TTarget> : Operator<TTarget>
        where TTarget : notnull
    {

        [DataMember]
        [DisallowNull]
        [NotNull]
        public virtual Operator<TTarget> Operator { get; set; }

        public Kernel([DisallowNull]Operator<TTarget> @operator)
        {
            this.Operator = @operator;
        }
    }
}
