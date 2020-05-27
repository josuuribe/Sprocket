using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators.BinaryOperators
{
    [DataContract]
    internal sealed class AndAlso<TTarget> : BinaryOperator<TTarget>
        where TTarget : notnull
    {
        public AndAlso() : base()
        { }
        public override bool Process(Rule<TTarget> element)
        {
            return OperatorLeft.Process(element) & OperatorRight.Process(element);
        }
    }
}
