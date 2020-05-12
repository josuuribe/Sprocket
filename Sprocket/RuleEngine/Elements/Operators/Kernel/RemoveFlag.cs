﻿using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators.Kernel
{
    [DataContract]
    internal sealed class RemoveFlag<TElement> : Kernel<TElement>
        where TElement : IElement
    {
        [DataMember]
        int Flag { get; set; }

        public RemoveFlag(Operator<TElement> @operator, int flag) : base(@operator)
        {
            this.Flag = flag;
        }

        public override bool Operate(Rule<TElement> rule)
        {
            bool b = this.Operator.Operate(rule.Element);
            rule.UserStatus = b ? rule.UserStatus & ~Flag : rule.UserStatus;
            return b;
        }
    }
}
