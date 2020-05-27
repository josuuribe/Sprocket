using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators.UnaryOperators
{
    [DataContract]
    internal class Not<TTarget> : UnaryOperator<TTarget>
        where TTarget : notnull
    {
        public Not() : base()
        { }

        public override bool Process(Rule<TTarget> rule)
        {
            return !Operator.Process(rule);
        }
    }
}
