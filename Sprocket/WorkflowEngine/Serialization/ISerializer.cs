using RaraAvis.Sprocket.RuleEngine.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace RaraAvis.Sprocket.WorkflowEngine.Serialization
{
    /// <summary>
    /// Interface to be used to serialize targets.
    /// </summary>
    /// <typeparam name="TTarget"></typeparam>
    public interface ISerializer<TTarget>
        where TTarget : notnull
    {
        /// <summary>
        /// Serializes a <see cref="IOperator{TTarget}"/>.
        /// </summary>
        /// <param name="operator">Operator to serialize.</param>
        /// <returns>Serialized string.</returns>
        [return: NotNull]
        string Serialize(IOperator<TTarget> @operator);
        /// <summary>
        /// Deserializes a string to get a <see cref="IOperator{TTarget}" />.
        /// </summary>
        /// <param name="serialized">String to be deserialized.</param>
        /// <returns>A <see cref="IOperator{TTarget}"/> object or null if object can not be deserialized.</returns>
        [return: MaybeNull]
        IOperator<TTarget> Deserialize(string serialized);
    }
}
