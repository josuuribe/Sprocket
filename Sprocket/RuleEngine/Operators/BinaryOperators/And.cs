using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators.BinaryOperators
{
    [DataContract]
    internal sealed class And<TTarget> : BinaryOperator<TTarget>
        where TTarget : notnull
    {
        public And() : base()
        { }
        public override bool Process(Rule<TTarget> element)
        {
            return OperatorLeft.Process(element) && OperatorRight.Process(element);
        }
    }
}
