﻿using RaraAvis.Sprocket.WorkflowEngine;

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
        /// Gets processed value based on this <see cref="IOperate{TElement, TValue}"/>.
        /// </summary>
        TValue Value { get; }
        /// <summary>
        /// The element used.
        /// </summary>
        TElement Element { get; }
    }
}
