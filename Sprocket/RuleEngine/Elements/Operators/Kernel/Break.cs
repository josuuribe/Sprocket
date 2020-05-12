using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using RaraAvis.Sprocket.WorkflowEngine.Entities.Enums;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators.Kernel
{
    /// <summary>
    /// Breaks execution for this rule.
    /// </summary>
    /// <typeparam name="TElement">An IElement object.</typeparam>
    [DataContract]
    internal sealed class Break<TElement> : Kernel<TElement>
        where TElement : IElement
    {
        public Break(Operator<TElement> operate) : base(operate)
        { }

        public override bool Operate(Rule<TElement> rule)
        {
            bool b = this.Operator.Operate(rule.Element);
            rule.StageAction = b ? StageAction.Break : rule.StageAction;
            return b;
        }
    }
}
