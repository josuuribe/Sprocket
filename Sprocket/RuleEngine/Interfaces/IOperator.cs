using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Collections.Generic;

namespace RaraAvis.Sprocket.RuleEngine.Interfaces
{
    /// <summary>
    /// Interface that operates, for example using an operator.
    /// </summary>
    /// <typeparam name="TElement">An IElement operator.</typeparam>
    public interface IOperator<TElement> : IProcessor<TElement, bool>
        where TElement : IElement
    {

    }
}
