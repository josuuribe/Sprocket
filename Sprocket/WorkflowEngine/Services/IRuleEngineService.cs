using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using RaraAvis.Sprocket.WorkflowEngine.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace RaraAvis.Sprocket.WorkflowEngine.Services
{
    /// <summary>
    /// Expose all requires functionality a rule engine must adopt.
    /// </summary>
    /// <typeparam name="TTarget">Target to be used for serialization purposes.</typeparam>
    public interface IRuleEngineService<TTarget>
        where TTarget : notnull
    {
        /// <summary>
        /// Starts a rule engine.
        /// </summary>
        /// <param name="operator">Operator to be executed.</param>
        /// <param name="target">Target to be used.</param>
        /// <returns>Processed rule with all information.</returns>
        [return: NotNull]
        Rule<TTarget> Init([DisallowNull]IOperator<TTarget> @operator, [DisallowNull]TTarget target);
        /// <summary>
        /// Serializer that is being used, this can be injected with a custom implementation via MEF.
        /// </summary>
        [DisallowNull]
        ISerializer<TTarget> Serializer { get; }
    }
}
