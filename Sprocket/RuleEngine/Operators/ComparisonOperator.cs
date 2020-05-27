using RaraAvis.Sprocket.RuleEngine.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators
{
    [DataContract]
    internal abstract class ComparisonOperator<TTarget, TValue> : Operator<TTarget>
        where TTarget : notnull
    {
        [DataMember]
        [DisallowNull]
        [NotNull]
        public IOperand<TTarget, TValue> OperateLeft;
        [DataMember]
        [DisallowNull]
        [NotNull]
        public IOperand<TTarget, TValue> OperateRight;

        protected ComparisonOperator([DisallowNull]IOperand<TTarget, TValue> operateLeft, [DisallowNull] IOperand<TTarget, TValue> operateRight)
        {
            this.OperateLeft = operateLeft;
            this.OperateRight = operateRight;
        }
    }
}
