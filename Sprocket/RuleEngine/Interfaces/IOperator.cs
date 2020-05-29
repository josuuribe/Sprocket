using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Diagnostics.CodeAnalysis;

namespace RaraAvis.Sprocket.RuleEngine.Interfaces
{
    /// <summary>
    /// Interface to be used by any operators that performs an operation on target.
    /// </summary>
    /// <typeparam name="TTarget">Target type to be used.</typeparam>
    public interface IOperator<TTarget>
        where TTarget : notnull
    {
        /// <summary>
        /// Operator will execute an operatio using the information in rule and also setting data on it.
        /// </summary>
        /// <param name="rule">Rule object to be used specific to this <see cref="Rule{TTarget}"/>.</param>
        /// <returns>True if the operation has returned true, false otherwise.</returns>
        bool Process(Rule<TTarget> rule);
    }
}
