using System.Collections.Generic;

namespace RaraAvis.Sprocket.RuleEngine.Interfaces
{
    /// <summary>
    /// Obtain information about element.
    /// </summary>
    /// <typeparam name="TElement">An IElement object.</typeparam>
    /// <typeparam name="TValue">The returned value with information.</typeparam>
    public interface IOperand<TElement, TValue> : IProcessor<TElement, TValue>
        where TElement : IElement
    {    }
}
