using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators.Kernel
{
    [DataContract]
    internal sealed class Break<TTarget> : Kernel<TTarget>
        where TTarget : notnull
    {
        public Break(Operator<TTarget> @operator) : base(@operator)
        {
        }

        public override bool Process(Rule<TTarget> rule)
        {
            bool b = this.Operator.Process(rule.Target);
            rule.ExecutionResult = b ? ExecutionResult.Exit : rule.ExecutionResult;
            return b;
        }
    }
}
