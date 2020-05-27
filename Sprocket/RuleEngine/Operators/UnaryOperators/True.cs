using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators.UnaryOperators
{
    [DataContract]
    internal class True<TTarget> : UnaryOperator<TTarget>
        where TTarget : notnull
    {
        public True() : base()
        { }
        public True(Operator<TTarget> @operator) : base(@operator)
        { }
        public override bool Process(Rule<TTarget> rule)
        {
            Operator.Process(rule);
            return true;
        }
    }
}


