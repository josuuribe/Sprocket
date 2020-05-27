using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators.Kernel
{
    [DataContract]
    internal sealed class AddFlag<TTarget> : Kernel<TTarget>
        where TTarget : notnull
    {
        [DataMember]
        public int Flag { get; set; }

        public AddFlag(Operator<TTarget> @operator, int flag) : base(@operator)
        {
            this.Flag = flag;
        }

        public override bool Process(Rule<TTarget> rule)
        {
            bool b = this.Operator.Process(rule);
            rule.UserStatus = b ? rule.UserStatus | Flag : rule.UserStatus;
            return b;
        }
    }
}
