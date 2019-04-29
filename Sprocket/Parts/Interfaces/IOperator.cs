using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Collections.Generic;

namespace RaraAvis.Sprocket.Parts.Interfaces
{
    /// <summary>
    /// Interface that operates, for example using an operator.
    /// </summary>
    /// <typeparam name="T">An IElement operator.</typeparam>
    public interface IOperator<T>
        where T : IElement
    {
        /// <summary>
        /// Matches, executes the rule using element information.
        /// </summary>
        /// <param name="element">An element with information to process.</param>
        /// <returns>True if matches, false otherwise.</returns>
        bool Match(RuleElement<T> element);
    }
}
