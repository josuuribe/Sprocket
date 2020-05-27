using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators.Kernel
{
    [DataContract]
    internal sealed class RemoveFlag<TTarget> : Kernel<TTarget>
        where TTarget : notnull
    {
        [DataMember]
        public int Flag { get; set; }

        public RemoveFlag(Operator<TTarget> @operator, int flag) : base(@operator)
        {
            this.Flag = flag;
        }

        public override bool Process(Rule<TTarget> rule)
        {
            bool b = this.Operator.Process(rule.Target);
            rule.UserStatus = b ? rule.UserStatus & ~Flag : rule.UserStatus;
            return b;
        }
    }
}
