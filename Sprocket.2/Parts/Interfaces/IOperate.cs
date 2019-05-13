using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Parts.Interfaces
{
    /// <summary>
    /// Obtain information about element.
    /// </summary>
    /// <typeparam name="T">An IElement object.</typeparam>
    /// <typeparam name="U">The returned value with information.</typeparam>
    public interface IOperate<T, out U>
        where T : IElement
    {
        /// <summary>
        /// Gets value for an element given.
        /// </summary>
        /// <param name="element">Information to get or set about rule.</param>
        /// <returns>Specified value.</returns>
        U Value(RuleElement<T> element);
    }
}
