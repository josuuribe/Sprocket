using Newtonsoft.Json;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators.Kernel
{
    [DataContract]
    internal sealed class Jump<TTarget> : Kernel<TTarget>
        where TTarget : notnull
    {
        [DataMember]
        public (IOperand<TTarget, bool>, IOperand<TTarget, bool>) Target { get; set; }

        [JsonConstructor]
        public Jump(Operator<TTarget> condition) : base(condition)
        {
        }

        public Jump(Operator<TTarget> condition, IOperand<TTarget, bool> target) : this(condition)
        {
            Target = (target, new Operand<TTarget, bool>.Noop());
        }

        public Jump(Operator<TTarget> condition, (IOperand<TTarget, bool>, IOperand<TTarget, bool>) target) : this(condition)
        {
            Target = target;
        }

        public override bool Process(Rule<TTarget> rule)
        {
            bool b = this.Operator.Process(rule.Target);
            var next = b ? this.Target.Item1 : this.Target.Item2;
            next.Process(rule);
            return b;
        }
    }
}
