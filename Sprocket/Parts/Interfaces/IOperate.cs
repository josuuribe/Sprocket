using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Parts.Interfaces
{
    /// <summary>
    /// Obtain information about element.
    /// </summary>
    /// <typeparam name="TElement">An IElement object.</typeparam>
    /// <typeparam name="TValue">The returned value with information.</typeparam>
    public interface IOperate<TElement, out TValue>
        where TElement : IElement
    {
        /// <summary>
        /// Gets value for an element given.
        /// </summary>
        /// <param name="element">Information to get or set about rule.</param>
        /// <returns>Specified value.</returns>
        TValue Value(RuleElement<TElement> element);
    }
}
