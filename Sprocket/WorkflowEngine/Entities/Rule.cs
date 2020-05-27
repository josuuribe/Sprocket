using RaraAvis.Sprocket.RuleEngine;
using System.Diagnostics.CodeAnalysis;

namespace RaraAvis.Sprocket.WorkflowEngine.Entities
{
    /// <summary>
    /// Class that stores the target and information related to the execution context.
    /// </summary>
    /// <typeparam name="T">Target type.</typeparam>
    public sealed class Rule<TTarget>
        where TTarget : notnull
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public Rule()
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        {
            this.ExecutionResult = ExecutionResult.None;
            this.UserStatus = 0;
        }
        /// <summary>
        /// Contructor with element, useful for castings.
        /// </summary>
        /// <param name="element"></param>
        public Rule([DisallowNull]TTarget element) : this()
        {
            this.Target = element;
        }
        /// <summary>
        /// Target object using during the execution.
        /// </summary>
        [DisallowNull]
        public TTarget Target { get; set; }
        /// <summary>
        /// User status set by operators '>>' and '<<' during execution.
        /// </summary>
        public int UserStatus { get; set; }
        /// <summary>
        /// Result returned when this operator finishes.
        /// </summary>
        public ExecutionResult ExecutionResult { get; set; }
        public static implicit operator Rule<TTarget>(TTarget target)
        {
            return new Rule<TTarget>(target);
        }
        public static implicit operator TTarget(Rule<TTarget> rule)
        {
            return rule.Target;
        }
    }
}
