using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Parts.Interfaces
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
        bool Match(RuleElement<TElement> element);
    }
}
