using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators.BinaryOperators
{
    [DataContract]
    internal sealed class OrElse<TTarget> : BinaryOperator<TTarget>
        where TTarget : notnull
    {
        public OrElse() : base()
        { }
        public override bool Process(Rule<TTarget> rule)
        {
            return OperatorLeft.Process(rule) | OperatorRight.Process(rule);
        }
    }
}
