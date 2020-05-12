namespace RaraAvis.Sprocket.RuleEngine.Interfaces
{
    /// <summary>
    /// Obtain information about element.
    /// </summary>
    /// <typeparam name="TElement">An IElement object.</typeparam>
    /// <typeparam name="TValue">The returned value with information.</typeparam>
    public interface IOperand<TElement, out TValue>
        where TElement : IElement
    {
        /// <summary>
        /// Gets processed value based on this <see cref="IOperand{TElement, TValue}"/>.
        /// </summary>
        TValue Value(TElement element);
        /// <summary>
        /// The element used.
        /// </summary>
        TElement Element { get; }
    }
}
