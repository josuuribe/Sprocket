using Newtonsoft.Json;
using RaraAvis.Sprocket.RuleEngine.Elements.Flows;
using RaraAvis.Sprocket.RuleEngine.Elements.Operands;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators.Kernel
{
    /// <summary>
    /// Sets the JMP property to establish next stage to execute.
    /// </summary>
    /// <typeparam name="TElement">An IElement object.</typeparam>
    [DataContract]
    internal sealed class Jump<TElement> : Kernel<TElement>
        where TElement : IElement
    {
        [DataMember]
        public (IOperand<TElement, bool>, IOperand<TElement, bool>) Target { get; set; }

        [JsonConstructor]
        public Jump(Operator<TElement> condition) : base(condition)
        {
        }

        public Jump(Operator<TElement> condition, IOperand<TElement, bool> target) : this(condition)
        {
            Target = (target, new Noop<TElement>());
        }

        public Jump(Operator<TElement> condition, (IOperand<TElement, bool>, IOperand<TElement, bool>) target) : this(condition)
        {
            Target = target;
        }

        public override bool Process(Rule<TElement> rule)
        {
            bool b = this.Operator.Process(rule.Element);
            var next = b ? this.Target.Item1 : this.Target.Item2;
            next.Process(rule);
            return b;
        }
    }
}
