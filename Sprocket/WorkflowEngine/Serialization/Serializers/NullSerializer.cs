using RaraAvis.Sprocket.RuleEngine;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace RaraAvis.Sprocket.WorkflowEngine.Serialization.Serializers
{
    internal class NullSerializer<TTarget> : ISerializer<TTarget>
        where TTarget : notnull
    {
        [return: MaybeNull]
        public IOperator<TTarget> Deserialize([NotNull] string text)
        {
            return new Operator<TTarget>.NullOperator();
        }
        [return: NotNull]
        public string Serialize([NotNull] IOperator<TTarget> @operator)
        {
            return "nulloperator";
        }
    }
}
