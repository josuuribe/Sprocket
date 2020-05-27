using RaraAvis.Sprocket.RuleEngine;
using RaraAvis.Sprocket.RuleEngine.Interfaces;

namespace RaraAvis.Sprocket.WorkflowEngine.Serialization.Serializers
{
    internal class NullSerializer<TTarget> : ISerializer<TTarget>
        where TTarget : notnull
    {
        public IOperator<TTarget> Deserialize(string text)
        {
            return new Operator<TTarget>.NullOperator();
        }

        public string Serialize(IOperator<TTarget> @operator)
        {
            return "nulloperator";
        }
    }
}
