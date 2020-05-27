using System.Diagnostics.CodeAnalysis;

namespace RaraAvis.Sprocket.RuleEngine.Interfaces
{
    /// <summary>
    /// Acts on target to perform an action o return a value.
    /// </summary>
    /// <typeparam name="TTarget">Root type object to be used by operands.</typeparam>
    /// <typeparam name="TValue">The type returned by any operand processed on target.</typeparam>
    public interface IOperand<in TTarget, out TValue>
        where TTarget : notnull
    {
        /// <summary>
        /// Process an action on a target to get or set a value.
        /// </summary>
        /// <param name="target">Target to be used.</param>
        /// <returns>The value computed by operand.</returns>
        TValue Process([DisallowNull]TTarget target);
    }
}
