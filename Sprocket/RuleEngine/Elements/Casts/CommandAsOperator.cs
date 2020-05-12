using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Casts
{
    [DataContract]
    internal class CommandAsOperator<TElement, TValue> : OperateAsOperator<TElement, TValue>
        where TElement : IElement
    {
        public CommandAsOperator(Operand<TElement, TValue> operate) : base(operate) { }

        public override bool Operate(Rule<TElement> rule)
        {
            Operand.Value(rule.Element);
            return true;
        }
    }
}
