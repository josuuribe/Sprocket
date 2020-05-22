using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
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
        [DataMember]
        ExecutionResult ExecutionResult { get; set; }

        public Break(Operator<TElement> @operator, ExecutionResult executionResult) : base(@operator)
        {
            this.ExecutionResult = executionResult;
        }

        public override bool Process(Rule<TElement> rule)
        {
            bool b = this.Operator.Process(rule.Element);
            rule.ExecutionResult = b ? ExecutionResult : rule.ExecutionResult;
            return b;
        }
    }
}
