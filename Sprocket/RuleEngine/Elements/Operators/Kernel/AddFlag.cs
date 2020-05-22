using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators.Kernel
{
    [DataContract]
    internal sealed class AddFlag<TElement> : Kernel<TElement>
        where TElement : IElement
    {
        [DataMember]
        public int Flag { get; set; }

        public AddFlag(Operator<TElement> @operator) : base(@operator)
        {
        }

        public override bool Process(Rule<TElement> rule)
        {
            bool b = this.Operator.Process(rule);
            rule.UserStatus = b ? rule.UserStatus | Flag : rule.UserStatus;
            return b;
        }
    }
}
