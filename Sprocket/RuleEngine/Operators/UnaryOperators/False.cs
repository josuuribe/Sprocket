using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators.UnaryOperators
{
    [DataContract]
    internal class False<TTarget> : UnaryOperator<TTarget>
        where TTarget : notnull
    {
        public False() : base()
        { }
        public False(Operator<TTarget> @operator) : base(@operator)
        { }
        public override bool Process(Rule<TTarget> element)
        {
            Operator.Process(element);
            return false;
        }
    }
}
