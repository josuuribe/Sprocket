using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Casts
{
    [DataContract]
    internal class CommandAsOperator<TElement, TValue> : BooleanOperateAsOperator<TElement, TValue>
        where TElement : IElement
    {
        public CommandAsOperator(Operand<TElement, TValue> operate) : base(operate) { }

        public override bool Process(Rule<TElement> rule)
        {
            Operand.Process(rule.Element);
            return true;
        }
    }
}
