using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;

namespace RaraAvis.Sprocket.RuleEngine.Interfaces
{
    /// <summary>
    /// Interface that operates, for example using an operator.
    /// </summary>
    /// <typeparam name="TElement">An IElement operator.</typeparam>
    public interface IOperator<TElement>
        where TElement : IElement
    {
        /// <summary>
        /// Matches, executes the rule using element information.
        /// </summary>
        /// <param name="element">An element with information to process.</param>
        /// <returns>True if matches, false otherwise.</returns>
        bool Operate(Rule<TElement> rule);

        IOperator<TElement> Next { get; set; }

        IOperator<TElement> Previous { get; set; }
    }
}
