using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators
{
    [DataContract]
    internal abstract class Kernel<TTarget> : Operator<TTarget>
        where TTarget : notnull
    {

        [DataMember]
        
        [NotNull]
        public virtual Operator<TTarget> Operator { get; set; }

        protected Kernel(Operator<TTarget> @operator)
        {
            this.Operator = @operator;
        }
    }
}
