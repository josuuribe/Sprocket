using RaraAvis.Sprocket.RuleEngine.Elements.Flows;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using RaraAvis.Sprocket.WorkflowEngine.Entities.Enums;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators.Kernel
{
    /// <summary>
    /// Sets the JMP property to establish next stage to execute.
    /// </summary>
    /// <typeparam name="TElement">An IElement object.</typeparam>
    [DataContract]
    internal sealed class JMP<TElement> : Kernel<TElement>
        where TElement : IElement
    {
        [DataMember]
        public Operator<TElement> Target { get; set; }

        public JMP(Operator<TElement> condition, Operator<TElement> target) : base(condition)
        {
            this.next = new End<TElement>();
            Target = target;
        }

        public override bool Operate(Rule<TElement> rule)
        {
            bool b = this.Operator.Operate(rule.Element);
            this.next = b ? this.Target : this.Target.Next;
            return true;
        }
    }
}
