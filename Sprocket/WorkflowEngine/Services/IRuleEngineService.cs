using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Collections.Generic;

namespace RaraAvis.Sprocket.WorkflowEngine.Services
{
    /// <summary>
    /// Expose all requires functionality a rule engine must adopt.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //[InheritedExport(typeof(IRuleEngineService<>))]
    public interface IRuleEngineService<T>
        where T : IElement
    {
        /// <summary>
        /// Starts an engine manager.
        /// </summary>
        /// <param name="stages">Woorkflow stages.</param>
        /// <param name="stags">Element to process.</param>
        Rule<T> Init(IOperator<T> op, T element);

        string Serialize(IOperator<T> @operator);

        IOperator<T> Deserialize(string serialized);
    }
}
